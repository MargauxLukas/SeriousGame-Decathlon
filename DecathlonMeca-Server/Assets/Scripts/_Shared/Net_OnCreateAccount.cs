[System.Serializable]
public class Net_OnCreateAccount : NetMessage
{
    public Net_OnCreateAccount()
    {
        OperationCode = NetOP.OnCreateAccount;
    }

    public byte Success { set; get; }
    public string Information { set; get; }
}
//Inutile, je m'en sers juste pour afficher un texte avec information

