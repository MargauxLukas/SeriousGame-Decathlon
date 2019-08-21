[System.Serializable]
public class Net_SendAllData : NetMessage
{
    public Net_SendAllData()
    {
        OperationCode = NetOP.AllData;
    }
    public int rank { set; get; }
    public string data { set; get; }
    public string tab { set; get; }

}
//Inutile, je m'en sers juste pour afficher un texte avec information