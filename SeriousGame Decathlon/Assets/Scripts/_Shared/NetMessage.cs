using UnityEngine;

/************************************************************************
 *   Lorsqu'on rajoutte un Net_... ne pas oublier de le rajouter ici    *
 ************************************************************************/
public static class NetOP
{
    public const int None              = 0;
    public const int CreateAccount     = 1;
    public const int LoginRequest      = 2;
    public const int OnCreateAccount   = 3;
    public const int RequestHallOfFame = 4;
    public const int SendingHallOfFame = 5;
    public const int SetRank           = 6;
}

[System.Serializable]
public abstract class NetMessage
{
    public byte OperationCode { set; get; }

    public NetMessage()
    {
        OperationCode = NetOP.None;
    }
}
