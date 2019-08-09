[System.Serializable]
public class Net_Information : NetMessage
{
    public Net_Information()
    {
        OperationCode = NetOP.Information;
    }

    public byte Success { set; get; }
    public string Information { set; get; }
}
//Inutile, je m'en sers juste pour afficher un texte avec information

