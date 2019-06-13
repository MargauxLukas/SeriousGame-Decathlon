using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngletManager : MonoBehaviour
{
    public GameObject fillingRate;
    public GameObject recount;
    public GameObject repack;

    public void Start()
    {
        fillingRate.SetActive(true);
        recount.SetActive(false);
        repack.SetActive(false);
    }

    public void FillingRateOpen()
    {
        //Debug.Log("FillingRate Open");
        fillingRate.SetActive(true);
        recount.SetActive(false);
        repack.SetActive(false);
    }

    public void RecountOpen()
    {
        //Debug.Log("Recount Open");
        fillingRate.SetActive(false);
        recount.SetActive(true);
        repack.SetActive(false);
    }

    public void RepackOpen()
    {
        //Debug.Log("Repack Open");
        fillingRate.SetActive(false);
        recount.SetActive(false);
        repack.SetActive(true);
    }
}
