using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonDirection : MonoBehaviour
{
    public bool isRight;
    public bool isLeft;
    public bool isUp;
    public bool isDown;

    public ColisScript scriptColis;

    private void Start()
    {
        scriptColis = GameObject.FindGameObjectWithTag("Colis").GetComponent<ColisScript>();
    }

    public void chooseDirection()
    {
        if(isRight)
        {
            Debug.Log("Right");
            scriptColis.Tourner(new Vector2(1,0));
        }
        else if(isLeft)
        {
            Debug.Log("Right");
            scriptColis.Tourner(new Vector2(-1, 0));
        }
        else if (isUp)
        {
            Debug.Log("Right");
            scriptColis.Tourner(new Vector2(0, 1));
        }
        else if (isDown)
        {
            Debug.Log("Right");
            scriptColis.Tourner(new Vector2(0, -1));
        }
    }
}
