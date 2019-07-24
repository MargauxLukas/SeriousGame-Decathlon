using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementTapisHautGTP : MonoBehaviour
{
    public bool doesStop = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "ColisGTP")
        {
            if(!collision.GetComponent<CartonVide>().doesTouch && !doesStop)
            {
                collision.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 0.2f;
            }
        }
    }
}
