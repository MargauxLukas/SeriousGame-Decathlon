[System.Serializable]
public class Net_SaveColis : NetMessage
{
    public Net_SaveColis()
    {
        OperationCode = NetOP.SaveColis;
    }

    public string json { set; get; }
    public string name { set; get; }
}
//Inutile, je m'en sers juste pour afficher un texte avec information