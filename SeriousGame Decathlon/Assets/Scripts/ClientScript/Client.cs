using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    public static Client instance { private set; get; }

    private const int MAX_USER  = 100  ;
    private const int PORT      = 26000;
    private const int WEB_PORT  = 26001;
    private const int BYTE_SIZE = 1024 ;
    //private const string SERVER_IP = "127.0.0.1";                  //Use 127.0.0.1 for local (When tablet is connected to PC)
    private const string SERVER_IP = "192.168.137.6";              //WifiTimothe
    //private const string SERVER_IP = "172.19.52.86";               //DKTWarehouse

    private byte reliableChannel;
    private byte error;

    private int hostId = -1;
    private int connectionId;

    private bool isStarted;
    public bool isConnectedToServer = false;

    //Tests
    public Net_SendLevel slSave;
    public Net_SendWayTicket swSave;
    public Net_SendGeneralData gdSave;

    #region Monobehaviour
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        Init();
    }
    #endregion

    private void Update()
    {
        UpdateMessagePump();
    }

    public void Init()
    {
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel = cc.AddChannel(QosType.Reliable);

        HostTopology topo = new HostTopology(cc, MAX_USER);

        // Client only code
        hostId = NetworkTransport.AddHost(topo, 0);

#if UNITY_WEBGL && !UNITY_EDITOR
        // Web Client
        connectionId = NetworkTransport.Connect(hostId, SERVER_IP, WEB_PORT, 0, out error);
        Debug.Log("Connecting from Web");
#elif UNITY_ANDROID && !UNITY_EDITOR
        // Android Client
        connectionId = NetworkTransport.Connect(hostId, SERVER_IP, PORT, 0, out error);
        Debug.Log("Connecting from Android");
#else
        // Standalone Client
        connectionId = NetworkTransport.Connect(hostId, SERVER_IP, PORT, 0, out error);
        Debug.Log("Connecting from Standalone");
#endif

       Debug.Log("Attemping to connect on " + SERVER_IP);
        isStarted = true;
    }

    public void Shutdown()
    {
        isStarted = false;
        NetworkTransport.Shutdown();
    }

    public void UpdateMessagePump()
    {
        if (!isStarted) { return; }

        int recHostId;      // Web or Standalone ?
        int connectionID;   // Which user is sending me this ?
        int channelId;      // Which lane is he sending that message from

        byte[] recBuffer = new byte[BYTE_SIZE];
        int dataSize;

        NetworkEventType type = NetworkTransport.Receive(out recHostId, out connectionID, out channelId, recBuffer, BYTE_SIZE, out dataSize, out error);
        switch (type)
        {
            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                Debug.Log("Connected to the server");
                isConnectedToServer = true;
                SaveLoadSystem.instance.LoadGeneralData("GeneralDataStart");
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log("Disconnected to the server");
                isConnectedToServer = false;
                break;

            case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer);
                NetMessage msg = (NetMessage)formatter.Deserialize(ms);

                OnData(connectionID, channelId, recHostId, msg);
                break;

            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected network event type");
                break;
        }
    }

    /********************************
     *   Ce que le client reçoit    *
     ********************************/
    #region onData
    private void OnData(int connectId, int channelId, int recHostId, NetMessage msg)
    {
        Debug.Log("Received a message of type " + msg.OperationCode);

        switch (msg.OperationCode)
        {
            case NetOP.None:
                Debug.Log("Unexpected NETOP");
                break;

            case NetOP.Information:
                GiveInformation((Net_Information)msg);
                break;

            case NetOP.ReceiveHallOfFame:
                ReceiveHallOfFame((Net_OnSendingHallOfFame)msg);
                break;

            case NetOP.ReceiveWayTicket:
                swSave = (Net_SendWayTicket)msg;
                break;

            case NetOP.ReceiveColisMF:
                LoadColisMFForLevel((Net_SendColisMF)msg);
                break;

            case NetOP.ReceiveColisRecep:
                LoadColisRecepForLevel((Net_SendColisRecep)msg);
                break;

            case NetOP.ReceiveLevel:
                LoadLevel((Net_SendLevel)msg);
                break;

            case NetOP.ReceiveDataGeneral:
                gdSave = (Net_SendGeneralData)msg;
                break;

            case NetOP.ReceiveGDAndSF:
                LoadGDAndSF((Net_SendGDAndSF)msg);
                break;
        }
    }

    private void GiveInformation(Net_Information info)
    {
        ChargementListeColis.instance.ChangeAuthentificationMessage(info.Information);
        if(info.Success != 0)
        {
            //Unable
        }
        else
        {
            //Successfull
        }
    }
    #endregion

    #region Send
    public void SendServer(NetMessage msg)
    {
        // This is where we hold our data
        byte[] buffer = new byte[BYTE_SIZE];

        // This is where you would crush your data into a byte[]
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);

        formatter.Serialize(ms, msg);

        NetworkTransport.Send(hostId, connectionId, reliableChannel, buffer, BYTE_SIZE, out error);
    }
    #endregion
    
    public void SendName(string username)
    {
        Net_GiveName gn = new Net_GiveName();
        gn.Username = username;

        SendServer(gn);
    }

    #region HallOfFame
    /******************************************
     *   Demande au serveur des informations  *
     ******************************************/
    public void RequestHallOfFame()
    {
        Net_Request request = new Net_Request();
        request.stringRequest = "HallOfFame";

        SendServer(request);
    }

    /**********************************
     *   Reception des informations   *
    ***********************************/
    public void ReceiveHallOfFame(Net_OnSendingHallOfFame oshof)
    {
        AffichageHallOfFame.instance.SetScore(oshof.name , oshof.score, oshof.rank, oshof.tab);
    }

    public void SendMyRank(int score, string name, string date, int scoreMF, int scoreRecep, int scoreGTP)
    {
        Net_SetRank sr = new Net_SetRank();
        sr.score      = score     ;
        sr.name       = name      ;
        sr.date       = date      ;  
        sr.scoreMF    = scoreMF   ;
        sr.scoreRecep = scoreRecep;
        sr.scoreGTP   = scoreGTP  ;

        SendServer(sr);
    }
    #endregion
    public void SendRequest(string req)
    {
        Net_Request request = new Net_Request();
        request.stringRequest = req;

        SendServer(request);
    }

    public void SendWayticket(string json, string name)
    {
        Net_SaveWayticket swt = new Net_SaveWayticket();
        swt.json = json;
        swt.name = name;

        SendServer(swt);
    }

    public void SendColis(string json, string name)
    {
        Net_SaveColis sc = new Net_SaveColis();
        sc.json = json;
        sc.name = name;

        SendServer(sc);
    }

    public void SendLevel(string json, int nbLevel)
    {
        Net_SaveLevel sl = new Net_SaveLevel();
        sl.json = json;
        sl.nbLevel = nbLevel;

        SendServer(sl);
    }

    public void LoadLevel(Net_SendLevel sl)
    {
        ChoixNiveauManager.instance.affichageLevel(sl.file, sl.nbLevel);
    }

    public void LoadColisMFForLevel(Net_SendColisMF scmf)
    {
        ChoixNiveauManager.instance.SelectLevelMF(scmf.fileColisMF, scmf.fileticket, scmf.nbLevel);
    }

    public void LoadColisRecepForLevel(Net_SendColisRecep scr)
    {
        ChoixNiveauManager.instance.SelectLevelRecep(scr.fileColisRecep, scr.fileticket, scr.nbLevel);
    }

    public void LoadGDAndSF(Net_SendGDAndSF sgdsf)
    {
        SavedData dataToLoad = SavedData.CreateInstance<SavedData>();
        JsonUtility.FromJsonOverwrite(sgdsf.dataSaved, dataToLoad);
        SaveAllScriptableBeginning.instance.StartAll(sgdsf.isSaveFile, dataToLoad);
    }

    public void SendLevelWithoutColis(string json, int nbLevel)
    {
        Net_SaveLevelWithoutColis slwc = new Net_SaveLevelWithoutColis();
        slwc.json = json;
        slwc.nbLevel = nbLevel;

        SendServer(slwc);
    }
}
