using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpBackPErso : MonoBehaviour
{
    public Transform whereToTp;
    public bool needSupp;
    public DeroulementMenuChoixPerso menu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((menu.speed > 0 && needSupp) || (menu.speed < 0 && !needSupp))
        {
            if (collision.tag == "PlayerChoice")
            {
                collision.transform.position = whereToTp.position;
            }
        }
    }
}
