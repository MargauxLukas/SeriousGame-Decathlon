[System.Serializable]
public class Net_SendColis : NetMessage
{
    public Net_SendColis()
    {
        OperationCode = NetOP.ReceiveColis;
    }

    public string file { set; get; }
    public string ticket { set; get; }
}