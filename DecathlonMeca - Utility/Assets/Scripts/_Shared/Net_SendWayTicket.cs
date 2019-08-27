[System.Serializable]
public class Net_SendWayTicket : NetMessage
{
    public Net_SendWayTicket()
    {
        OperationCode = NetOP.ReceiveWayTicket;
    }

    public string file   { set; get; }
    public string ticket { set; get; }
}