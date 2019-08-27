[System.Serializable]
public class Net_SaveLevelWithoutColis : NetMessage
{
    public Net_SaveLevelWithoutColis()
    {
        OperationCode = NetOP.SaveLevelWithoutColis;
    }

    public string json { set; get; }
    public int nbLevel { set; get; }
}