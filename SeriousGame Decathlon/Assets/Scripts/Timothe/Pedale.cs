using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedale : MonoBehaviour
{
    public ColisManager colisManag;

    public void ActivationPedale()
    {
        colisManag.AppelColis();
        if (TutoManager.instance != null)
        {
            TutoManager.instance.Manager(1);
        }
    }
}
