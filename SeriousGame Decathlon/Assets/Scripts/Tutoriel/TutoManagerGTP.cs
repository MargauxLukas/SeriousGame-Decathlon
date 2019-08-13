using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManagerGTP : MonoBehaviour
{
    public static TutoManagerGTP instance;

    public DialogueManager dialogueManager;
    public GameObjectsManagerRecep gameObjectsManager;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(instance); }
    }
}
