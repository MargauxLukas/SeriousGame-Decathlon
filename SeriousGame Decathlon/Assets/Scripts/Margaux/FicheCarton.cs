using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FicheCarton : MonoBehaviour
{
    //public string buttonName;
    public GameObject newColis;
    private ColisScript scriptColis;
    public Carton carton;
    public void InstantiateCarton(string buttonName)
    {
        scriptColis = newColis.GetComponent<ColisScript>();
        scriptColis.colisScriptable = Colis.CreateInstance<Colis>();
        scriptColis.colisScriptable.carton = carton;
        scriptColis.colisScriptable.carton.codeRef = buttonName;
        scriptColis.colisScriptable.carton.Initialize();

        Instantiate(newColis, new Vector3(-2, -1.4f, 0),Quaternion.identity);
    }
}
