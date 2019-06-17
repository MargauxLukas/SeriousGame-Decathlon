using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRenvoieColis : MonoBehaviour
{
    public ColisManager manage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TestCollider");
        if (collision.gameObject.GetComponent<ColisScript>() != null && !collision.gameObject.GetComponent<ColisScript>().doesEntrance)
        {

            manage.RenvoieColis(collision.gameObject);
        }
    }
}
