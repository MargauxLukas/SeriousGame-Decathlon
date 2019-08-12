[System.Serializable]
public class Net_SendLevel : NetMessage
{
    public Net_SendLevel()
    {
        OperationCode = NetOP.ReceiveLevel;
    }

    public string file { set; get; }
}