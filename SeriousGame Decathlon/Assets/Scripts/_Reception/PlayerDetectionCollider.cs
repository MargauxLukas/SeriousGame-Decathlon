using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionCollider : MonoBehaviour
{
    public ConvoyeurButton convoyeurButton;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!convoyeurButton.isCollide)
        {
            convoyeurButton.isCollide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        convoyeurButton.isCollide = false;
    }
}
