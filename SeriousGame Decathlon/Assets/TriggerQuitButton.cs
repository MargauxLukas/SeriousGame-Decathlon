using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerQuitButton : MonoBehaviour
{
    public GameObjectsManagerRecep gameObjectsManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Colis" && TutoManagerRecep.instance != null && (TutoManagerRecep.instance.phaseNum == 22 || TutoManagerRecep.instance.phaseNum == 23))
        {
            gameObjectsManager.quitButton.GetComponent<Button>().interactable = true;
        }
    }
}
