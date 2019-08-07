using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPersoChoice : MonoBehaviour
{
    public int numDuPerso;

    public Animator anim;

    public void StartAnim()
    {
        anim.SetBool("isActive", true);
        Debug.Log("TestActive" + gameObject.name);
    }

    public void StopAnim()
    {
        anim.SetBool("isActive", false);
        Debug.Log("TestDesactive" + gameObject.name);
    }
}
