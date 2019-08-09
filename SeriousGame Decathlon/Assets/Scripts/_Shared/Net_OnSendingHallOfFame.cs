using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Net_OnSendingHallOfFame : NetMessage
{
   public Net_OnSendingHallOfFame()
    {
        OperationCode = NetOP.ReceiveHallOfFame;
    }

    public string name { set; get; }
    public int score { set; get; }
    public int rank { set; get; }
}

//ICI, je range les infos que je dois échanger entre le serveur et le client.
