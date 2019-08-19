using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAnimationEntrepot : MonoBehaviour
{
    public List<GameObject> marcheList;
    public List<GameObject> specialList;
    public List<bool> isActive;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i ++)
        {
            int rngNb = Random.Range(0, 20);
            int compteur = 0;
            while (isActive[rngNb] && compteur < 25) 
            {
                compteur++;
                rngNb = Random.Range(0, 20);
            }
            marcheList[rngNb].SetActive(true);
            isActive[rngNb] = true;
        }

        for(int i = 0; i < isActive.Count; i++)
        {
            if(!isActive[i])
            {
                specialList[i].SetActive(true);
            }
        }
    }
}
