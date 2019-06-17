using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRenvoieColis : MonoBehaviour
{
    public ColisManager manage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ColisScript>() != null && !collision.gameObject.GetComponent<ColisScript>().doesEntrance)
        {
            Debug.Log("Test");
            manage.RenvoieColis(collision.gameObject);
        }
    }
}
