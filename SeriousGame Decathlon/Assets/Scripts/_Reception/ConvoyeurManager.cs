using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyeurManager : MonoBehaviour
{
    [Header("Camera")]
    public GameObject camera;

    [Header("Player")]
    public GameObject player;

    [Header("Pointeur")]
    public GaugeConvoyeur gauge;

    private float height    = 1f  ;          //Hauteur actuelle
    private float minHeight = 1f  ;          //Hauteur minimum
    private float maxHeight = 2.6f;          //Hauteur maximum
    public float maxReplier;
    private float maxDeplier;

    [Header("Boolean")]
    public bool isOn;
    public bool isReplierMax = true;

    [Header("Sprite Vue General")]
    public GameObject vueGeneralDeplier;
    public GameObject vueGeneralReplier;

    public void Start()
    {
        maxReplier = camera.transform.position.y;
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
            Debug.Log(camera.transform.position.y);
            if (camera.transform.position.y <= maxReplier)
            {
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 0.02f, camera.transform.position.z);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.02f, player.transform.position.z);
                player.GetComponent<Animator>().SetFloat("DirectionY",   1f);
                player.GetComponent<Animator>().SetBool ("DoesWalk"  , true);
                isReplierMax = false;
            }
            else
            {
                PlayerNotMove();
                isReplierMax = true;
                vueGeneralDeplier.SetActive(false);
                vueGeneralReplier.SetActive(true );
            }
        }
        else
        {
            isReplierMax = false;
            vueGeneralDeplier.SetActive(true );
            vueGeneralReplier.SetActive(false);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 0.02f, camera.transform.position.z);
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.02f, player.transform.position.z);
            player.GetComponent<Animator>().SetFloat("DirectionY",  -1f);
            player.GetComponent<Animator>().SetBool ("DoesWalk"  , true);
        }
    }

    /********************************************************
    *   Function pour dire au joueur d'arreter de marcher   *
    ********************************************************/
    public void PlayerNotMove()
    {
        player.GetComponent<Animator>().SetFloat("DirectionY",    0f);
        player.GetComponent<Animator>().SetBool ("DoesWalk"  , false);
    }

    /*************************************************************************
    *   Function permettant de mettre la hauteur pile au niveau des traits   *
    **************************************************************************/
    public void SetFloor()
    {
        if(height < 1.25f)                          //1st floor
        {
            gauge.SetPosition(0);
            height = 1f;
        }
        if (height >= 1.25f && height < 1.85f)      //2nd floor
        {
            gauge.SetPosition(1);
            height = 1.5f;
        } 
        if (height >= 1.85f && height < 2.25f)      //3th floor
        {
            gauge.SetPosition(2);
            height = 2f;
        } 
        if (height >= 2.25f)                        //4th floor
        {
            gauge.SetPosition(3);
            height = 2.5f;
        } 
    }
}
