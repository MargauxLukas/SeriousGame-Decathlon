[System.Serializable]
public class Net_SetRank : NetMessage
{
    public Net_SetRank()
    {
        OperationCode = NetOP.SetRank;
    }

    public int score { set; get; }
    public string name { set; get; }
}
//Inutile, je m'en sers juste pour afficher un texte avec information