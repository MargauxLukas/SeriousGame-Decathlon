using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngletManager : MonoBehaviour
{
    public SpriteRenderer fillingRate;
    public SpriteRenderer recount;
    public SpriteRenderer repack;

    public void FillingRateOpen()
    {
        fillingRate.sortingOrder = 12;
        recount.sortingOrder = 11;
        repack.sortingOrder = 11;
    }

    public void RecountOpen()
    {
        fillingRate.sortingOrder = 11;
        recount.sortingOrder = 12;
        repack.sortingOrder = 11;
    }

    public void RepackOpen()
    {
        fillingRate.sortingOrder = 11;
        recount.sortingOrder = 11;
        repack.sortingOrder = 12;
    }
}
