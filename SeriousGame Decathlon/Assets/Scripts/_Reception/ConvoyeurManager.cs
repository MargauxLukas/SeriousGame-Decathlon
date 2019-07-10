using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyeurManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    public GaugeConvoyeur gauge;

    public  float height    = 1f;        //Hauteur actuelle
    private float minHeight = 1f;        //Hauteur minimum
    private float maxHeight = 3f;        //Hauteur maximum

    public float maxReplier;
    public float maxDeplier;

    public bool isOn;
    public bool isReplierMax = true;

    public void Start()
    {
        maxReplier = transform.position.y;
    }

    /********************************************************
     *   Permet de faire descendre ou monter le convoyeur   *
     ********************************************************/
    public void MoveZ(string direction)
    {
        Debug.Log(direction);
        if(direction.Equals("up"))
        {
            if(height <= maxHeight)
            {
                height = height + 0.002f;
                gauge.Up();
            }
        }
        else
        {
            if (height >= minHeight)
            {
                height = height - 0.002f;
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
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 0.01f, camera.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.01f, player.transform.position.z);
                player.GetComponent<Animator>().SetFloat("DirectionY", 1f);
                player.GetComponent<Animator>().SetBool("DoesWalk", true);
                isReplierMax = false;
            }
            else
            {
                PlayerNotMove();
                isReplierMax = true;
            }
        }
        else
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 0.01f, camera.transform.position.z);
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.01f, player.transform.position.z);
            player.GetComponent<Animator>().SetFloat("DirectionY", -1f);
            player.GetComponent<Animator>().SetBool("DoesWalk", true);
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
        if (height >= 1.25f && height < 1.85f)
        {
            gauge.SetPosition(1);
            height = 1.5f;
        } 
        if (height >= 1.85f && height < 2.25f)
        {
            gauge.SetPosition(2);
            height = 2f;
        } 
        if (height >= 2.25f && height < 2.85f)
        {
            gauge.SetPosition(3);
            height = 2.5f;
        } 
        if (height >= 2.85f)
        {
            gauge.SetPosition(4);
            height = 3f;
        } 
    }
}
