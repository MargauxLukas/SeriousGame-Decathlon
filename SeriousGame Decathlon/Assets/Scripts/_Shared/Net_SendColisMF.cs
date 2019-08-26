[System.Serializable]
public class Net_SendColisMF : NetMessage
{
    public Net_SendColisMF()
    {
        OperationCode = NetOP.ReceiveColisMF;
    }

    public string fileColisMF  { set; get; }
    public string fileticket { set; get; }
    public int nbLevel { set; get; }
}