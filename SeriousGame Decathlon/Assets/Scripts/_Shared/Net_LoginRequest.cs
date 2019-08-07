using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Net_LoginRequest : NetMessage
{
    public Net_LoginRequest()
    {
        OperationCode = NetOP.LoginRequest;
    }

    public string Username { set; get; }
    public string Password { set; get; }
}
//Tres inutile, a virer quand on veut car il est inutile mais pas oublier de mettre à jour le _Shared des 2 côtés (Client/Server)
