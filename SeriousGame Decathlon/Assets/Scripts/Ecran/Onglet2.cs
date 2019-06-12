using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onglet2 : MonoBehaviour
{
    public OngletManager om;

    private void OnMouseDown()
    {
        om.RecountOpen();
    }
}
