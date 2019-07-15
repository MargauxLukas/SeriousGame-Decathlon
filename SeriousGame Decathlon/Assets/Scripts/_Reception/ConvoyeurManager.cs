using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyeurManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    public GaugeConvoyeur gauge;

    public  float height    = 1f;          //Hauteur actuelle
    private float minHeight = 1f;          //Hauteur minimum
    private float maxHeight = 2.6f;        //Hauteur maximum

    public float maxReplier;
    public float maxDeplier;

    public bool isOn;
    public bool isReplierMax = true;

    public GameObject vueGeneralDeplier;
    public GameObject vueGeneralReplier;

    public void Start()
    {
        maxReplier = transform.position.y;
        //maxdeplier
    }

    /********************************************************
     *   Permet de faire descendre ou monter le convoyeur   *
     ********************************************************/
    public void MoveZ(string direction)
    {
        if(direction.Equals("up"))
        {
            if(height <= maxHeight)                             //Peut monter tant qu'il a pas atteint le maxHeight
            {
                height = height + 0.004f;
                gauge.Up();
            }
        }
        else
        {
            if (height >= minHeight)                            //Peut descendre tant qu'il a pas atteint le minHeight
            {
                height = height - 0.004f;
                gauge.Down();
            }
        }
    }

    /********************************************************
    *   Permet de faire avancer ou reculer le convoyeur     *
    ********************************************************/
    public void MoveY(string direction)
    {
        if (direction.Equals("replier"))
        {
            if (transform.position.y <= maxReplier)
            {
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 0.02f, camera.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.02f, player.transform.position.z);
                player.GetComponent<Animator>().SetFloat("DirectionY", 1f);
                player.GetComponent<Animator>().SetBool("DoesWalk", true);
                isReplierMax = false;
            }
            else
            {
                PlayerNotMove();
                isReplierMax = true;
                vueGeneralDeplier.SetActive(false);
                vueGeneralReplier.SetActive(true);
            }
        }
        else
        {
            vueGeneralDeplier.SetActive(true);
            vueGeneralReplier.SetActive(false);
            //if (transform.position.y >= maxDeplier)
            //{
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 0.02f, camera.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.02f, player.transform.position.z);
                player.GetComponent<Animator>().SetFloat("DirectionY", -1f);
                player.GetComponent<Animator>().SetBool("DoesWalk", true);
            //}
            /*else
            {
                PlayerNotMove();
            }*/
        }
    }

    /********************************************************
    *   Function pour dire au joueur d'arreter de marcher   *
    ********************************************************/
    public void PlayerNotMove()
    {
        player.GetComponent<Animator>().SetFloat("DirectionY", 0f);
        player.GetComponent<Animator>().SetBool("DoesWalk", false);
    }

    public void SetFloor()
    {
        if(height < 1.25f)  //1m
        {
            gauge.SetPosition(0);
            height = 1f;
        }
        if (height >= 1.25f && height < 1.85f) //1m50
        {
            gauge.SetPosition(1);
            height = 1.5f;
        } 
        if (height >= 1.85f && height < 2.25f)   //2m
        {
            gauge.SetPosition(2);
            height = 2f;
        } 
        if (height >= 2.25f)    //2.5m
        {
            gauge.SetPosition(3);
            height = 2.5f;
        } 
        /*if (height >= 2.85f)     //3m
        {
            gauge.SetPosition(4);
            height = 3f;
        } */
    }
}
