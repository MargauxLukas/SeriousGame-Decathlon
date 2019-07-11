using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionCollider : MonoBehaviour
{
    public ConvoyeurButton convoyeurButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        convoyeurButton.isCollide = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        convoyeurButton.isCollide = false;
    }
}
