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

    public List<GameObject> listToDesactivate;
    public List<GameObject> listToActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColisScript>() != null && !collision.gameObject.GetComponent<ColisScript>().doesEntrance && !collision.gameObject.GetComponent<ColisScript>().doesEntranceSecond && !collision.gameObject.GetComponent<ColisScript>().doesRenvoie)
        {
            if (collision.gameObject.GetComponent<ColisScript>().colisScriptable.listArticles.Count > 0)
            {
                if (isFinalRenvoie)
                {
                    faceToGet.colis = null;
                    manage.RenvoieColis(collision.gameObject);
                    StartCoroutine(RenvoieFinalAnim(collision.gameObject));
                    collision.gameObject.GetComponent<ColisScript>().doesRenvoie = true;
                    canReturn = false;
                    if (TutoManager.instance != null) { TutoManager.instance.Manager(29); }
                }
                else if (canReturn)
                {
                    faceToGet.colis = collision.gameObject.GetComponent<ColisScript>();
                    renvoieManage.ChangePoste(camera, collision.gameObject, cameraPosition, colisPosition);
                    collision.gameObject.GetComponent<ColisScript>().canMoveVertical = true;
                    collision.gameObject.GetComponent<ColisScript>().entrancePosition = colisPosition.position;
                    collision.gameObject.GetComponent<ColisScript>().doesEntranceSecond = true;
                    if (TutoManager.instance != null) { TutoManager.instance.Manager(24); }
                }
                if(listToActive.Count>0)
                {
                    foreach(GameObject obj in listToActive)
                    {
                        obj.SetActive(true);
                    }
                }

                if(listToDesactivate.Count>0)
                {
                    foreach(GameObject obj in listToDesactivate)
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
    }

    IEnumerator RenvoieFinalAnim(GameObject obj)
    {
        yield return new WaitForSeconds(1.9f);
        renvoieManage.ChangePoste(camera, obj, cameraPosition, colisPosition);
    }

    public void CanReturnNow()
    {
        canReturn = true;
    }
}
