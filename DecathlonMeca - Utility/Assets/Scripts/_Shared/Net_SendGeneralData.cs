[System.Serializable]
public class Net_SendGeneralData : NetMessage
{
    public Net_SendGeneralData()
    {
        OperationCode = NetOP.ReceiveDataGeneral;
    }

    public string file { set; get; }
}

//ICI, C'est quand le Client demande quelque chose au serveur
