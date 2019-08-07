[System.Serializable]
public class Net_RequestHallOfFame : NetMessage
{
    public Net_RequestHallOfFame()
    {
        OperationCode = NetOP.RequestHallOfFame;
    }

    public bool isRequest { set; get; }
}

//ICI, c'est juste de client a Serveur, ça me sert à savoir si le client cherche a avoir accès au Info du Ranking.
