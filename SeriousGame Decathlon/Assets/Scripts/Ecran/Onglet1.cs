using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onglet1 : MonoBehaviour
{
    public OngletManager om;

    private void OnMouseDown()
    {
        om.FillingRateOpen();
    }
}
