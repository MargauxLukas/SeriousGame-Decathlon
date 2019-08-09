[System.Serializable]
public class Net_GiveName : NetMessage
{
    public Net_GiveName()
    {
        OperationCode = NetOP.SendName;
    }

    public string Username { set; get; }
}
//Inutile mais a garder pour le moment
