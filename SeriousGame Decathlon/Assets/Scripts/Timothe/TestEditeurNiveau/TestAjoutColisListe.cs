using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAjoutColisListe : MonoBehaviour
{
    public GameObject boutonInfoColis;
    public GameObject boutonAddColis;
    public GameObject ParentColisAdd;

    private Vector2 nextPosition;
    public float decalage;

    public void AddButton()
    {
        GameObject nouveauBouton = Instantiate(boutonInfoColis, ParentColisAdd.transform.position, Quaternion.identity, ParentColisAdd.transform);
    }
}
