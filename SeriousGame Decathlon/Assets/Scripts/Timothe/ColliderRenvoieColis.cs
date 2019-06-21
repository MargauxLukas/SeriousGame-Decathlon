using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRenvoieColis : MonoBehaviour
{
    public ColisManager manage;

    public RenvoieManager renvoieManage;

    public GameObject camera;

    public Transform cameraPosition;
    public Transform colisPosition;

    public GetFAce faceToGet;

    public bool isFinalRenvoie;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColisScript>() != null && !collision.gameObject.GetComponent<ColisScript>().doesEntrance)
        {
            if(isFinalRenvoie)
            {
                manage.RenvoieColis(collision.gameObject);
                renvoieManage.ChangePoste(camera, collision.gameObject, cameraPosition, colisPosition);
                collision.gameObject.GetComponent<ColisScript>().doesRenvoie = true;
            }
            else
            {
                renvoieManage.ChangePoste(camera, collision.gameObject, cameraPosition, colisPosition);
                collision.gameObject.GetComponent<ColisScript>().canMoveVertical = true;
                collision.gameObject.GetComponent<ColisScript>().entrancePosition = colisPosition.position;
                collision.gameObject.GetComponent<ColisScript>().doesEntrance = true;
            }
        }
    }
}
