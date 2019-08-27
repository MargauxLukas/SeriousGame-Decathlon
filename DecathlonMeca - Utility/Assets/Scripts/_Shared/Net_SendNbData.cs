[System.Serializable]
public class Net_SendNbData : NetMessage
{
    public Net_SendNbData()
    {
        OperationCode = NetOP.AllNbData;
    }
    public int nbGeneral { set; get; }
    public int nbMF { set; get; }
    public int nbRecep { set; get; }
    public int nbGTP { set; get; }

}
//Inutile, je m'en sers juste pour afficher un texte 