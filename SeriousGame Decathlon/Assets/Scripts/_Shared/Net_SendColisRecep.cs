[System.Serializable]
public class Net_SendColisRecep : NetMessage
{
    public Net_SendColisRecep()
    {
        OperationCode = NetOP.ReceiveColisRecep;
    }

    public string fileColisRecep { set; get; }
    public string fileticket { set; get; }
    public int nbLevel { set; get; }
}