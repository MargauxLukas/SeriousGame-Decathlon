using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFAce : MonoBehaviour
{
    public RotationScript scriptRot;

    public ColisScript colis;

    public List<Sprite> cartonSprite;

    public void UpdateThisFace()
    {
        SquareFace newFace = scriptRot.actualFace.upVoisin;
        if(scriptRot.actualFace.face == "Right")
        {
            newFace.fullRotation = 90;
        }
        else if(scriptRot.actualFace.face == "Backward")
        {
            newFace.fullRotation = 180;
        }
        else if (scriptRot.actualFace.face == "Left")
        {
            newFace.fullRotation = 27;
        }

        SquareFace temporaryFace = scriptRot.actualFace;
        scriptRot.actualFace = newFace;
        scriptRot.UpdateSprite(cartonSprite, GetComponent<SpriteRenderer>());
        scriptRot.actualFace = temporaryFace;
    }
}
