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

    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        if (ChargementListeColis.instance != null)
        {
            switch(ChargementListeColis.instance.apparitionPos)
            {
                case 0:
                    player.transform.position = posEntree.position;
                    camera.transform.position = posEntree.position + new Vector3(0, 0, -10);
                    break;
                case 1:
                    player.transform.position = posMF.position;
                    camera.transform.position = posMF.position + new Vector3(0,0,-10);
                    break;
                case 2:
                    player.transform.position = posRecep.position;
                    camera.transform.position = posRecep.position + new Vector3(0, 0, -10);
                    break;
                case 3:
                    player.transform.position = posGTP.position;
                    camera.transform.position = posGTP.position + new Vector3(0, 0, -10);
                    break;
            }
        }
        camera.GetComponent<GTPCameraFollow>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
