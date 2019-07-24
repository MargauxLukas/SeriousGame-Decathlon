using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionCartonVideGTP : MonoBehaviour
{
    public GameObject prefabColis;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ColisGTP")
        {
            Instantiate(prefabColis, transform.position, Quaternion.identity);
        }
    }
}
