using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    public DBAccess dbAccess;

    private const int MAX_USER  = 100  ;
    private const int PORT      = 26000;
    private const int WEB_PORT  = 26001;
    private const int BYTE_SIZE = 1024 ;

    private byte reliableChannel;
    private int hostId;
    private int webHostId;

    private bool isStarted;
    private byte error;


    #region Monobehaviour
    private void Start()
    {
        GetLocalIPAddress();
        //Debug.Log(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable());
        DontDestroyOnLoad(gameObject);
        Init();
    }

    /*********************************************************************************************
    *   Permet de connaître l'adresse IP du serveur (Le client en a besoin pour s'y connecter)   *
    **********************************************************************************************/
    public static string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                Debug.Log(ip.ToString());
                return ip.ToString();
            }
        }

        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    private void Update()
    {
        UpdateMessagePump();
    }
    #endregion

    /*******************************
     *   Initialisation SERVEUR    *
     *******************************/
    public void Init()
    {
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel = cc.AddChannel(QosType.Reliable);

        HostTopology topo = new HostTopology(cc, MAX_USER);

        // Server only code
        hostId    = NetworkTransport.AddHost         (topo, PORT    , null);
        webHostId = NetworkTransport.AddWebsocketHost(topo, WEB_PORT, null);

        Debug.Log(string.Format("Opening connection on Port {0} and webPort {1}", PORT, WEB_PORT));
        isStarted = true;
    }

    /****************************************************
     *    A appeller si on veut eteindre le Serveur     *
     ****************************************************/
    public void Shutdown()
    {
        isStarted = false;
        NetworkTransport.Shutdown();
    }

    public void UpdateMessagePump()
    {
        if(!isStarted) { return; }

        int recHostId;      // Web or Standalone ?
        int connectionID;   // Which user is sending me this ?
        int channelId;      // Which lane is he sending that message from

        byte[] recBuffer = new byte[BYTE_SIZE];
        int dataSize;

        NetworkEventType type = NetworkTransport.Receive(out recHostId, out connectionID, out channelId, recBuffer, BYTE_SIZE, out dataSize, out error);

        switch(type)
        {
            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                Debug.Log(string.Format("User {0} has connected through host {1}", connectionID, recHostId));
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected!", connectionID));
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
    /***********************************************
     *    Quand le client m'envois des trucs       *
     ***********************************************/
    private void OnData(int connectId, int channelId, int recHostId, NetMessage msg)
    {
        Debug.Log("Received a message of type " + msg.OperationCode);

        switch(msg.OperationCode)
        {
            case NetOP.None:
                Debug.Log("Unexpected NETOP");
                break;

            case NetOP.SendName:
                ReceiveName(connectId, channelId, recHostId, (Net_GiveName)msg);
                break;

            case NetOP.RequestHallOfFame:
                HallOfFame(connectId, channelId, recHostId, (Net_RequestHallOfFame)msg);
                break;

            case NetOP.SetRank:
                SetRanking(connectId, channelId, recHostId, (Net_SetRank)msg);
                break;

            case NetOP.SaveWayticket:
                LoadWayticket(connectId, channelId, recHostId, (Net_SaveWayticket)msg);
                break;
        }
    }

    private void LoadWayticket(int connectId, int channelId, int recHostId, Net_SaveWayticket lwt)
    {
        Net_Information info = new Net_Information();
        info.Success = 0;
        info.Information = "Wayticket reçu";

        SaveLoadSystem.instance.SaveWayTicket(lwt.json, lwt.name);
    }

    //A CHANGER PART AUTRE CHOSE
    private void ReceiveName(int connectId, int channelId, int recHostId, Net_GiveName gn)
    {
        Debug.Log(string.Format("{0}", gn.Username));

        Net_Information info = new Net_Information();
        info.Success = 0;
        info.Information = "Account was created :)";

        SendClient(recHostId, connectId, info);
    }
    #endregion

    #region Send
    /******************************************************************************************************************
     *      Ici, on envoie des trucs au client, les messages sont Serialize et le message est de type "Net_..."       *
     ******************************************************************************************************************/
    public void SendClient(int recHost, int connectId, NetMessage msg)
    {
        // This is where we hold our data
        byte[] buffer = new byte[BYTE_SIZE];

        // This is where you would crush your data into a byte[]
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);
        if (recHost == 0){NetworkTransport.Send(hostId   , connectId, reliableChannel, buffer, BYTE_SIZE, out error);}
        else             {NetworkTransport.Send(webHostId, connectId, reliableChannel, buffer, BYTE_SIZE, out error);}
    }
    #endregion

    #region HallOfFameRequest
    /*************************************************************************************************
     *     Lorsque le client fait une Requête pour acceder au Hall Of Fame, il se retrouve ici       *
     *************************************************************************************************/
    public void HallOfFame(int connectId, int channelId, int recHostId, Net_RequestHallOfFame rhof)
    {
        Net_Information info = new Net_Information();

        info.Success = 0;
        info.Information = "On cherche dans la bd";                                                              //On lui informe juste qu'on a reçu sa requête (Peut servir à afficher un message d'attente ou autre)

        SendClient(recHostId, connectId, info);                                                                  

        Net_OnSendingHallOfFame oshof = new Net_OnSendingHallOfFame();

        for(int i = 1; i <= 10; i++)
        {
            oshof.name = dbAccess.getHallOfFameName(i);
            oshof.score = dbAccess.getHallOfFameScore(i);
            oshof.rank = i;

            SendClient(recHostId, connectId, oshof);                                                            //On lui envois le classement 1 par 1, peut importe dans l'ordre ou arrive les paquets puisque je lui donne aussi le rank, du coup il sait ou le ranger même si il reçoit le 7eme avant le 1er
        }

        //dbAccess.CanCloseDB();
    }

    public void SetRanking(int connectId, int channelId, int recHostId, Net_SetRank sr)
    {
        dbAccess.SetRanking(sr.score, sr.name);
    }
    #endregion
}
