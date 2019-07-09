using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGoto : MonoBehaviour
{
    public int nbLabel;
    public int alea;
    public bool canTest;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            switch (alea)
            {
                case 3:
                    Test();
                    break;
            }
        }

    }

    public void IncreaseLabel()
    {
        nbLabel++;
        Test();
    }

    public void IncreaseLabelV2()
    {
        canTest = true;
        Test();
    }

    void Test()
    {
        if (nbLabel >= 3)
        {
            Debug.Log("Skip this");

        }
        
        if(canTest)
        {
            canTest = false;
            Debug.Log("TestLabel3");

        }
    }
}
