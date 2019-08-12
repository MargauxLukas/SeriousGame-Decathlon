using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttentreInstance : MonoBehaviour
{
    public bool haveReponse;

    public static TestAttentreInstance instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        StartCoroutine(StartingWait());
    }

    private IEnumerator StartingWait()
    {
        yield return new WaitForSeconds(10f);
        haveReponse = true;
    }

    public int ReturnInt(int nbReturn, bool isFinal)
    {
        if(isFinal)
        {
            return nbReturn;
        }
        else
        {
            StartCoroutine(ReturnIntCorout(nbReturn));
        }
        return -1;
    }

    public int ReturnIntSecond(int nbReturn)
    {
        for(int i = 0; i < 30000; i++)
        {

        }
        return nbReturn + 1;
    }

    public IEnumerator ReturnIntCorout(int intToReturn)
    {
        if (haveReponse)
        {
            ReturnInt(intToReturn+1,true);
        }
        else
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            StartCoroutine(ReturnIntCorout(intToReturn));
        }
    }

}
