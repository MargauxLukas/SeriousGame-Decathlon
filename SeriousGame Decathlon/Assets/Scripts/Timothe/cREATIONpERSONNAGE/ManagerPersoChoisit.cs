using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPersoChoisit : MonoBehaviour
{
    public int Personnage;

    public List<RuntimeAnimatorController> animatorPersonnages;

    public static ManagerPersoChoisit instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

}
