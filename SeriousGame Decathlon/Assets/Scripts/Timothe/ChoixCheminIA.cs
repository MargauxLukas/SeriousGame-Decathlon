using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixCheminIA : MonoBehaviour
{
    public List<ChoixCheminIA> voisins;
    public Vector3 position;

    private void Start()
    {
        position = transform.position;
    }

}
