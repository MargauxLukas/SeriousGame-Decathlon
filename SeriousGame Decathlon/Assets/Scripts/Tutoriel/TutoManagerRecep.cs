using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManagerRecep : MonoBehaviour
{
    public static TutoManagerRecep instance;

    public DialogueManager dialogueManager;
    public GameObjectsManagerRecep gameObjectsManager;

    public List<Dialogue> listDialogues;

    [Header("Launch Dialogue & Phases")]
    public float phaseNum = 0;
    public int dialogNum = 0;
    public bool canPlayFirst = true;
    public bool canPlaySecond = false;
    public float interactionNum = 0;
    public string stringInteractionNum;
    /*public float chapterNum = 0;
    public string stringChapterNum;*/


    //Déplacement doigt
    private Vector3 fingerPosition;
    private Vector3 targetPosition;

    private float fingerSpeed;

    //SpriteMask Flèche
    public SpriteMask arrowSpriteMask;

    [Header("Flèche")]
    public float arrowRotation;
    public float arrowEulerAngleX;
    public float arrowEulerAngleY;
    public float arrowEulerAngleZ;

    void Awake()
    {
        if (instance == null) { instance = this  ; }
        else                  { Destroy(instance); }
    }

    private void Start()
    {
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
    }

    private void Update()
    {
        arrowSpriteMask = gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.arrowSpriteMask);
        arrowSpriteMask.sprite = gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrow).sprite;
    }

    public void DialogueIsFinished()
    {
        canPlayFirst = false;
        canPlaySecond = true;
        Manager(interactionNum);
    }

    public void Indications(Vector2 squareSpriteMask01Pos, Vector2 squareSpriteMask01Scale,
                            Vector2 squareSpriteMask02Pos, Vector2 squareSpriteMask02Scale,
                            Vector2 circleSpriteMask01Pos, Vector2 circleSpriteMask01Scale,
                            Vector2 circleSpriteMask02Pos, Vector2 circleSpriteMask02Scale,
                            Vector2 doigtClickPos, 
                            Vector3 doigtStayPos, Vector3 doigtStayTar, float doigtStaySpeed, bool doigtStayIsMoving, 
                            Vector2 arrowPos, float eulerAngX, float eulerAngY, float eulerAngZ)
    {
        if((squareSpriteMask01Pos != Vector2.zero && squareSpriteMask01Scale != Vector2.zero)
        || (circleSpriteMask01Pos != Vector2.zero && circleSpriteMask01Scale != Vector2.zero) 
        || (squareSpriteMask02Pos != Vector2.zero && squareSpriteMask02Scale != Vector2.zero) 
        || (circleSpriteMask02Pos != Vector2.zero && circleSpriteMask02Scale != Vector2.zero))
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        }

        if(squareSpriteMask01Pos != Vector2.zero && squareSpriteMask01Scale != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = squareSpriteMask01Pos;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = squareSpriteMask01Scale;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;
        }

        if(circleSpriteMask01Pos != Vector2.zero && circleSpriteMask01Scale != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask01).transform.localPosition = circleSpriteMask01Pos;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask01).transform.localScale = circleSpriteMask01Scale;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask01).enabled = true;
        }

        if (squareSpriteMask02Pos != Vector2.zero && squareSpriteMask02Scale != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localPosition = squareSpriteMask02Pos;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localScale = squareSpriteMask02Scale;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = true;
        }

        if (circleSpriteMask02Pos != Vector2.zero && circleSpriteMask02Scale != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask01).transform.localPosition = circleSpriteMask02Pos;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask01).transform.localScale = circleSpriteMask02Scale;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask01).enabled = true;
        }

        if (doigtClickPos != Vector2.zero)
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

            if (doigtStayIsMoving && doigtStaySpeed > 0 && doigtStayTar != doigtStayPos)
            {
                StartCoroutine(MoveDoigt(doigtStayPos, doigtStayTar, doigtStaySpeed));
            }
        }

        if(arrowPos != Vector2.zero && (eulerAngX != 0 || eulerAngY != 0 || eulerAngZ != 0))
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrow).transform.localPosition = arrowPos;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrow).transform.localRotation = Quaternion.Euler(eulerAngX, eulerAngY, eulerAngZ);
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrow).enabled = true;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrow).enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.arrowSpriteMask).enabled = true;
        }

        else
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;

            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtStaySpriteMask).enabled = false;

            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrow).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrow).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.arrowSpriteMask).enabled = false;
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

    IEnumerator NewPhase(float time)
    {
        yield return new WaitForSeconds(time);

        phaseNum++;
        canPlayFirst = true;
        canPlaySecond = false;

        Manager(4);
    }

    public void Manager(float interaction)
    {
        /*stringChapterNum = chapterNum.ToString();*/
        interactionNum = interaction;
        stringInteractionNum = interactionNum.ToString();

        Debug.Log(/*"Chapter : " + chapterNum + */"Interaction : " + interactionNum + " Phase : " + phaseNum);

        switch (/*stringChapterNum + "|" + */stringInteractionNum)
        {
            #region Chapter1 - Convoyeur

            #region 0 - Initial State
            case (/*"0 |"*/ "0"):
                switch (phaseNum)
                {
                    case (0):
                       Phase00();
                       break;
                }
                break;
                #endregion

            #region 1 - Interaction bouton On/Off
            case (/*"0 |"*/ "1"):
                switch (phaseNum)
                {
                    case (1):
                        Phase01();
                        break;
                }
                break;
                #endregion

            #region 2 - Interaction bouton Déplier
            case (/*"0 |"*/ "2"):
                switch (phaseNum)
                {
                    case (2):
                        Phase02();
                        break;
                }
                break;
                #endregion

            #region 3 - Convoyeur déplié au max.
            case (/*"0 |"*/ "3"):
                switch (phaseNum)
                {
                    case (3):
                        Phase03();
                        break;
                }
                break;
            #endregion

            #region 4 - Skip Automatique
            case (/*"0 |"*/ "4"):
                switch (phaseNum)
                {
                    case (4):
                        Phase04();
                        break;
                }
                break;
            #endregion

            #region 5 - Interaction bouton Monter
            case (/*"0 |"*/ "5"):
                switch (phaseNum)
                {
                    case (5):
                        Phase05();
                        break;
                }
                break;
            #endregion

            #region 6 - Jauge Convoyeur 3e étage
            case (/*"0 |"*/ "6"):
                switch (phaseNum)
                {
                    case (6):
                        Phase06();
                        break;
                }
                break;
            #endregion

                #endregion

        }
    }

    void Phase00()
    {
        Indications(new Vector2(0,0), new Vector2(0,0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(-5.57f, -3.67f), new Vector2(1,1),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(61.32f, -4.54f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false,
                    new Vector2(0, 0), 0, 0, 0);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.onOffButton).interactable = true;

        canPlayFirst = true;
        canPlaySecond = false;
        phaseNum++;
    }

    void Phase01()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.onOffButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(1.46f, -3.67f), new Vector2(1,1),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(68.13f, -4.32f, 0), new Vector3(0, 0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.unfoldButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase02()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0,0), new Vector2(0,0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false,
                    new Vector2(0, 0), 0, 0, 0);

        phaseNum++;
    }

    void Phase03()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.unfoldButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
           Indications(new Vector2(5.1f, 1.24f), new Vector2(0.98f, 1.79f),
                       new Vector2(0, 0), new Vector2(0, 0),
                       new Vector2(0, 0), new Vector2(0, 0),
                       new Vector2(0, 0), new Vector2(0, 0),
                       new Vector2(0, 0),
                       new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false,
                       new Vector2(0, 0), 0, 0, 0);

            StartCoroutine(NewPhase(1f));
        }
    }

    void Phase04()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(5.1f, 1.24f), new Vector2(0.98f, 1.79f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(-3.21f, -3.67f), new Vector2(1,1),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(63.44f, -4.32f, 0), new Vector3(0, 0, 0), 0, false,
                        new Vector2(72.2f, 1.21f), 0, 0, 270);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.ascendButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase05()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false,
                    new Vector2(72.2f, 1.21f), 0, 0, 270);

        phaseNum++;
    }

    void Phase06()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.ascendButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-2.87f, -8.75f), new Vector2(0.84f, 0.9f),
                        new Vector2(/*Convoyeur Pos*/), new Vector2(/*Convoyeur Scale*/),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(63.91f, 9.27f, 0), new Vector3(/*Convoyeur Pos*/), 4f, true,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;
        }
    }

    void Phase07()
    {

    }
}
