using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManagerRecep : MonoBehaviour
{
    public static TutoManagerRecep instance;

    public DialogueManager dialogueManager;
    public GameObjectsManagerRecep gameObjectsManager;

    public float phaseNum = 0;
    public int dialogNum = 0;

    [Header("Launch Dialogue & Phases")]
    public bool canPlayFirst = true;
    public bool canPlaySecond = false;
    public float interactionNum = 0;

    //Déplacement doigt
    private Vector3 fingerPosition;
    private Vector3 targetPosition;

    private float fingerSpeed;

    public float arrowRotation;
    public float arrowEulerAngleX;
    public float arrowEulerAngleY;
    public float arrowEulerAngleZ;

    void Start()
    {
        if (instance == null) { instance = this  ; }
        else                  { Destroy(instance); }
    }

    public void DialogueIsFinished()
    {
        canPlayFirst = false;
        canPlaySecond = true;
        Manager(interactionNum);
    }

    public void Indications(Vector2 blackScreenPos, 
                            Vector2 squareSpriteMaskPos, 
                            Vector2 circleSpriteMaskPos, 
                            Vector2 doigtClickPos, 
                            Vector3 doigtStayPos, Vector3 doigtStayTar, float doigtStaySpeed, bool doigtStayIsMoving, 
                            Vector2 arrowPos, float eulerAngX, float eulerAngY, float eulerAngZ)
    {
        if(blackScreenPos != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.blackScreen).transform.localPosition = blackScreenPos;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        }

        if(squareSpriteMaskPos != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask).transform.localPosition = squareSpriteMaskPos;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask).enabled = true;
        }

        if(circleSpriteMaskPos != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask).transform.localPosition = circleSpriteMaskPos;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask).enabled = true;
        }

        if(doigtClickPos != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = doigtClickPos;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = true;
        }

        if (doigtStayPos != Vector3.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = doigtStayPos;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtStaySpriteMask).enabled = true;

            if (doigtStayIsMoving)
            {
                StartCoroutine(MoveDoigt(doigtStayPos, doigtStayTar, doigtStaySpeed));
            }
        }

        if(arrowPos != Vector2.zero && (eulerAngX != 0 || eulerAngY != 0 || eulerAngZ != 0))
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrow).transform.localPosition = arrowPos;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrow).transform.localRotation = Quaternion.Euler(eulerAngX, eulerAngY, eulerAngZ);
        }
    }

    IEnumerator MoveDoigt(Vector3 fingerPos, Vector3 targetPos, float fingerSpeed)
    {
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition += (targetPos - fingerPos) * Time.fixedDeltaTime * fingerSpeed;

        if (Vector3.Distance(gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition, targetPosition) <= 0.2f)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = fingerPosition;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", true);
        }
        else
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", false);
        }

        yield return new WaitForSeconds(Time.fixedDeltaTime);
        StartCoroutine(MoveDoigt(fingerPos, targetPos, fingerSpeed));
    }

    public void Manager(float interaction)
    {
        interactionNum = interaction;
        Debug.Log("Interaction : " + interactionNum + " Phase : " + phaseNum);
        switch (interaction)
        {

        }
    }
}
