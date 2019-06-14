using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualEditorInfo : MonoBehaviour
{
    public List<Colis> colisDisponible;
    public List<LevelScriptable> niveauxDisponibles;

    public int nbColisDispo;

    private void Start()
    {
        for(int i = 0; i < nbColisDispo; i++)
        {
            //SaveLoadSystem.instance.LoadColis();
        }
    }
}
