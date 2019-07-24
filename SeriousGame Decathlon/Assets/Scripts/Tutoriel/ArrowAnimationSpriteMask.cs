using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimationSpriteMask : MonoBehaviour
{
    public GameObject arrow;
    public SpriteMask arrowSpriteMask;
  
    void Update()
    {
        arrowSpriteMask.sprite = arrow.GetComponent<SpriteRenderer>().sprite;
    }
}
