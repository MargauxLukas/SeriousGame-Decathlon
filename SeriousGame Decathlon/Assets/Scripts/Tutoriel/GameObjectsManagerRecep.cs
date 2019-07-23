using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsManagerRecep : MonoBehaviour
{
    [Header("Sprite Masks")]
    public GameObject blackScreen;
    public GameObject squareSpriteMask;
    public GameObject circleSpriteMask;

    [Header("Doigt")]
    public GameObject doigtClick;
    public GameObject doigtClickSpriteMask;
    public GameObject doigtStay;
    public GameObject doigtStaySpriteMask;

    [Header("Flèche")]
    public GameObject arrow;
    public GameObject arrowSpriteMask;

    public SpriteRenderer GameObjectToSpriteRenderer(GameObject gameObject)
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }

    public SpriteMask GameObjectToSpriteMask(GameObject gameObject)
    {
        return gameObject.GetComponent<SpriteMask>();
    }

    public Animator GameObjectToAnimator(GameObject gameObject)
    {
        return gameObject.GetComponent<Animator>();
    }

    public Transform GameObjectToTransform(GameObject gameObject)
    {
        return gameObject.GetComponent<Transform>();
    }
}
