using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitToGeneraView : MonoBehaviour
{
    public GameObject cameraGeneral;

    public void SwitchCameraState()
    {
        cameraGeneral.SetActive(!cameraGeneral.activeSelf);
    }
}
