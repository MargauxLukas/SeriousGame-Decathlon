using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFAce : MonoBehaviour
{
    public RotationScript scriptRot;

    public ColisScript colis;

    public List<Sprite> cartonSprite;
    public SquareFace newFace;

    public GameObject wayTicket;

    private void Update()
    {
        if (colis != null)
        {
            if (colis.doesEntranceSecond)
            {
                transform.position = new Vector3(colis.gameObject.transform.position.x + 5f, transform.position.y, 0);
                transform.rotation = Quaternion.identity;
                cartonSprite = colis.colisScriptable.carton.spriteCartonsListe;
                GetComponent<SpriteRenderer>().sprite = scriptRot.GetUpFace(cartonSprite);
            }
            if(colis.colisScriptable.isBadOriented)
            {
                //transform.Rotate(new Vector3(0, 0, 90));
            }
            UpdateThisFace();
        }
    }

    public void UpdateThisFace()
    {
        if(newFace == null)
        {
            newFace = scriptRot.actualFace.upVoisin;
        }

        switch(scriptRot.actualFace.face)
        {
            case "Up":
                GetComponent<SpriteRenderer>().sprite = cartonSprite[5];
                transform.rotation = Quaternion.identity;
                wayTicket.SetActive(false);
                break;
            case "Down":
                GetComponent<SpriteRenderer>().sprite = cartonSprite[4];
                transform.rotation = Quaternion.identity;
                if (colis.colisScriptable.wayTicket != null)
                {
                    wayTicket.SetActive(true);
                    Debug.Log("Pourletest");
                }
                break;
            case "Right":
                GetComponent<SpriteRenderer>().sprite = cartonSprite[0];
                transform.rotation = Quaternion.identity;
                transform.Rotate(new Vector3(0, 0, 90));
                wayTicket.SetActive(false);
                break;
            case "Left":
                GetComponent<SpriteRenderer>().sprite = cartonSprite[0];
                transform.rotation = Quaternion.identity;
                transform.Rotate(new Vector3(0, 0, 90));
                wayTicket.SetActive(false);
                break;
            case "Forward":
                GetComponent<SpriteRenderer>().sprite = cartonSprite[0];
                transform.rotation = Quaternion.identity;
                wayTicket.SetActive(false);
                break;
            case "Backward":
                GetComponent<SpriteRenderer>().sprite = cartonSprite[0];
                transform.rotation = Quaternion.identity;
                transform.Rotate(new Vector3(0, 0, 180));
                wayTicket.SetActive(false);
                break;
        }
        
        //newFace = scriptRot.UpdateVueHaut(cartonSprite, GetComponent<SpriteRenderer>(), newFace);
    }
}
