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
    public bool canReturn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColisScript>() != null && !collision.gameObject.GetComponent<ColisScript>().doesEntrance && !collision.gameObject.GetComponent<ColisScript>().doesEntranceSecond && !collision.gameObject.GetComponent<ColisScript>().doesRenvoie)
        {
            if (collision.gameObject.GetComponent<ColisScript>().colisScriptable.listArticles.Count > 0 && canReturn)
            {
                if (isFinalRenvoie)
                {
                    faceToGet.colis = null;
                    manage.RenvoieColis(collision.gameObject);
                    renvoieManage.ChangePoste(camera, collision.gameObject, cameraPosition, colisPosition);
                    collision.gameObject.GetComponent<ColisScript>().doesRenvoie = true;
                    canReturn = false;
                }
                else
                {
                    faceToGet.colis = collision.gameObject.GetComponent<ColisScript>();
                    renvoieManage.ChangePoste(camera, collision.gameObject, cameraPosition, colisPosition);
                    collision.gameObject.GetComponent<ColisScript>().canMoveVertical = true;
                    collision.gameObject.GetComponent<ColisScript>().entrancePosition = colisPosition.position;
                    collision.gameObject.GetComponent<ColisScript>().doesEntranceSecond = true;
                }
            }
        }
    }

    public void CanReturnNow()
    {
        canReturn = true;
    }
}
