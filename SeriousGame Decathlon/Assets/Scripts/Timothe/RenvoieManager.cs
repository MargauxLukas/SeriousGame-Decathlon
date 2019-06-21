using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenvoieManager : MonoBehaviour
{
    public ColisManager manager;

    public void ChangePoste (GameObject camera, GameObject colis, Transform camPos, Transform colisPos)
    {
        camera.transform.position = camPos.position;
        colis.transform.position = colisPos.position;
    }
}
