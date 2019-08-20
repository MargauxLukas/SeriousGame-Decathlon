using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneWithoutButton : MonoBehaviour
{
    public BoutonChangementScene loadScene;
    public int newScene;
    // Start is called before the first frame update
    void Start()
    {
        loadScene.LoadNewScene(newScene);
    }
}
