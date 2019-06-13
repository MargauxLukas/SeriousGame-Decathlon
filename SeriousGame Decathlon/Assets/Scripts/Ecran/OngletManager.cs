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
        /*fillingRate.sortingOrder = 12;
        recount.sortingOrder = 11;
        repack.sortingOrder = 11;*/
        Debug.Log("FillingRate Open");
        fillingRate.SetActive(true);
        recount.SetActive(false);
        repack.SetActive(false);
    }

    public void RecountOpen()
    {
        /*fillingRate.sortingOrder = 11;
        recount.sortingOrder = 12;
        repack.sortingOrder = 11;*/

        Debug.Log("Recount Open");
        fillingRate.SetActive(false);
        recount.SetActive(true);
        repack.SetActive(false);
    }

    public void RepackOpen()
    {
        /* fillingRate.sortingOrder = 11;
         recount.sortingOrder = 11;
         repack.sortingOrder = 12;*/

        Debug.Log("Repack Open");
        fillingRate.SetActive(false);
        recount.SetActive(false);
        repack.SetActive(true);
    }
}
