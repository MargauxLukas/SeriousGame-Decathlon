[System.Serializable]
public class Net_SendColis : NetMessage
{
    public Net_SendColis()
    {
        OperationCode = NetOP.ReceiveColis;
    }

    public string fileColis  { set; get; }
    public string fileticket { set; get; }
    public int nbLevel { set; get; }
}