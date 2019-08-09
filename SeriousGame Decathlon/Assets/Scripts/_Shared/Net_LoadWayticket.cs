[System.Serializable]
public class Net_LoadWayticket : NetMessage
{
    public Net_LoadWayticket()
    {
        OperationCode = NetOP.LoadWayticket;
    }

    public string json { set; get; }
    public string name { set; get; }
}
//Inutile, je m'en sers juste pour afficher un texte avec information


