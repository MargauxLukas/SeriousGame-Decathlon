using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManagerRecep : MonoBehaviour
{
    public static TutoManagerRecep instance;

    public DialogueManager dialogueManager;
    public GameObjectsManagerRecep gameObjectsManager;
    public ChangementEtiquettes etiqueteuse;

    public List<Dialogue> listDialogues;

    [Header("Launch Dialogue & Phases")]
    public float phaseNum = 0;
    public int dialogNum = 0;
    public bool canPlayFirst = true;
    public bool canPlaySecond = false;
    public float interactionNum = 0;

    private int colisNum = 8;

    [Header("Menu Colis")]
    public bool canPalette          = false;
    public bool canOpenTurnMenu     = false;
    public bool canCloseMenuTourner = false;
    public bool canCloseGestAnomalies = false;


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

    public void RenvoiColis()
    {
        colisNum++;

        if(colisNum >= 11)
        {
            phaseNum++;
            Manager(8);
        }
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

    IEnumerator CloseTurnMenu(float time)
    {
        yield return new WaitForSeconds(time);
        canCloseMenuTourner = true;
    }

    IEnumerator CloseGestAnomalies(float time)
    {
        yield return new WaitForSeconds(time);
        canCloseGestAnomalies = true;
    }

    IEnumerator IlluminateCircleMenu(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        gameObjectsManager.GameObjectToSpriteRenderer(gameObject).enabled = true;
    }

    public void Manager(float interaction)
    {
        interactionNum = interaction;

        Debug.Log("Interaction : " + interactionNum + " Phase : " + phaseNum);

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

            #region 1 - Interaction bouton On/Off
            case (1):
                switch (phaseNum)
                {
                    case (1):
                        Phase01();
                        break;

                    case (36):
                        Phase36();
                        break;

                    case (48):
                        Phase48();
                        break;
                }
                break;
                #endregion

            #region 2 - Interaction bouton Déplier
            case (2):
                switch (phaseNum)
                {
                    case (2):
                        Phase02();
                        break;
                }
                break;
                #endregion

            #region 3 - Convoyeur déplié au max.
            case (3):
                switch (phaseNum)
                {
                    case (3):
                        Phase03();
                        break;
                }
                break;
            #endregion

            #region 4 - Skip Automatique
            case (4):
                switch (phaseNum)
                {
                    case (4):
                        Phase04();
                        break;

                    case (26):
                        Phase26();
                        break;

                    case (27):
                        Phase27();
                        break;

                    case (28):
                        Phase28();
                        break;

                    case (29):
                        Phase29();
                        break;

                    case (30):
                        Phase30();
                        break;

                    case (44):
                        Phase44();
                        break;
                }
                break;
            #endregion

            #region 5 - Interaction bouton Monter
            case (5):
                switch (phaseNum)
                {
                    case (5):
                        Phase05();
                        break;
                }
                break;
            #endregion

            #region 6 - Jauge Convoyeur 3e étage
            case (6):
                switch (phaseNum)
                {
                    case (6):
                        Phase06();
                        break;
                }
                break;
            #endregion

            #region 7 - Colis sur convoyeur
            case (7):
                switch (phaseNum)
                {
                    case (7):
                        Phase07();
                        break;

                    case (10):
                        Phase10();
                        break;

                    case (19):
                        Phase19();
                        break;
                }
                break;
            #endregion

            #region 8 - Envoi Colis
            case (8):
                switch (phaseNum)
                {
                    case (8):
                        Phase08();
                        break;

                    case (13):
                        Phase13();
                        break;

                    case (14):
                        Phase14();
                        break;

                    case (15):
                        Phase15();
                        break;

                    case (22):
                        Phase22();
                        break;

                    case (37):
                        Phase37();
                        break;

                    case (38):
                        Phase38();
                        break;

                    case (39):
                        RenvoiColis();
                        break;

                    case (40):
                        Phase40();
                        break;

                    case (41):
                        Phase41();
                        break;
                }
                break;
            #endregion

            #region 9 - Click Colis
            case (9):
                switch (phaseNum)
                {
                    case (9):
                        Phase09();
                        break;

                    case (18):
                        Phase18();
                        break;

                    case (21):
                        Phase21();
                        break;
                }
                break;
            #endregion

            #region 10 - Click Flèches tourner
            case (10):
                switch (phaseNum)
                {
                    case (11):
                        Phase11();
                        break;

                    case (12):
                        Phase12();
                        break;
                }
                break;
            #endregion

            #region 11 - Interaction bouton Descendre
            case (11):
                switch (phaseNum)
                {
                    case (16):
                        Phase16();
                        break;
                }
                break;
            #endregion

            #region 12 - Jauge convoyeur 2e étage
            case (12):
                switch (phaseNum)
                {
                    case (17):
                        Phase17();
                        break;
                }
                break;
            #endregion

            #region 13 - Interaction bouton Palette
            case (13):
                switch (phaseNum)
                {
                    case (20):
                        Phase20();
                        break;
                }
                break;
            #endregion

            #region 14 - Interaction bouton Quitter
            case (14):
                switch (phaseNum)
                {
                    case (23):
                        Phase23();
                        break;

                    case (42):
                        Phase42();
                        break;
                }
                break;
            #endregion

            #region 15 - OnTriggerEnter triggerCamera
            case (15):
                switch (phaseNum)
                {
                    case (24):
                        Phase24();
                        break;

                    case (34):
                        Phase34();
                        break;

                    case (43):
                        Phase43();
                        break;

                    case (46):
                        Phase46();
                        break;
                }
                break;
            #endregion

            #region 16 - Interaction Poste gestion anomalies
            case (16):
                switch (phaseNum)
                {
                    case (25):
                        Phase25();
                        break;
                }
                break;
            #endregion

            #region 17 - Ouverture Menu circulaire
            case (17):
                switch (phaseNum)
                {
                    case (31):
                        Phase31();
                        break;
                }
                break;
            #endregion

            #region 18 - Interaction bouton Palette menu circulaire
            case (18):
                switch (phaseNum)
                {
                    case (32):
                        Phase32();
                        break;
                }
                break;
            #endregion

            #region 19 - Fermeture Poste gestion anomalies
            case (19):
                switch (phaseNum)
                {
                    case (33):
                        Phase33();
                        break;
                }
                break;
            #endregion

            #region 20 - Interaction Poste interactible
            case (20):
                switch (phaseNum)
                {
                    case (35):
                        Phase35();
                        break;

                    case (47):
                        Phase47();
                        break;
                }
                break;
            #endregion

            #region 21 - Jauge convoyeur 1er étage
            case (21):
                switch (phaseNum)
                {
                    case (39):
                        Phase39();
                        break;
                }
                break;
            #endregion

            #region 22 - Interaction étiqueteuse
            case (22):
                switch (phaseNum)
                {
                    case (45):
                        Phase45();
                        break;
                }
                break;
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
           Indications(new Vector2(5.1f, -7.28f), new Vector2(0.98f, 1.79f),
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
                        new Vector2(-3.21f, -12.21f), new Vector2(1,1),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(63.44f, -12.86f, 0), new Vector3(0, 0, 0), 0, false,
                        new Vector2(72.2f, -7.31f), 0, 0, 270);

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
                        new Vector2(-0.1f, -5.1f), new Vector2(2.35f, 1.5f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(63.91f, 9.27f, 0), new Vector3(66.77f, -5.6f), 4f, true,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

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
                        new Vector3(0, 0, 0), new Vector3(0,0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(70.89f, -4.88f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;

            StartCoroutine(CloseTurnMenu(1f));
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
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-1.07f, -8.72f), new Vector2(0.88f, 0.89f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase09()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                    new Vector2(0, 0), 0, 0, 0);

        phaseNum++;
    }

    void Phase10()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(66.97f, -7.23f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.upArrowTurnSpriteMask).enabled = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.upArrow).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase11()
    {
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.upArrowTurnSpriteMask).enabled = false;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.upArrow).interactable = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(3.07f, -6.4f), new Vector2(1.2f, 1.2f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(69.96f, -7.37f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotaArrow).interactable = true;
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
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotaArrow).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(70.89f, -4.88f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;

            StartCoroutine(CloseTurnMenu(1f));
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
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis3).enabled = true;
            
            gameObjectsManager.GameObjectToButton(gameObjectsManager.upArrow)       .interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.downArrow)     .interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftArrow)     .interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightArrow)    .interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftRotaArrow) .interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotaArrow).interactable = true;

            canCloseMenuTourner = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase14()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis3).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis4).enabled = true;

        phaseNum++;
    }

    void Phase15()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis4).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.upArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.downArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftRotaArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotaArrow).interactable = false;

            canCloseMenuTourner = false;

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
                        new Vector3(65.68f, -12.74f, 0), new Vector3(0, 0), 0, false,
                        new Vector2(72.2f, -8f), 0, 0, 270);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.descendButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase16()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(72.2f, -8f), 0, 0, 270);

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
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.descendButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-2.87f, -8.75f), new Vector2(0.84f, 0.9f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis5).enabled = true;

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
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

        phaseNum++;
    }

    void Phase19()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis5).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-6.15f, -6.42f), new Vector2(1.26f, 1.48f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(60.89f, -7.48f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.palette).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase20()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.palette).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-1.07f, -8.72f), new Vector2(0.88f, 0.89f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis6).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase21()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

        phaseNum++;
    }

    void Phase22()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis6).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-9.17f, -3.58f), new Vector2(0.35f, 0.34f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(57.62f, -4.45f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.quitButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase23()
    {
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localPosition = new Vector3(-4.29f, 4.67f, 2);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localScale = new Vector3(1.5f, 1.5f, 1);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localRotation = Quaternion.Euler(0, 0, 180);
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrowCameraDezoom).enabled = true;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrowCameraDezoom).enabled = true;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cameraTriggerUp).enabled = true;

        phaseNum++;
    }

    void Phase24()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrowCameraDezoom).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrowCameraDezoom).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cameraTriggerUp).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0.13f, 23.9f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.detectAnomaliesDesk).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase25()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.detectAnomaliesDesk).enabled = false;

        Indications(new Vector2(-66.71f, 31.71f), new Vector2(3.13f, 0.69f),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                    new Vector2(0, 0), 0, 0, 0);

        StartCoroutine(NewPhase(1f));
    }

    void Phase26()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-66.75f, 25.68f), new Vector2(1.78f, 1.94f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;

            Manager(4);
        }
    }

    void Phase27()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.menuCirculaireTuto).enabled = true;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.menuCirculaireTutoSpriteMask).enabled = true;

        StartCoroutine(IlluminateCircleMenu(gameObjectsManager.whiteCircle22, 0.5f));

        StartCoroutine(NewPhase(0.5f));
    }

    void Phase28()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.whiteCircle22).enabled = false;

            StartCoroutine(IlluminateCircleMenu(gameObjectsManager.whiteCircle12, 0.5f));

            StartCoroutine(NewPhase(0.5f));
        }
    }

    void Phase29()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.whiteCircle12).enabled = false;

            StartCoroutine(NewPhase(0.5f));
        }
    }

    void Phase30()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.menuCirculaireTuto).enabled = false;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.menuCirculaireTutoSpriteMask).enabled = false;

        Indications(new Vector2(-66.75f, 25.68f), new Vector2(1.78f, 1.94f),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(-0.07f, 24.64f, 0), new Vector3(0, 0), 0, false,
                    new Vector2(0, 0), 0, 0, 0);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cartonGestAnomalies).enabled = true;

        phaseNum++;
    }

    void Phase31()
    {
        Indications(new Vector2(-66.75f, 25.68f), new Vector2(1.78f, 1.94f),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                    new Vector2(0, 0), 0, 0, 0);

        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.menuCirculaireSpriteMask).enabled = true;

        canPalette = true;

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
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.menuCirculaireSpriteMask).enabled = false;

            canPalette = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cartonGestAnomalies).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localRotation = Quaternion.Euler(0, 180, 0);

            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(-7.95f, 20.44f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;

            StartCoroutine(CloseGestAnomalies(1f));
        }
    }

    void Phase33()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localRotation = Quaternion.Euler(0, 0, 0);

        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localPosition = new Vector3(-4.29f, -4.6f, 2);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localScale = new Vector3(1.5f, 1.5f, 1);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localRotation = Quaternion.Euler(0, 0, 360);
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrowCameraDezoom).enabled = true;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrowCameraDezoom).enabled = true;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cameraTriggerDown).enabled = true;

        phaseNum++;
    }

    void Phase34()
    {
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrowCameraDezoom).enabled = false;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrowCameraDezoom).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cameraTriggerDown).enabled = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0.54f, 3.58f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.posteInteractible).enabled = true;

        phaseNum++;
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
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.posteInteractible).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(61.22f, -12.91f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToButton(gameObjectsManager.onOffButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase36()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis7).enabled = true;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.upArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.downArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.leftArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.rightArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.leftRotaArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotaArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.palette).interactable = true;

        canCloseMenuTourner = true;

        phaseNum++;
    }

    void Phase37()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis7).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis8).enabled = true;

        phaseNum++;
    }

    void Phase38()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis8).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.upArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.downArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftRotaArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotaArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.palette).interactable = false;

            canCloseMenuTourner = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.descendButton).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase39()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis9).enabled = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis10).enabled = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis11).enabled = true;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.upArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.downArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.leftArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.rightArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.leftRotaArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotaArrow).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.palette).interactable = true;

        canCloseMenuTourner = true;
    }

    void Phase40()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis9).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis10).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis11).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis12).enabled = true;

        phaseNum++;
    }

    void Phase41()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis12).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.upArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.downArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftRotaArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotaArrow).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.palette).interactable = false;

            etiqueteuse.nbEtiquettes = 0;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(63.24f, 1.72f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.ampouleSpriteMask).enabled = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.ampoule).interactable = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase42()
    {
        Indications(new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.ampouleSpriteMask).enabled = false;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.ampoule).interactable = false;

        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localPosition = new Vector3(-4.29f, 4.67f, 2);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localScale = new Vector3(1.5f, 1.5f, 1);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localRotation = Quaternion.Euler(0, 0, 180);
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrowCameraDezoom).enabled = true;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrowCameraDezoom).enabled = true;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cameraTriggerUp).enabled = true;

        phaseNum++;
    }

    void Phase43()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrowCameraDezoom).enabled = false;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrowCameraDezoom).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cameraTriggerUp).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-68.85f, 30.92f), new Vector2(0.98f, 1.46f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            StartCoroutine(NewPhase(1f));
        }
    }

    void Phase44()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-68.85f, 30.92f), new Vector2(0.98f, 1.46f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(-1.96f, 29.94f),
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.etiqueteuse).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
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
                        new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                        new Vector2(0, 0), 0, 0, 0);

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.etiqueteuse).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localPosition = new Vector3(-4.29f, -4.6f, 2);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localScale = new Vector3(1.5f, 1.5f, 1);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.arrowCameraDezoom).transform.localRotation = Quaternion.Euler(0, 0, 360);
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrowCameraDezoom).enabled = true;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrowCameraDezoom).enabled = true;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cameraTriggerDown).enabled = true;

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }

    void Phase46()
    {
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.arrowCameraDezoom).enabled = false;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.arrowCameraDezoom).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.cameraTriggerDown).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.posteInteractible).enabled = true;

        phaseNum++;
    }

    void Phase47()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.posteInteractible).enabled = false;

        Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(61.22f, -12.91f),
                    new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                    new Vector2(0, 0), 0, 0, 0);

        gameObjectsManager.GameObjectToButton(gameObjectsManager.onOffButton).interactable = true;

        phaseNum++;
    }

    void Phase48()
    {
        if (canPlayFirst)
        {
            Indications(new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0), new Vector2(0, 0),
                    new Vector2(0, 0),
                    new Vector3(0, 0, 0), new Vector3(0, 0), 0, false,
                    new Vector2(0, 0), 0, 0, 0);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            Indications(new Vector2(-2.87f, -8.75f), new Vector2(0.84f, 0.9f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(1.2f, 2.3f), new Vector2(0.6f, 0.6f),
                        new Vector2(0, 0), new Vector2(0, 0),
                        new Vector2(0, 0),
                        new Vector3(/*posVentouse*/), new Vector3(/*posColis*/), 4, true,
                        new Vector2(0, 0), 0, 0, 0);

            canPlayFirst = true;
            canPlaySecond = false;
            phaseNum++;
        }
    }
}