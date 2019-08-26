[System.Serializable]
public class Net_Request : NetMessage
{
    public Net_Request()
    {
        OperationCode = NetOP.Request;
    }

    public string stringRequest { set; get; }

    public int integer { set; get; }

    public string colis { set; get; }
}

//ICI, C'est quand le Client demande quelque chose au serveur
