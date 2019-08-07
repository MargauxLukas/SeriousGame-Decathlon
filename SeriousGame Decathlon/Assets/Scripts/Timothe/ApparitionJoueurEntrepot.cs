using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionJoueurEntrepot : MonoBehaviour
{
    public GameObject player;

    public Transform posEntree;
    public Transform posMF;
    public Transform posRecep;
    public Transform posGTP;
    // Start is called before the first frame update
    void Start()
    {
        if (ChargementListeColis.instance != null)
        {
            switch(ChargementListeColis.instance.apparitionPos)
            {
                case 0:
                    player.transform.position = posEntree.position;
                    break;
                case 1:
                    player.transform.position = posMF.position;
                    break;
                case 2:
                    player.transform.position = posRecep.position;
                    break;
                case 3:
                    player.transform.position = posGTP.position;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
