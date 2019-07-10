using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyeurManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;

    public float height = 1f;
    public float minHeight = 1f;
    public float maxHeight = 3f;
    public float speed = 2f;
    public bool isOn;

    private void Update()
    {
        
    }

    public void MoveZ(string direction)
    {
        Debug.Log(direction);
        if(direction.Equals("up"))
        {
            if(height <= maxHeight)
            {
                height = height + 0.002f;
            }
        }
        else
        {
            if (height >= minHeight)
            {
                height = height - 0.002f;
            }
        }
    }

    public void MoveY(string direction)
    {
        if (direction.Equals("replier"))
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 0.01f, camera.transform.position.z);
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.01f, player.transform.position.z);
            player.GetComponent<Animator>().SetFloat("DirectionY", 1f);
            player.GetComponent<Animator>().SetBool("DoesWalk", true);
        }
        else
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 0.01f, camera.transform.position.z);
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.01f, player.transform.position.z);
            player.GetComponent<Animator>().SetFloat("DirectionY", -1f);
            player.GetComponent<Animator>().SetBool("DoesWalk", true);
        }
    }

    public void PlayerNotMove()
    {
        player.GetComponent<Animator>().SetFloat("DirectionY", 0f);
        player.GetComponent<Animator>().SetBool("DoesWalk", false);
    }
}
