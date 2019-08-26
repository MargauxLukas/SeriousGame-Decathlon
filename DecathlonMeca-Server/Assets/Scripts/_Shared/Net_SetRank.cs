[System.Serializable]
public class Net_SetRank : NetMessage
{
    public Net_SetRank()
    {
        OperationCode = NetOP.SetRank;
    }

    public int score { set; get; }
    public int scoreMF { set; get; }
    public int scoreRecep { set; get; }
    public int scoreGTP { set; get; }
    public string name { set; get; }
    public string date { set; get; }
}
//Inutile, je m'en sers juste pour afficher un texte avec information