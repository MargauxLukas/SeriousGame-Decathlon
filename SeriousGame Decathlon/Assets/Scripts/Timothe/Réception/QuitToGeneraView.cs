using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitToGeneraView : MonoBehaviour
{
    public GameObject cameraGeneral;

    private bool doesTouch;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touchObject();
            if (doesTouch)
            {
                SwitchCameraState();
                doesTouch = false;
            }
        }
    }

    void touchObject() //Fonction permettant de détecter si le joueur touche l'objet
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.name == gameObject.name)
            {
                Debug.Log("Test");
                doesTouch = true;
            }
        }
    }
    public void SwitchCameraState()
    {
        cameraGeneral.SetActive(!cameraGeneral.activeSelf);
    }
}
