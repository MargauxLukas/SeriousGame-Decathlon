using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{
    public static Client instance { private set; get; }

    private const int MAX_USER = 100;
    private const int PORT = 26000;
    private const int WEB_PORT = 26001;
    private const int BYTE_SIZE = 1024;
    //private const string SERVER_IP = "127.0.0.1";                  //Use 127.0.0.1 for local (When tablet is connected to PC)
    //private const string SERVER_IP = "192.168.137.6";                //WifiTimothe
    private const string SERVER_IP = "172.19.52.80";               //DKTWarehouse

    private byte reliableChannel;
    private byte error;

    private int hostId = -1;
    private int connectionId;

    private bool isStarted;
    public bool isConnectedToServer = false;

    public SQLToCSV stcsv;

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

            case NetOP.AllNbData:
                SendNbData((Net_SendNbData)msg);
                break;

            case NetOP.AllData:
                SendData((Net_SendAllData)msg);
                break;
        }
    }

    private void GiveInformation(Net_Information info)
    {
        if (info.Success != 0)
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

    public void SendRequest()
    {
        Net_Request request = new Net_Request();
        request.stringRequest = "File";

        SendServer(request);
    }

    public void SendNbData(Net_SendNbData snbd)
    {
        stcsv.maxGeneral = snbd.nbGeneral;
        stcsv.maxMF      = snbd.nbMF;
        stcsv.maxRecep   = snbd.nbRecep;
        stcsv.maxGTP     = snbd.nbGTP;
    }

    public void SendData(Net_SendAllData sad)
    {
        stcsv.setToAList(sad.data, sad.rank, sad.tab);
    }
}