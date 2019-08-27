using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixCheminIA : MonoBehaviour //Permet de savoir quel point est relié à quel point sur la scène de l'entrepôt
{
    public List<ChoixCheminIA> voisins;
    public Vector3 position;

    private void Start()
    {
        position = transform.position;
    }

}
