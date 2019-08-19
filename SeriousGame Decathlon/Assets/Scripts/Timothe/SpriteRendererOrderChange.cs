using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererOrderChange : MonoBehaviour
{
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        render.sortingOrder =(int)(-transform.position.y*10);
    }
}
