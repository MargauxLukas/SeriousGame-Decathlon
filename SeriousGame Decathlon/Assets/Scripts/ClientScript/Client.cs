using System.Collections;
using System.Collections.Generic;
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
    private const string SERVER_IP = "192.168.137.6";
    private byte reliableChannel;
    private byte error;

    private int hostId;
    private int connectionId;

    private bool isStarted;

    #region Monobehaviour
    private void Start()
    {
        if (instance == null){instance = this;}
        else{Destroy(instance);}
        DontDestroyOnLoad(gameObject);
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
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log("Disconnected to the server");
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

    #region onData
    private void OnData(int connectId, int channelId, int recHostId, NetMessage msg)
    {
        Debug.Log("Received a message of type " + msg.OperationCode);

        switch (msg.OperationCode)
        {
            case NetOP.None:
                Debug.Log("Unexpected NETOP");
                break;

            case NetOP.OnCreateAccount:
                OnCreateAccount((Net_OnCreateAccount)msg);
                break;

            case NetOP.SendingHallOfFame:
                ReceiveHallOfFame((Net_OnSendingHallOfFame)msg);
                break;
        }
    }

    private void OnCreateAccount(Net_OnCreateAccount oca)
    {
        ChargementListeColis.instance.ChangeAuthentificationMessage(oca.Information);
        if(oca.Success != 0)
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
    
    public void SendCreateAccount(string username)
    {
        Net_CreateAccount ca = new Net_CreateAccount();
        ca.Username = username;

        SendServer(ca);
    }

    public void sendLoginRequest(string userName, string password)
    {

    }

    #region HallOfFame
    /******************************************
     *   Demande au serveur des informations  *
     ******************************************/
    public void RequestHallOfFame(bool request)
    {
        Net_RequestHallOfFame rhof = new Net_RequestHallOfFame();
        rhof.isRequest = true;

        SendServer(rhof);
    }

    /**********************************
     *   Reception des informations   *
    ***********************************/
    public void ReceiveHallOfFame(Net_OnSendingHallOfFame oshof)
    {
        AffichageHallOfFame.instance.SetScore(oshof.name , oshof.score, oshof.rank);
    }

    public void SendMyRank(int score, string name)
    {
        Net_SetRank sr = new Net_SetRank();
        sr.score = score;
        sr.name = name;

        SendServer(sr);
    }
    #endregion

    public void SendWayticket(string json, string name)
    {
        Net_LoadWayticket lwt = new Net_LoadWayticket();
        lwt.json = json;
        lwt.name = name;

        SendServer(lwt);
    }
}
