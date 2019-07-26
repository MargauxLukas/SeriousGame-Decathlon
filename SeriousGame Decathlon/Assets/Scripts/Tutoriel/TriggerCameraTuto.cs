using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraTuto : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && TutoManagerRecep.instance != null)
        {
            TutoManagerRecep.instance.Manager(15);
        }
    }
}
