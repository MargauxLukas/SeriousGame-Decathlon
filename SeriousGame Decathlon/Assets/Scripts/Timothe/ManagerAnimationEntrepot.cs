using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAnimationEntrepot : MonoBehaviour
{
    public List<GameObject> marcheList;
    public List<GameObject> specialList;
    public List<bool> isActive;

    public GameObject marcheAdrien;
    public GameObject marcheAmelie;
    public GameObject marcheAnthony;
    public GameObject marcheAurelie;
    public GameObject marcheBahkta;
    public GameObject marcheBenjamin;
    public GameObject marcheDavid;
    public GameObject marcheDorothee;
    public GameObject marcheEmmanuel;
    public GameObject marcheJulien;
    public GameObject marcheLaetitia;
    public GameObject marcheLorris;
    public GameObject marcheLuc;
    public GameObject marcheManika;
    public GameObject marcheMarine;
    public GameObject marcheMarjorie;
    public GameObject marcheStephanie;
    public GameObject marcheThierry;
    public GameObject marcheVeronique;
    public GameObject marcheYannick;

    public GameObject Adrien;
    public GameObject Amelie;
    public GameObject Anthony;
    public GameObject Bahkta;
    public GameObject Benjamin;
    public GameObject David;
    public GameObject Dorothee;
    public GameObject Emmanuel;
    public GameObject Julien;
    public GameObject Laetitia;
    public GameObject Lorris;
    public GameObject Luc;
    public GameObject Manika;
    public GameObject Marine;
    public GameObject Marjorie;
    public GameObject Stephanie;
    public GameObject Thierry;
    public GameObject Veronique;
    public GameObject Yannick;

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
