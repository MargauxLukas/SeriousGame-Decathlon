using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPersoChoisit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Animator>()!=null && ManagerPersoChoisit.instance != null)
        {
            GetComponent<Animator>().runtimeAnimatorController = ManagerPersoChoisit.instance.animatorPersonnages[ManagerPersoChoisit.instance.Personnage];
        }
    }
}
