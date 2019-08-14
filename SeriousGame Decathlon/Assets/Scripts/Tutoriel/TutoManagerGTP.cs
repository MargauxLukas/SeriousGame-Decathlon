using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManagerGTP : MonoBehaviour
{
    public static TutoManagerGTP instance;

    public DialogueManager dialogueManager;
    public GameObjectsManagerGTP gameObjectsManager;

    public List<Dialogue> listDialogues;

    [Header("Launch Dialogue & Phases")]
    public float phaseNum = 0;
    public float interactionNum = 0;
    public int dialogNum = 0;
    public bool canPlayFirst = true;
    public bool canPlaySecond = false;

    public float remplissageColisTuto;

    //Déplacement doigt
    private Vector3 fingerStartPosition;
    private bool checkpointMoveDoigt = false;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(instance); }
    }

    private void Start()
    {
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
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
                            Vector3 doigtStayPos, Vector3 doigtStayTar, float doigtStaySpeed, bool doigtStayIsMoving)
    {
        if ((squareSpriteMask01Pos != Vector2.zero && squareSpriteMask01Scale != Vector2.zero)
        || (circleSpriteMask01Pos != Vector2.zero && circleSpriteMask01Scale != Vector2.zero)
        || (squareSpriteMask02Pos != Vector2.zero && squareSpriteMask02Scale != Vector2.zero)
        || (circleSpriteMask02Pos != Vector2.zero && circleSpriteMask02Scale != Vector2.zero))
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        }

        if (squareSpriteMask01Pos != Vector2.zero && squareSpriteMask01Scale != Vector2.zero)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = squareSpriteMask01Pos;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = squareSpriteMask01Scale;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;
        }

        if (circleSpriteMask01Pos != Vector2.zero && circleSpriteMask01Scale != Vector2.zero)
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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask02).transform.localPosition = circleSpriteMask02Pos;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask02).transform.localScale = circleSpriteMask02Scale;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask02).enabled = true;
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

        if (squareSpriteMask01Pos == Vector2.zero && squareSpriteMask01Scale == Vector2.zero
        && circleSpriteMask01Pos == Vector2.zero && circleSpriteMask01Scale == Vector2.zero
        && squareSpriteMask02Pos == Vector2.zero && squareSpriteMask02Scale == Vector2.zero
        && circleSpriteMask02Pos == Vector2.zero && circleSpriteMask02Scale == Vector2.zero
        && doigtClickPos == Vector2.zero
        && doigtStayPos == Vector3.zero && doigtStayTar == Vector3.zero && doigtStaySpeed <= 0 && !doigtStayIsMoving)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;

            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = false;

            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask02).enabled = false;

            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtStaySpriteMask).enabled = false;
            StopAllCoroutines();
        }
    }

    IEnumerator MoveDoigt(Vector3 fingerPos, Vector3 targetPos, float fingerSpeed)
    {
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition += (targetPos - fingerPos) * Time.fixedDeltaTime * fingerSpeed;
        
        if (Vector3.Distance(gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition, targetPos) <= 0.2f)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = fingerPos;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", true);
        }
        else
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", false);
        }

        yield return new WaitForSeconds(Time.fixedDeltaTime);
        StartCoroutine(MoveDoigt(fingerPos, targetPos, fingerSpeed));
    }

    IEnumerator MoveDoigtCheckpoint(Vector3 fingerPos, Vector3 checkpointPos, Vector3 targetPos, float fingerSpeed)
    {
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition += (checkpointPos - fingerPos) * Time.fixedDeltaTime * fingerSpeed;

        if(!checkpointMoveDoigt && Vector3.Distance(gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition, checkpointPos) <= 0.2f)
        {
            fingerStartPosition = fingerPos;
            fingerPos = checkpointPos;
            checkpointMoveDoigt = true;

            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveDoigtCheckpoint(fingerPos, checkpointPos, targetPos, fingerSpeed));
        }

        if(checkpointMoveDoigt && Vector3.Distance(gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition, targetPos) <= 0.2f)
        {
            fingerPos = fingerStartPosition;
            checkpointMoveDoigt = false;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = fingerPos;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", true);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
            StartCoroutine(MoveDoigtCheckpoint(fingerPos, checkpointPos, targetPos, fingerSpeed));
        }
        else
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", false);
        }
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
        interactionNum = interaction;

        Debug.Log("Interaction : " + interactionNum + " Phase : " + phaseNum + " Dialogue : " + dialogNum);

        switch (interactionNum)
        {
            #region 0 - Initial State
            case (0):
                switch (phaseNum)
                {
                    case (0):
                        Phase00();
                        break;
                }
                break;
            #endregion

            #region 1 - Colis vide apairé
            case (1):
                switch (phaseNum)
                {
                    case (1):
                        Phase01();
                        break;

                    case (11):
                        Phase11();
                        break;
                }
                break;
            #endregion

            #region 2 - Interaction bouton Loupe
            case (2):
                switch (phaseNum)
                {
                    case (5):
                        Phase05();
                        break;
                }
                break;
            #endregion

            #region 3 - Interaction bouton Back
            case (3):
                switch (phaseNum)
                {
                    case (6):
                        Phase06();
                        break;
                }
                break;
            #endregion

            #region 4 - Skip Automatique
            case (4):
                switch (phaseNum)
                {
                    case (2):
                        Phase02();
                        break;

                    case (3):
                        Phase03();
                        break;

                    case (4):
                        Phase04();
                        break;

                    case (12):
                        Phase12();
                        break;

                    case (13):
                        Phase13();
                        break;
                }
                break;
            #endregion

            #region 5 - Colis source ouvert
            case (5):
                switch (phaseNum)
                {
                    case (7):
                        Phase07();
                        break;

                    case (14):
                        Phase14();
                        break;

                    case (18):
                        Phase18();
                        break;
                }
                break;
            #endregion

            #region 6 - Bon nombre articles dans le colis
            case (6):
                switch (phaseNum)
                {
                    case (8):
                        Phase08();
                        break;

                    case (15):
                        Phase15();
                        break;

                    case (19):
                        Phase19();
                        break;
                }
                break;
            #endregion

            #region 7 - Colis source fermé
            case (7):
                switch (phaseNum)
                {
                    case (9):
                        Phase09();
                        break;

                    case (16):
                        Phase16();
                        break;

                    case (20):
                        Phase20();
                        break;
                }
                break;
            #endregion

            #region 8 - Interaction bouton Push
            case (8):
                switch (phaseNum)
                {
                    case (10):
                        Phase10();
                        break;

                    case (17):
                        Phase17();
                        break;
                }
                break;
                #endregion
        }
    }

    void Phase00()
    {
        Indications(new Vector2(-3.64f, 4.14f), new Vector2(1.25f, 0.97f),
                    new Vector2(-3.72f, -2.51f), new Vector2(1.25f, 1.25f),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(62.97f, 3.42f, 900f), new Vector3(62.97f, -2.9f, 900f), 4, true);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.bac1).enabled = true;

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
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.bac1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(8.2f, 3.29f), new Vector2(3.97f, 2.34f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            StartCoroutine(NewPhase(1f));
        }
    }

    void Phase02()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(8.325f, 4.42f), new Vector2(1, 1),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            StartCoroutine(NewPhase(1f));
        }
    }

    void Phase03()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(8.37f, 2.61f), new Vector2(2.52f, 0.69f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            StartCoroutine(NewPhase(1f));
        }
    }

    void Phase04()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(6.42f, 2.62f), new Vector2(0.8f, 0.8f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(73.08f, 1.87f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe1Button).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase05()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe1Button).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(4.78f, 1.37f), new Vector2(0.49f, 0.34f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(71.45f, 0.61f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe1Button).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
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
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe1Button).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(5.29f, -2.54f), new Vector2(1.28f, 1.12f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(72.07f, -3.35f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
    
    void Phase07()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(72.42f, -0.61f, 900), new Vector3(62.85f, -3.02f, 900), 4, true);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = true;
            remplissageColisTuto = 3;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase08()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = false;
            remplissageColisTuto = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(72.07f, -3.35f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase09()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(-2.8f, 2.4f), new Vector2(0.5f, 0.5f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(63.89f, 1.64f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase10()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(62.97f, 3.42f, 900f), new Vector3(65.95f, -2.9f, 900), 4, true);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.bac2).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase11()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.bac2).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(8.34f, 2.62f), new Vector2(0.8f, 0.8f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            StartCoroutine(NewPhase(1f));
        }
    }

    void Phase12()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(7.12f, -4.38f), new Vector2(0.67f, 0.77f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            StartCoroutine(NewPhase(1f));
        }
    }

    void Phase13()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(75.09f, -3.35f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase14()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtStaySpriteMask).enabled = true;

            StartCoroutine(MoveDoigtCheckpoint(new Vector3(73.88f, -0.61f, 900), new Vector3 (73.6f, -4.75f, 900), new Vector3(65.6f, -3.02f, 900), 4));

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = true;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide2).enabled = true;

            remplissageColisTuto = 4;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase15()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtStaySpriteMask).enabled = false;

            StopAllCoroutines();

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide2).enabled = false;

            remplissageColisTuto = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase16()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(66.75f, 1.64f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton2).interactable = true;

        phaseNum++;
    }

    void Phase17()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton2).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(72.07f, -3.35f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase18()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = true;

        remplissageColisTuto = 4;

        phaseNum++;
    }

    void Phase19()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = false;
        remplissageColisTuto = 0;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

        phaseNum++;
    }

    void Phase20()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(/*Position Bouton Push*/),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);
    }
}
