using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFAce : MonoBehaviour
{
    public RotationScript scriptRot;

    public ColisScript colis;

    public List<Sprite> cartonSprite;
    public SquareFace newFace;

    private void Update()
    {
        if (colis != null)
        {
            if (colis.doesEntranceSecond)
            {
                transform.position = new Vector3(colis.gameObject.transform.position.x + 5f, transform.position.y, 0);
                cartonSprite = colis.colisScriptable.carton.spriteCartonsListe;
                GetComponent<SpriteRenderer>().sprite = scriptRot.GetUpFace(cartonSprite);
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

        //Debug.Log(cartonSprite.Count);
        /*if(scriptRot.actualFace.face == "Right")
        {
            newFace.fullRotation = 90;
        }
        else if(scriptRot.actualFace.face == "Backward")
        {
            newFace.fullRotation = 180;
        }
        else if (scriptRot.actualFace.face == "Left")
        {
            newFace.fullRotation = 270;
        }

        SquareFace temporaryFace = scriptRot.actualFace;
        scriptRot.actualFace = newFace;*/
        newFace = scriptRot.UpdateVueHaut(cartonSprite, GetComponent<SpriteRenderer>(), newFace);
        /*scriptRot.UpdateSprite(cartonSprite, GetComponent<SpriteRenderer>());
        scriptRot.actualFace = temporaryFace;*/
        //Debug.Log(newFace.face);
    }
}
