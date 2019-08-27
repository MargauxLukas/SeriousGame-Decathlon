[System.Serializable]
public class Net_SaveWayticket : NetMessage
{
    public Net_SaveWayticket()
    {
        OperationCode = NetOP.SaveWayticket;
    }

    public string json { set; get; }
    public string name { set; get; }
}
//Inutile, je m'en sers juste pour afficher un texte avec information


