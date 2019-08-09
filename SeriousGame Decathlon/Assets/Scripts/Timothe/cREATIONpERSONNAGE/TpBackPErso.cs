using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpBackPErso : MonoBehaviour
{
    public Transform whereToTp;
    public bool needSupp;
    public DeroulementMenuChoixPerso menu;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((menu.speed > 0 && needSupp) || (menu.speed < 0 && !needSupp))
        {
            if (collision.tag == "PlayerChoice")
            {
                if (menu.speed > 0 && needSupp)
                {
                    collision.transform.position -= new Vector3(Vector3.Distance(whereToTp.position, transform.position), 0, 0);
                }
                else if (menu.speed < 0 && !needSupp)
                {
                    collision.transform.position += new Vector3(Vector3.Distance(whereToTp.position, transform.position), 0, 0);
                }
            }
        }
    }
}
