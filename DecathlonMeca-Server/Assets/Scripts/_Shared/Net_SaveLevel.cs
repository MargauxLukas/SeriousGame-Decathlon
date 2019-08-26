[System.Serializable]
public class Net_SaveLevel : NetMessage
{
    public Net_SaveLevel()
    {
        OperationCode = NetOP.SaveLevel;
    }

    public string json { set; get; }
    public int nbLevel { set; get; }
}