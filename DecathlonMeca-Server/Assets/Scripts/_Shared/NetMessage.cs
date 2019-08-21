using UnityEngine;

/************************************************************************
 *   Lorsqu'on rajoutte un Net_... ne pas oublier de le rajouter ici    *
 ************************************************************************/
public static class NetOP
{
    public const int None                  = 0;
    public const int SendName              = 1;
    public const int Information           = 2;
    public const int Request               = 3;
    public const int ReceiveHallOfFame     = 5;
    public const int SetRank               = 6;
    public const int SaveWayticket         = 7;
    public const int ReceiveWayTicket      = 8;
    public const int SaveColis             = 9;
    public const int SaveLevel             = 10;
    public const int SaveLevelWithoutColis = 11;
    public const int ReceiveColisMF        = 12;
    public const int ReceiveColisRecep     = 13;
    public const int ReceiveLevel          = 14;
    public const int ReceiveDataGeneral    = 15;
    public const int ReceiveGDAndSF        = 16;
    public const int AllData               = 17;
    public const int AllNbData               = 18;
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
