[System.Serializable]
public class Net_CreateAccount : NetMessage
{
    public Net_CreateAccount()
    {
        OperationCode = NetOP.CreateAccount;
    }

    public string Username { set; get; }
}
//Inutile mais a garder pour le moment
