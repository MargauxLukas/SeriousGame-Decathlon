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

    [Header("Miscellaneous")]
    public float remplissageColisTuto;
    public float correctySourceQtyWrongInputValue;
    public float correctySourceQtyMissingInputValue;
    public string correctPickedQtyInputValue;
    public string remainQtyInputValue;
    public string consoleInputValue;
    public Console console;

    //Déplacement doigt
    private Vector3 fingerStartPosition;
    private bool checkpointMoveDoigt = false;
    private bool fingerActive = false;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(instance); }
    }

    private void Start()
    {
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;
    }

    private void Update()
    {
        Debug.Log("Real time PhaseNum : " + phaseNum);
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
            fingerActive = true;
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
            fingerActive = false;

            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtStaySpriteMask).enabled = false;
            StopAllCoroutines();
        }
    }

    IEnumerator MoveDoigt(Vector3 fingerPos, Vector3 targetPos, float fingerSpeed)
    {
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition += (targetPos - fingerPos) * Time.fixedDeltaTime * fingerSpeed;

        if (phaseNum == 0 || phaseNum == 1 || phaseNum == 10 || phaseNum == 11)
        {
            if (Vector3.Distance(gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition, targetPos) <= 0.3f)
            {
                gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = fingerPos;
                gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", true);
            }
            else
            {
                gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", false);
            }
        }
        else if(phaseNum == 7 || phaseNum == 8)
        {
            if (Vector3.Distance(gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition, targetPos) <= 0.4f)
            {
                gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = fingerPos;
                gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", true);
            }
            else
            {
                gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", false);
            }
        }
        else
        {
            if (Vector3.Distance(gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition, targetPos) <= 0.2f)
            {
                gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = fingerPos;
                gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", true);
            }
            else
            {
                gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", false);
            }
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

    void CorrectFingerPos()
    {
        if (fingerActive)
        {
            Debug.Log("fingerActive");
            gameObjectsManager.doigtClick.transform.position += new Vector3(0, 0, 900);
        }
    }

    public void Manager(float interaction)
    {
        interactionNum = interaction;

        Debug.Log("Interaction : " + interactionNum + " | Phase : " + phaseNum + " | Dialogue : " + dialogNum);

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

                    case (24):
                        Phase24();
                        break;

                    case (66):
                        Phase66();
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

                    case (52):
                        Phase52();
                        break;

                    case (76):
                        Phase76();
                        break;
                }
                break;
            #endregion

            #region 3 - Interaction bouton Back liste PickTU
            case (3):
                switch (phaseNum)
                {
                    case (6):
                        Phase06();
                        break;

                    case (56):
                        Phase56();
                        break;

                    case (78):
                        Phase78();
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

                    case (35):
                        Phase35();
                        break;

                    case (40):
                        Phase40();
                        break;

                    case (72):
                        Phase72();
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

                    case (25):
                        Phase25();
                        break;

                    case(34):
                        Phase34();
                        break;

                    case (39):
                        Phase39();
                        break;

                    case (43):
                        Phase43();
                        break;

                    case (58):
                        Phase58();
                        break;

                    case (65):
                        Phase65();
                        break;

                    case (70):
                        Phase70();
                        break;

                    case (81):
                        Phase81();
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

                    case (26):
                        Phase26();
                        break;

                    case (41):
                        Phase41();
                        break;

                    case (44):
                        Phase44();
                        break;

                    case (59):
                        Phase59();
                        break;

                    case (67):
                        Phase67();
                        break;

                    case (71):
                        Phase71();
                        break;

                    case (82):
                        Phase82();
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

                    case (27):
                        Phase27();
                        break;

                    case (36):
                        Phase36();
                        break;

                    case (45):
                        Phase45();
                        break;

                    case (60):
                        Phase60();
                        break;

                    case (68):
                        Phase68();
                        break;

                    case (79):
                        Phase79();
                        break;

                    case (83):
                        Phase83();
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

                    case (21):
                        Phase21();
                        break;

                    case (23):
                        Phase23();
                        break;

                    case (33):
                        Phase33();
                        break;

                    case (42):
                        Phase42();
                        break;

                    case (57):
                        Phase57();
                        break;

                    case (61):
                        Phase61();
                        break;

                    case (64):
                        Phase64();
                        break;

                    case (69):
                        Phase69();
                        break;

                    case (80):
                        Phase80();
                        break;

                    case (84):
                        Phase84();
                        break;
                }
                break;
            #endregion

            #region 9 - Interaction bouton OK écran RemainingQty
            case (9):
                switch (phaseNum)
                {
                    case (22):
                        Phase22();
                        break;

                    case (63):
                        Phase63();
                        break;
                }
                break;
            #endregion

            #region 10 - Interaction bouton Anomaly
            case (10):
                switch (phaseNum)
                {
                    case (28):
                        Phase28();
                        break;

                    case (37):
                        Phase37();
                        break;

                    case (46):
                        Phase46();
                        break;
                }
                break;
            #endregion

            #region 11 - Interaction bouton CorrectSourceQty
            case (11):
                switch (phaseNum)
                {
                    case (29):
                        Phase29();
                        break;

                    case (47):
                        Phase47();
                        break;
                }
                break;
            #endregion

            #region 12 - InputField Wrong good value
            case (12):
                switch (phaseNum)
                {
                    case (30):
                        Phase30();
                        break;

                    case (49):
                        Phase49();
                        break;
                }
                break;
            #endregion

            #region 13 - Interaction bouton Confirm écran CorrectSourceQty
            case (13):
                switch (phaseNum)
                {
                    case (31):
                        Phase31();
                        break;

                    case (50):
                        Phase50();
                        break;
                }
                break;
            #endregion

            #region 14 - Interaction bouton Back écran CorrectSourceQty
            case (14):
                switch (phaseNum)
                {
                    case (32):
                        Phase32();
                        break;

                    case (51):
                        Phase51();
                        break;
                }
                break;
            #endregion

            #region 15 - Interaction bouton Wrong Product
            case (15):
                switch (phaseNum)
                {
                    case (38):
                        Phase38();
                        break;
                }
                break;
            #endregion

            #region 16 - InputField Missing good value
            case (16):
                switch (phaseNum)
                {
                    case (48):
                        Phase48();
                        break;
                }
                break;
            #endregion

            #region 17 - Interaction bouton CorrectPickedQty
            case (17):
                switch (phaseNum)
                {
                    case (53):
                        Phase53();
                        break;
                }
                break;
            #endregion

            #region 18 - InputField CorrectPickedQty good value
            case (18):
                switch (phaseNum)
                {
                    case (54):
                        Phase54();
                        break;
                }
                break;
            #endregion

            #region 19 - Interaction bouton OK écran CorrectPickedQty
            case (19):
                switch (phaseNum)
                {
                    case (55):
                        Phase55();
                        break;
                }
                break;
            #endregion

            #region 20 - InputField RemainingQty good value
            case (20):
                switch (phaseNum)
                {
                    case (62):
                        Phase62();
                        break;
                }
                break;
            #endregion

            #region 21 - Interaction bouton - console
            case (21):
                switch (phaseNum)
                {
                    case (73):
                        Phase73();
                        break;
                }
                break;
            #endregion

            #region 22 - InputField Console good value
            case (22):
                switch (phaseNum)
                {
                    case (74):
                        Phase74();
                        break;
                }
                break;
            #endregion

            #region 23 - Interaction bouton Valider console
            case (23):
                switch (phaseNum)
                {
                    case (75):
                        Phase75();
                        break;
                }
                break;
            #endregion

            #region 24 - Interaction bouton Close PickTU
            case (24):
                switch (phaseNum)
                {
                    case (77):
                        Phase77();
                        break;
                }
                break;
            #endregion

            #region 25 - Interaction bouton Quitter
            case (25):
                switch (phaseNum)
                {
                    case (85):
                        Phase85();
                        break;
                }
                break;
                #endregion
        }
    }

    #region Colis source 1
    void Phase00()
    {
        Indications(new Vector2(-3.72f, -2.51f), new Vector2(1.25f, 0.97f),
                    new Vector2(-3.64f, 4.14f), new Vector2(1.25f, 1.25f),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(62.97f, 3.42f, 900f), new Vector3(62.97f, -2.9f, 900f), 4, true);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.bac1).enabled = true;

        remplissageColisTuto = 100;

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

            CorrectFingerPos();

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe1Button).interactable = true;

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

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe1Button).interactable = false;

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

            CorrectFingerPos();

            gameObjectsManager.GameObjectToButton(gameObjectsManager.listPickTUBackButton).interactable = true;

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

            gameObjectsManager.GameObjectToButton(gameObjectsManager.listPickTUBackButton).interactable = false;

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

            CorrectFingerPos();

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
            remplissageColisTuto = 0.3f;

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
            remplissageColisTuto = 100;

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

            CorrectFingerPos();

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

            CorrectFingerPos();

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
    #endregion

    #region Colis source 2
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

            CorrectFingerPos();

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

        CorrectFingerPos();

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton2).interactable = true;

        phaseNum++;
    }
    #endregion

    #region Colis source 3
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
                    new Vector2(63.89f, 1.64f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = true;

        phaseNum++;
    }

    void Phase21()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = false;

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
            Indications(new Vector2(10.78f, 1.39f), new Vector2(1.4f, 0.36f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(77.26f, 0.58f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.remainingQtyOkButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase22()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.remainingQtyOkButton).interactable = false;

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
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
    #endregion

    #region Colis source 4
    void Phase23()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.bac3).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase24()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.bac3).enabled = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(75.09f, -3.35f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = true;

        phaseNum++;
    }

    void Phase25()
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
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide3).enabled = true;

            remplissageColisTuto = 4;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase26()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide3).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = true;

            remplissageColisTuto = 0;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase27()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(8.32f, 1.39f), new Vector2(1.2f, 0.36f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(75.75f, 0.58f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.anomalyButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase28()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.anomalyButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(5.33f, 3.21f), new Vector2(0.93f, 0.92f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(72.33f, 2.39f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase29()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(10.16f, 2.27f), new Vector2(0.4f, 0.4f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(77, 1.31f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyWrongPlusButton).interactable = true;

            correctySourceQtyWrongInputValue = 1;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase30()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyWrongPlusButton).interactable = false;

            correctySourceQtyWrongInputValue = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(7.31f, 1.38f), new Vector2(1.3f, 0.35f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(74.61f, 0.56f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyConfirmButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase31()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyConfirmButton).interactable = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(71.53f, 0.56f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyBackButton).interactable = true;

        phaseNum++;
    }

    void Phase32()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyBackButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(69.67f, 1.64f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton3).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
    #endregion

    #region Colis source 5
    void Phase33()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton3).interactable = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(72.07f, -3.35f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

        phaseNum++;
    }

    void Phase34()
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
            Indications(new Vector2(10.7f, 4.34f), new Vector2(1, 0.75f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            StartCoroutine(NewPhase(1));
        }
    }

    void Phase35()
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
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase36()
    {
        if (canPlayFirst)
        {
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
                        new Vector2(75.75f, 0.58f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.anomalyButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase37()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.anomalyButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(76.22f, 2.47f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.wrongProductButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
    #endregion

    #region Colis source 6
    void Phase38()
    {
        gameObjectsManager.GameObjectToButton(gameObjectsManager.wrongProductButton).interactable = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(75.09f, -3.35f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = true;

        phaseNum++;
    }

    void Phase39()
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
            Indications(new Vector2(10.7f, 4.34f), new Vector2(1, 0.75f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            StartCoroutine(NewPhase(1));
        }
    }

    void Phase40()
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
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = true;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide2).enabled = true;

            remplissageColisTuto = 2;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase41()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide2).enabled = false;

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
                        new Vector2(66.75f, 1.64f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton2).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
    #endregion

    #region Colis source 7
    void Phase42()
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
    }

    void Phase43()
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
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = true;

            remplissageColisTuto = 2;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase44()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = false;

            remplissageColisTuto = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
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
    }

    void Phase45()
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
                        new Vector2(75.75f, 0.58f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.anomalyButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase46()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.anomalyButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(72.33f, 2.39f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase47()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(77f, 2.22f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyMissingPlusButton).interactable = true;

            correctySourceQtyMissingInputValue = 1;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase48()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyMissingPlusButton).interactable = false;

        correctySourceQtyMissingInputValue = 0;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(77f, 1.31f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyWrongPlusButton).interactable = true;

        correctySourceQtyWrongInputValue = 1;

        phaseNum++;
    }

    void Phase49()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyWrongPlusButton).interactable = false;

            correctySourceQtyWrongInputValue = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(7.31f, 1.38f), new Vector2(1.3f, 0.35f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(74.61f, 0.56f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyConfirmButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase50()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyConfirmButton).interactable = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(71.53f, 0.56f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyBackButton).interactable = true;

        phaseNum++;
    }

    void Phase51()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctSourceQtyBackButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(73.08f, 1.87f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe1Button).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase52()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe1Button).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(77.35f, 0.61f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctPickedQtyButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase53()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctPickedQtyButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(72.78f, 2.45f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctPickedQtyMinusButton).interactable = true;

            correctPickedQtyInputValue = "2";

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase54()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctPickedQtyMinusButton).interactable = false;

            correctPickedQtyInputValue = null;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(78.1f, 0.65f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctPickedQtyOkButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase55()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.correctPickedQtyOkButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(71.45f, 0.61f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.listPickTUBackButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase56()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.listPickTUBackButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase57()
    {
        if (canPlayFirst)
        {
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
                        new Vector2(72.07f, -3.35f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
    #endregion

    #region Colis source 8
    void Phase58()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide3).enabled = true;

        remplissageColisTuto = 3;

        phaseNum++;
    }

    void Phase59()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide3).enabled = false;

            remplissageColisTuto = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase60()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(69.67f, 1.64f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton3).enabled = true;

        phaseNum++;
    }

    void Phase61()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton3).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(77.35f, 2.54f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.remainingQtyPlusButton).enabled = true;

            remainQtyInputValue = "1";

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase62()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.remainingQtyPlusButton).enabled = false;

            remainQtyInputValue = null;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(63.89f, 1.64f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.remainingQtyOkButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase63()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.remainingQtyOkButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton3).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase64()
    {
        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton3).interactable = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(75.09f, -3.35f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = true;

        phaseNum++;
    }
    #endregion

    #region Colis source 9
    void Phase65()
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
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.bac1).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
    
    void Phase66()
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
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = true;

            remplissageColisTuto = 6;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase67()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = false;

        remplissageColisTuto = 0;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(72.07f, -3.35f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

        phaseNum++;
    }

    void Phase68()
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
                        new Vector2(63.89f, 1.64f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase69()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(72.07f, -3.35f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource2).enabled = true;

        phaseNum++;
    }
    #endregion

    #region Colis source 10
    void Phase70()
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
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide3).enabled = true;

            remplissageColisTuto = 1;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase71()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide3).enabled = false;

            remplissageColisTuto = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(2.91f, -2.63f), new Vector2(0.22f, 0.85f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            StartCoroutine(NewPhase(1));
        }
    }

    void Phase72()
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
            Indications(new Vector2(2.03f, -4.02f), new Vector2(1.24f, 0.37f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(67.81f, -4.8f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.console3MinusButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase73()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        consoleInputValue = "-2";

        if(console.nbText.text == "-2")
        {
            phaseNum++;
        }
    }

    void Phase74()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.console3MinusButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localRotation = Quaternion.Euler(0, 0, 50.3f);

            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(2.03f, -4.77f), new Vector2(0.35f, 0.35f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(68.94f, -5),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.console3ValidateButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase75()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localRotation = Quaternion.Euler(0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.console3ValidateButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(77.26f, 1.76f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe3Button).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase76()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.loupe3Button).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(74.09f, 0.55f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.closePickTUButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase77()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.closePickTUButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(71.45f, 0.61f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.listPickTUBackButton).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase78()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.listPickTUBackButton).enabled = false;

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

    void Phase79()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(69.67f, 1.64f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton3).enabled = true;

        phaseNum++;
    }

    void Phase80()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(72.07f, -3.35f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

        phaseNum++;
    }
    #endregion

    #region Colis source 11
    void Phase81()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = true;

        remplissageColisTuto = 2;

        phaseNum++;
    }

    void Phase82()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisVide1).enabled = false;

            remplissageColisTuto = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase83()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colisSource1).enabled = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(63.89f, 1.64f),
                    new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pushButton1).interactable = true;

        phaseNum++;
    }

    void Phase84()
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
                        new Vector2(62.23f, 4.65f),
                        new Vector3(0, 0, 0), new Vector3(0, 0, 0), 0, false);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.quitButton).interactable = true;

            StartCoroutine(NewPhase(1));
        }
    }

    void Phase85()
    {
        gameObjectsManager.quitButton.GetComponent<BoutonChangementScene>().LoadNewScene(0);
    }
    #endregion
}
