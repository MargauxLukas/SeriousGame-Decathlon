[System.Serializable]
public class Net_SendGDAndSF : NetMessage
{
    public Net_SendGDAndSF()
    {
        OperationCode = NetOP.ReceiveGDAndSF;
    }

    public bool isSaveFile { set; get; }
    public string dataSaved { set; get; }
}


