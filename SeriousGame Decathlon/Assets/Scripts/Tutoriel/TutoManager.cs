using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public static TutoManager instance;

    public DialogueManager       dialogueManager;
    public GameObjectsManager gameObjectsManager;
    public ColisManager             colisManager;

    public List<Dialogue> listDialogues;
    public List<Colis   >     listColis;

    public float     phaseNum = 0;
    public int      dialogNum = 0;
    public string articlesNum = "";

    [Header("Menu Colis")]
    public bool canJeter            = false;
    public bool canVider            = false;
    public bool canOuvrirFermer     = false;
    public bool canOpenTurnMenu     = false;
    public bool canCloseMenuToruner = false;

    [Header("Menu Articles")]
    public bool canColis1         = false;
    public bool canColis2         = false;
    public bool canInfo           = false;
    public bool canCloseFicheInfo = false;

    [Header("Launch Dialogue & Phases")]
    public bool canPlayFirst    =  true;
    public bool canPlaySecond   = false;
    public float interactionNum =     0;

    //Déplacement doigt
    private Vector3 fingerPosition;
    private Vector3 targetPosition;

    private float fingerSpeed;

    //Position Colis
    private Vector3    colisPosition;
    private Vector3 articlesPosition;

    void Awake()
    {
        if (instance == null) { instance = this  ; }
        else                  { Destroy(instance); }
    }

    private void Start()
    {
        colisManager.GetComponent<ColisManager>().listeColisTraiter = listColis;

        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
    }

    public void DialogueIsFinished()
    {
        canPlayFirst  =   false;
        canPlaySecond =    true;
        Manager(interactionNum);
    }

    public void Manager(float interaction)
    {
        interactionNum = interaction;
        Debug.Log("Interaction : " + interactionNum + " Phase : " + phaseNum);
        switch (interaction)
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

            #region 1 - Interaction Pédale
            case (1):
                switch (phaseNum)
                {
                    case (1):
                        Phase01();
                        break;

                    case (64):
                        Phase64();
                        break;

                    case (99):
                        Phase99();
                        break;
                }
                break;
            #endregion

            #region 2 - Scan HU
            case (2):
                switch (phaseNum)
                {
                    case (2):
                        Phase02();
                        break;

                    case (100):
                        Phase100();
                        break;
                }
                break;
            #endregion

            #region 3 - Ouverture Ecran
            case (3):
                switch (phaseNum)
                {
                    case (3):
                        Phase03();
                        break;

                    case (9):
                        Phase09();
                        break;

                    case (15):
                        Phase15();
                        break;

                    case (30):
                        Phase30();
                        break;

                    case (38):
                        Phase38();
                        break;

                    case (53):
                        Phase53();
                        break;

                    case (57):
                        Phase57();
                        break;

                    case (73):
                        Phase73();
                        break;

                    case (82):
                        Phase82();
                        break;

                    case (89):
                        Phase89();
                        break;

                    case (101):
                        Phase101();
                        break;

                    case (112):
                        Phase112();
                        break;
                }
                break;
            #endregion

            #region 4 - Skip automatique
            case (4):
                switch (phaseNum)
                {
                    case (4):
                        Phase04();
                        break;

                    case (5):
                        Phase05();
                        break;

                    case (6):
                        Phase06();
                        break;

                    case (7):
                        Phase07();
                        break;

                    case (8):
                        Phase08();
                        break;

                    case (9):
                        Phase09();
                        break;

                    case (11):
                        Phase11();
                        break;

                    case (16):
                        Phase16();
                        break;

                    case (18):
                        Phase18();
                        break;

                    case (19):
                        Phase19();
                        break;

                    case (26):
                        Phase26();
                        break;

                    case (31):
                        Phase31();
                        break;

                    case (32):
                        Phase32();
                        break;

                    case (36):
                        Phase36();
                        break;

                    case (39):
                        Phase39();
                        break;

                    case (42):
                        Phase42();
                        break;

                    case (56):
                        Phase56();
                        break;

                    case (61):
                        Phase61();
                        break;

                    case (63):
                        Phase63();
                        break;

                    case (65):
                        Phase65();
                        break;

                    case (67):
                        Phase67();
                        break;

                    case (70):
                        Phase70();
                        break;

                    case (87):
                        Phase87();
                        break;

                    case (97):
                        Phase97();
                        break;

                    case (105):
                        Phase105();
                        break;
                }
                break;
            #endregion

            #region 5 - Interaction onglet Recount
            case (5):
                switch (phaseNum)
                {
                    case (10):
                        Phase10();
                        break;
                }
                break;
            #endregion

            #region 6 - Interaction bouton Recount
            case (6):
                switch (phaseNum)
                {
                    case (12):
                        Phase12();
                        break;
                }
                break;
            #endregion

            #region 7 - Fermeture Ecran
            case (7):
                switch (phaseNum)
                {
                    case (13):
                        Phase13();
                        break;

                    case (17):
                        Phase17();
                        break;

                    case (35):
                        Phase35();
                        break;

                    case (46):
                        Phase46();
                        break;

                    case (55):
                        Phase55();
                        break;

                    case (59):
                        Phase59();
                        break;

                    case (68):
                        Phase68();
                        break;

                    case (76):
                        Phase76();
                        break;

                    case (95):
                        Phase95();
                        break;

                    case (110):
                        Phase110();
                        break;
                }
                break;
            #endregion

            #region 8 - Scan RFID
            case (8):
                switch (phaseNum)
                {
                    case (14):
                        Phase14();
                        break;

                    case (66):
                        Phase66();
                        break;
                }
                break;
            #endregion

            #region 9 - Ouverture Menu circulaire Colis
            case (9):
                switch (phaseNum)
                {
                    case (20):
                        Phase20();
                        break;

                    case (22):
                        Phase22();
                        break;

                    case (47):
                        Phase47();
                        break;

                    case (49):
                        Phase49();
                        break;

                    case (80):
                        Phase80();
                        break;

                    case (117):
                        Phase117();
                        break;
                }
                break;
            #endregion

            #region 10 - Interaction bouton OuvrirFermer
            case (10):
                switch (phaseNum)
                {
                    case (21):
                        Phase21();
                        break;

                    case (48):
                        Phase48();
                        break;

                    case (81):
                        Phase81();
                        break;
                }
                break;
            #endregion

            #region 11 - Interaction bouton Vider
            case (11):
                switch (phaseNum)
                {
                    case (23):
                        Phase23();
                        break;

                    case (69):
                        Phase69();
                        break;
                }
                break;
            #endregion

            #region 12 - Ouverture Menu circulaire Articles
            case (12):
                switch (phaseNum)
                {
                    case (24):
                        Phase24();
                        break;

                    case (27.1f):
                        Phase27bis();
                        break;

                    case (78):
                        Phase78();
                        break;
                }
                break;
            #endregion

            #region 13 - Interaction bouton Info Articles
            case (13):
                switch (phaseNum)
                {
                    case (25):
                        Phase25();
                        break;
                }
                break;
            #endregion

            #region 14 - Fermeture Fiche info
            case (14):
                switch (phaseNum)
                {
                    case (27):
                        Phase27();
                        break;

                    case (71):
                        Phase71();
                        break;

                    case (72):
                        Phase72();
                        break;
                }
                break;
            #endregion

            #region 15 - Interaction bouton Colis1
            case (15):
                switch (phaseNum)
                {
                    case (27.2f):
                        Phase27ter();
                        break;

                    case (79):
                        Phase79();
                        break;
                }
                break;
            #endregion

            #region 16 - Interaction bouton Inventory
            case (16):
                switch (phaseNum)
                {
                    case (33):
                        Phase33();
                        break;
                }
                break;
            #endregion

            #region 17 - Interaction bouton Print HU (Recount)
            case (17):
                switch (phaseNum)
                {
                    case (34):
                        Phase34();
                        break;
                }
                break;
            #endregion

            #region 18 - Collage HU
            case (18):
                switch (phaseNum)
                {
                    case (37):
                        Phase37();
                        break;

                    case (96):
                        Phase96();
                        break;

                    case (111):
                        Phase111();
                        break;
                }
                break;
            #endregion

            #region 19 - Interaction toggle End Task
            case (19):
                switch (phaseNum)
                {
                    case (40):
                        Phase40();
                        break;

                    case (45):
                        Phase45();
                        break;

                    case (54):
                        Phase54();
                        break;

                    case (83):
                        Phase83();
                        break;

                    case (83.1f):
                        Phase83bis();
                        break;

                    case (113):
                        Phase113();
                        break;
                }
                break;
            #endregion

            #region 20 - Interaction onglet Filling Rate
            case (20):
                switch (phaseNum)
                {
                    case (41):
                        Phase41();
                        break;
                }
                break;
            #endregion

            #region 21 - Interaction bouton %
            case (21):
                switch (phaseNum)
                {
                    case (43):
                        Phase43();
                        break;
                }
                break;
            #endregion

            #region 22 - Interaction toggle Can meca open
            case (22):
                switch (phaseNum)
                {
                    case (44):
                        Phase44();
                        break;
                }
                break;
            #endregion

            #region 23 - Interaction bouton Return to meca
            case (23):
                switch (phaseNum)
                {
                    case (58):
                        Phase58();
                        break;

                    case (84):
                        Phase84();
                        break;

                    case (114):
                        Phase114();
                        break;
                }
                break;
            #endregion

            #region 24 - Renvoi colis droite
            case (24):
                switch (phaseNum)
                {
                    case (60):
                        Phase60();
                        break;

                    case (85):
                        Phase85();
                        break;

                    case (115):
                        Phase115();
                        break;
                }
                break;
            #endregion

            #region 25 - Interaction bouton Tourner
            case (25):
                switch (phaseNum)
                {
                    case (50):
                        Phase50();
                        break;
                }
                break;
            #endregion

            #region 26 - Interaction flèches
            case (26):
                switch (phaseNum)
                {

                }
                break;
            #endregion

            #region 27 - Colis bien orienté
            case (27):
                switch (phaseNum)
                {
                    case (51):
                        Phase51();
                        break;
                }
                break;
            #endregion

            #region 28 - Fermeture menu flèches
            case (28):
                switch (phaseNum)
                {
                    case (52):
                        Phase52();
                        break;
                }
                break;
            #endregion

            #region 29 - Renvoi colis haut
            case (29):
                switch (phaseNum)
                {
                    case (62):
                        Phase62();
                        break;

                    case (86):
                        Phase86();
                        break;

                    case (98):
                        Phase98();
                        break;

                    case (116):
                        Phase116();
                        break;
                }
                break;
            #endregion

            #region 30 - Interaction bouton Print RFID
            case (30):
                switch (phaseNum)
                {
                    case (75):
                        Phase75();
                        break;
                }
                break;
            #endregion

            #region 31 - Collage RFID
            case (31):
                switch (phaseNum)
                {
                    case (77):
                        Phase77();
                        break;
                }
                break;
            #endregion

            #region 33 - Interaction bouton CB01 ou New colis Repack tab
            case (33):
                switch (phaseNum)
                {
                    case (88):
                        Phase88();
                        break;

                    case (106):
                        Phase106();
                        break;
                }
                break;
            #endregion

            #region 34 - Interaction bouton Create HU
            case (34):
                switch (phaseNum)
                {
                    case (90):
                        Phase90();
                        break;
                }
                break;
            #endregion

            #region 35 - Good value on Packaging Mat. Dropdown
            case (35):
                switch (phaseNum)
                {
                    case (91):
                        Phase91();
                        break;
                }
                break;
            #endregion

            #region 36 - Good value on Reference Dropdown
            case (36):
                switch (phaseNum)
                {
                    case (92):
                        Phase92();
                        break;
                }
                break;
            #endregion

            #region 37 - Good value on Quantity Text
            case (37):
                switch (phaseNum)
                {
                    case (93):
                        Phase93();
                        break;
                }
                break;
            #endregion

            #region 38 - Interaction bouton OK Create HU
            case (38):
                switch (phaseNum)
                {
                    case (94):
                        Phase94();
                        break;
                }
                break;
            #endregion

            #region 41 - Good value on Quantity 1 & 2 (Repack)
            case (41):
                switch (phaseNum)
                {
                    case (108):
                        Phase108();
                        break;
                }
                break;
            #endregion

            #region 42 - Interaction bouton - (Repack)
            case (42):
                switch (phaseNum)
                {
                    case (107):
                        Phase107();
                        break;
                }
                break;
            #endregion

            #region 43 - Interaction bouton Print HU (Repack)
            case (43):
                switch (phaseNum)
                {
                    case (109):
                        Phase109();
                        break;
                }
                break;
            #endregion

            #region 44 - Interaction bouton Jeter
            case (44):
                switch (phaseNum)
                {
                    case (118):
                        Phase118();
                        break;
                }
                break;
            #endregion

            #region 45 - Interaction bouton Quitter
            case (45):
                switch (phaseNum)
                {
                    case (119):
                        Phase119();
                        break;
                }
                break;
            #endregion

            #region 46 - Good value on Choix Nombre articles text
            case (46):
                switch (phaseNum)
                {
                    case (28):
                        Phase28();
                        break;

                    case (79.1f):
                        Phase79bis();
                        break;
                }
                break;
            #endregion

            #region 47 - Interaction bouton validate choix (pile article à déplacer)
            case (47):
                switch (phaseNum)
                {
                    case (29):
                        Phase29();
                        break;

                    case (79.2f):
                    {
                        Phase79ter();
                        break;
                    }
                }
                break;
            #endregion

            default:
                Debug.Log("Too far :/");
                break;
        }
    }

    IEnumerator MoveDoigt()
    {
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition += (targetPosition - gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition).normalized * Time.fixedDeltaTime * fingerSpeed;
       
        if (Vector3.Distance(gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition, targetPosition) <= 0.2f)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = fingerPosition;
            gameObjectsManager.GameObjectToAnimator (gameObjectsManager.doigtStay).SetBool("endLoop", true);
        }
        else
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", false);
        }

        yield return new WaitForSeconds(Time.fixedDeltaTime);
        StartCoroutine(MoveDoigt());
    }

    IEnumerator NewPhase      (float time)
    {
        yield return new WaitForSeconds(time);

        phaseNum++;
        canPlayFirst  =  true;
        canPlaySecond = false;

        Manager(4);
    }

    IEnumerator CloseFicheInfo(float time)
    {
        yield return new WaitForSeconds(time);
        canCloseFicheInfo = true;
    }

    IEnumerator CloseTurnMenu (float time)
    {
        yield return new WaitForSeconds(time);
        canCloseMenuToruner = true;
    }

    IEnumerator ReturnToMenu  (float time)
    {
        yield return new WaitForSeconds(time);
        gameObjectsManager.quitButtonToMenu.GetComponent<BoutonChangementScene>().LoadNewScene(4);
    }

    #region Colis1
    void Phase00()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)     .enabled = true;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.circleSpriteMask).transform.localPosition = new Vector2(11.64f,-4.04f);
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.circleSpriteMask).transform.localScale    = new Vector2(  0.9f,  0.9f);
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.circleSpriteMask).enabled = true;

        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.1f,-4.5f);
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = true;

        canPlayFirst  =  true;
        canPlaySecond = false;
        phaseNum++;
    }

    void Phase01()
    {
        if (canPlayFirst)
        {        
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)         .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.circleSpriteMask)    .enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.pedal)          .interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2( 5.3f, 1.24f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(0.44f, 0.84f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask02).transform.localPosition = new Vector2(8.8f, -1.43f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask02).transform.localScale    = new Vector2(1.4f,  1.19f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask02).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtStay).transform.localPosition = new Vector3(5.75f, 1f, 30f);
            fingerPosition = new Vector3(5.75f,    1f, 30f);
            targetPosition = new Vector3(8.75f, -1.4f, 30f);
            fingerSpeed = 8f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.pistolet).enabled = true;
            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.colis1)  .enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

   void Phase02()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;

            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask02).enabled = false;

            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.pistolet)           .enabled = false;
            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.colis1)             .enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.4f, 2.94f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(   2f,  1.2f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(11f, 2.45f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.screen)              .enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        } 
    }

    void Phase03()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)         .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01)  .enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.screen).enabled = false;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>()                 .enabled = false;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.recountTab).interactable = false;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.fillTab)   .interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(  6f,  3.5f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(5.8f, 1.15f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase04()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(6.05f, 3.64f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(5.56f, 1.18f);
            
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(2.35f, -4.23f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.29f,  0.48f);

            StartCoroutine(NewPhase(2f));
        }
    }

    void Phase05()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase06()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().CloseMonitorTuto();

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = true;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(6.85f, -2.64f);
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(   1f,  0.48f);
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

        StartCoroutine(NewPhase(2f));
    }

    void Phase07()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase08()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

        gameObjectsManager.GameObjectToButton        (gameObjectsManager.recountTab)   .interactable = false;
        gameObjectsManager.GameObjectToButton        (gameObjectsManager.fillTab)      .interactable = false;

        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().OpenMonitorTuto();

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)   .enabled = true;
        gameObjectsManager.GameObjectToTransform (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(6.23f, 1.64f);
        gameObjectsManager.GameObjectToTransform (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.72f, 0.33f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

        phaseNum++;
        Manager(4);
    }

    void Phase09()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(7.75f, 1.22f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)      .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)      .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton    (gameObjectsManager.recountTab)     .interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase10()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = true;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(1.28f, -0.3f);
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.64f, 0.83f);
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

        phaseNum++;

        Manager(4);
    }

    void Phase11()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(9.43f, 0.29f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2( 1.8f, 0.42f);

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick)      .transform.localPosition = new Vector2(10.75f, -0.28f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)      .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)      .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton    (gameObjectsManager.recountButton)  .interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase12()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)         .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01)  .enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.recountTab)      .interactable = false;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.recountButton)   .interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(0.32f, 1.03f, 30f);
            fingerPosition = new Vector3(0.32f, 1.03f, 30f);
            targetPosition = new Vector3( 9.3f, 1.03f, 30f);
            fingerSpeed = 8f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase13()
    {
        if (canPlayFirst)
        {
            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;

            gameObjectsManager.GameObjectToTransform (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(8.61f, -1.44f);
            gameObjectsManager.GameObjectToTransform (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.54f,  1.25f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform (gameObjectsManager.squareSpriteMask02).transform.localPosition = new Vector2(6.85f, -2.64f);
            gameObjectsManager.GameObjectToTransform (gameObjectsManager.squareSpriteMask02).transform.localScale    = new Vector2(   1f,  0.48f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(9.3f, -1.9f, 30f);
            fingerPosition = new Vector3( 9.3f, -1.9f, 30f);
            targetPosition = new Vector3(6.72f, -1.9f, 30f);
            fingerSpeed = 8f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1)  .enabled = true;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase14()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask02).enabled = false;

            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.colis1)             .enabled = false;
            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.scanRFID)           .enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.4f, 2.94f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(   2f,  1.2f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(11f, 2.45f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase15()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)         .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01)  .enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(1.35f, 0.3f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.57f, 0.4f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask02).transform.localPosition = new Vector2(2.38f, -4.25f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask02).transform.localScale    = new Vector2(1.26f,   0.5f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask02).enabled = true;

            StartCoroutine(NewPhase(3f));
        }
    }

    void Phase16()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask02).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase17()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3 (0.5f, 0.2f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;

            StartCoroutine(NewPhase(0.5f));
        }
    }

    void Phase18()
    {
        colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.menuCirculaireColis).transform.localPosition = colisPosition;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.menuCirculaireColis).enabled = true;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)              .enabled = true;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.menuColis44SpriteMaskTuto).enabled = true;

        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3(0.5f, 0.2f, 0);

        StartCoroutine(NewPhase(2f));
    }

    void Phase19()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.menuCirculaireColis)      .enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)              .enabled = false;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.menuColis44SpriteMaskTuto).enabled = false;

        fingerSpeed = 0;

        colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
        gameObjectsManager.GameObjectToTransform  (gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3(0.5f, 0.2f, 0);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

        phaseNum++;
    }

    void Phase20()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

        canOuvrirFermer = true;

        phaseNum++;
    }

    void Phase21()
    {
        if (canPlayFirst)
        {
            canOuvrirFermer = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase22()
    {
        canVider = true;

        phaseNum++;
    }

    void Phase23()
    {
        if (canPlayFirst)
        {
            canVider = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(2.98f, -2.06f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(   1f,   0.5f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtStay).transform.localPosition = new Vector3(3.16f, -1.89f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticlesColis1).enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase24()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.menuCirculaireArticlesSpriteMask).enabled = true;

        canInfo = true;

        phaseNum++;
    }

    void Phase25()
    {
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.menuCirculaireArticlesSpriteMask).enabled = false;

        canInfo = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(0.55f, -0.96f);
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.46f,  1.65f);
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticlesColis1).enabled = false;

        phaseNum++;

        Manager(4);
    }

    void Phase26()
    {
        if (canPlayFirst)
        {
            canCloseFicheInfo = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(4.31f, -0.91f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            StartCoroutine(CloseFicheInfo(1f));

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase27()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            canCloseFicheInfo = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtStay).transform.localPosition = new Vector3(3.16f, -1.89f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.pileArticlesColis1) .enabled = true;

            phaseNum += 0.1f;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase27bis()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

        canColis1 = true;

        phaseNum += 0.1f;
    }

    void Phase27ter()
    {
        if (canPlayFirst)
        {
            canColis1 = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pileArticlesColis1PlusButton).interactable = true;

            articlesNum = "9";

            phaseNum += 0.8f;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase28()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pileArticlesColis1PlusButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pileArticlesColis1ValidateButton).interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase29()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton     (gameObjectsManager.pileArticlesColis1ValidateButton).interactable = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticlesColis1)              .enabled      = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase30()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        phaseNum++;
        Manager(4);
    }

    void Phase31()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(9.45f, -0.78f);
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.93f,  0.45f);
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

        phaseNum++;

        Manager(4);
    }

    void Phase32()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(10.72f, -1.34f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.inventoryButton).interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase33()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.inventoryButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(1.17f, -1.81f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2( 1.7f,  0.45f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(2.66f, -2.42f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.recountPrintHUButton).interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase34()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.recountPrintHUButton).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab)          .interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase35()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2( 7.2f, 2.08f);
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(0.52f, 0.66f);
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

        colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
        gameObjectsManager.GameObjectToTransform (gameObjectsManager.squareSpriteMask02).transform.localPosition = colisPosition;
        gameObjectsManager.GameObjectToTransform (gameObjectsManager.squareSpriteMask02).transform.localScale    = new Vector2(1.54f, 1.25f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = true;

        phaseNum++;

        Manager(4);
    }

    void Phase36()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(7.63f, 1.84f, 30f);
            fingerPosition = new Vector3(7.63f, 1.84f, 30f);
            targetPosition = colisPosition;
            fingerSpeed = 4f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase37()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask02).enabled = false;

            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.colis1)             .enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase38()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = true;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(11.47f,  4.1f);
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2( 0.33f, 0.33f);
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

        phaseNum++;

        Manager(4);
    }

    void Phase39()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.05f, 3.67f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToToggle        (gameObjectsManager.toggleEndTask1) .interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase40()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToToggle        (gameObjectsManager.toggleEndTask1) .interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(2.39f, 1.64f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.72f, 0.33f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(3.96f, 1.11f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.fillTab)        .interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase41()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(3.44f, -0.26f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(3.43f, 1.3f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase42()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(2.54f, 0.55f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(0.82f, 0.55f);

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(3.47f, -0.1f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.fill50Button)   .interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase43()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(9.84f, -0.21f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(2.37f,  0.41f);

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(9.08f, -0.7f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToToggle        (gameObjectsManager.mecaOpenToggle) .interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase44()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToToggle        (gameObjectsManager.mecaOpenToggle) .interactable = false;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.fillTab)        .interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.18f, 2.92f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToToggle        (gameObjectsManager.toggleEndTask2).interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase45()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToToggle        (gameObjectsManager.toggleEndTask2) .interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase46()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

        phaseNum++;
    }

    void Phase47()
    {
        canOuvrirFermer = true;

        phaseNum++;
    }

    void Phase48()
    {
        if (canPlayFirst)
        {
            canOuvrirFermer = false;
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
        
    }

    void Phase49()
    {
        canOpenTurnMenu = true;

        phaseNum++;
    }

    void Phase50()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = colisPosition;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.47f, 1.47f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = colisPosition + new Vector3(1.74f, -0.41f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.turnRightButton)    .interactable = true;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.turnLeftButton)     .interactable = true;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.turnUpButton)       .interactable = true;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.turnDownButton)     .interactable = true;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.leftRotationButton) .interactable = true;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.rightRotationButton).interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase51()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)             .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01)      .enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)              .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)              .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask)    .enabled = false;


            gameObjectsManager.GameObjectToButton        (gameObjectsManager.turnRightButton)    .interactable = false;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.turnLeftButton)     .interactable = false;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.turnUpButton)       .interactable = false;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.turnDownButton)     .interactable = false;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.leftRotationButton) .interactable = false;
            gameObjectsManager.GameObjectToButton        (gameObjectsManager.rightRotationButton).interactable = false;

            canOpenTurnMenu = false;
            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.colis1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(4.31f, -0.91f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

            StartCoroutine(CloseTurnMenu(1f));

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase52()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            canCloseMenuToruner = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase53()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.2f, 2.26f);
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = true;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = true;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = true;

        gameObjectsManager.GameObjectToToggle        (gameObjectsManager.toggleEndTask3) .interactable = true;

        phaseNum++;
    }

    void Phase54()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToToggle        (gameObjectsManager.toggleEndTask3) .interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase55()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.4f, 2.94f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(   2f,  1.2f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase56()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        } 
    }

    void Phase57()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.32f, -2.25f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)           .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)           .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask) .enabled = true;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.returnMecaButton).interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase58()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick)           .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtClick)           .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtClickSpriteMask) .enabled = false;

            gameObjectsManager.GameObjectToButton        (gameObjectsManager.returnMecaButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase59()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3(0.5f, 0.2f, 30f);
        fingerPosition = colisPosition + new Vector3(  0.5f,   0.2f, 30f);
        targetPosition =                 new Vector3(12.74f, -2.05f, 30f);
        fingerSpeed = 8f;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay).enabled = true;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;
        StartCoroutine(MoveDoigt());

        gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.colis1).enabled = true;

        phaseNum++;
    }

    void Phase60()
    {
        if (canPlayFirst)
        {
            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.colis1)             .enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.blackScreen)       .transform.localPosition = new Vector3(-20.1f, 0.23f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(-15.08f, 3.29f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(  2.67f, 1.97f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase61()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3(0.5f, 0.2f, 30f);
            fingerPosition = colisPosition + new Vector3(   0.5f, 0.2f, 30f);
            targetPosition =                 new Vector3(-19.97f, 2.5f, 30f);
            fingerSpeed = 4f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.colis1)             .enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase62()
    {
        fingerSpeed = 0;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = false;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = false;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = false;

         phaseNum++;
            
        Manager(4);
    }

    void Phase63()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }
    #endregion

    #region Colis2
    void Phase64()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase65()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2)       .enabled      = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet)     .enabled      = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen)       .enabled      = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        gameObjectsManager.GameObjectToButton     (gameObjectsManager.recountTab)   .interactable = true;
        gameObjectsManager.GameObjectToButton     (gameObjectsManager.recountButton).interactable = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID)     .enabled      = true;

        phaseNum++;
    }

    void Phase66()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2)       .enabled      = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet)     .enabled      = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen)       .enabled      = false;
            gameObjectsManager.GameObjectToButton     (gameObjectsManager.recountTab)   .interactable = false;
            gameObjectsManager.GameObjectToButton     (gameObjectsManager.recountButton).interactable = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID)     .enabled      = false;

            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().OpenMonitorTuto();

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.blackScreen)       .transform.localPosition = new Vector3(3.9f,   0.24f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = true;
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(1.31f, -0.22f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01).transform.localScale    = new Vector2(1.63f,  0.83f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask02).transform.localPosition = new Vector2(2.35f, -4.23f);
            gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask02).transform.localScale    = new Vector2( 1.3f,  0.48f);
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask02).enabled = true;

            StartCoroutine(NewPhase(2.5f));
        }
    }

    void Phase67()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask02).enabled = false;

            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        } 
    }

    void Phase68()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = true;

        canOuvrirFermer = true;
        canVider        = true;

        phaseNum++;
    }

    void Phase69()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = false;

            canOuvrirFermer = false;
            canVider        = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase70()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles1Colis2).enabled = true;

        canInfo           = true;
        canCloseFicheInfo = true;

        phaseNum++;
    }

    void Phase71()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles1Colis2).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles2Colis2).enabled =  true;

        StartCoroutine(NewPhase(1f));
    }

    void Phase72()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles2Colis2).enabled = false;
           
            canInfo = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            canCloseFicheInfo = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase73()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)         .enabled = true;
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01)  .transform.localPosition = new Vector2(5.07f, 0.32f);
        gameObjectsManager.GameObjectToTransform     (gameObjectsManager.squareSpriteMask01)  .transform.localScale    = new Vector2(1.73f, 0.47f);
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01)  .enabled = true;

        gameObjectsManager.GameObjectToButton        (gameObjectsManager.printRFIDButton).interactable = true;

        phaseNum += 2;
    }

    void Phase75()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen)       .enabled = false;
            gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.printRFIDButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst  =  true;
            canPlaySecond = false;
        }
    }

    void Phase76()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(4.23f, 2.02f, 30f);
        fingerPosition = new Vector3(4.23f,  2.02f, 30f);
        targetPosition = new Vector3(3.38f, -1.52f, 30f);
        fingerSpeed = 2f;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay)          .enabled = true;
        gameObjectsManager.GameObjectToAnimator      (gameObjectsManager.doigtStay)          .enabled = true;
        gameObjectsManager.GameObjectToSpriteMask    (gameObjectsManager.doigtStaySpriteMask).enabled = true;
        StartCoroutine(MoveDoigt());

        gameObjectsManager.GameObjectToBoxCollider   (gameObjectsManager.pileArticles1Colis2).enabled = true;

        phaseNum++;
    }

    void Phase77()
    {
        if (canPlayFirst)
        {
            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtStaySpriteMask).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase78()
    {
        canColis1 = true;

        phaseNum++;
    }

    void Phase79()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles1Colis2).enabled = false;
        canColis1 = false;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pileArticles1Colis2PlusButton).interactable = true;

        articlesNum = "10";

        phaseNum += 0.1f;
    }

    void Phase79bis()
    {
        gameObjectsManager.GameObjectToButton(gameObjectsManager.pileArticles1Colis2PlusButton).interactable = false;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pileArticles1Colis2ValidateButton).interactable = true;

        phaseNum += 0.1f;
    }

    void Phase79ter()
    {
        gameObjectsManager.GameObjectToButton(gameObjectsManager.pileArticles1Colis2ValidateButton).interactable = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = true;

        phaseNum += 0.8f;
    }

    void Phase80()
    {
        canOuvrirFermer = true;

        phaseNum++;
    }

    void Phase81()
    {
        if (canPlayFirst)
        {
            canOuvrirFermer = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase82()
    {
        Debug.Log("Hello Phase 82");
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask1).interactable = true;
        
        phaseNum++;
    }

    void Phase83()
    {
        gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask1).interactable = false;

        gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask2).interactable = true;

        phaseNum += 0.1f;
    }

    void Phase83bis()
    {
        gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask2).interactable = false;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.returnMecaButton).interactable = true;

        phaseNum += 0.9f;
    }

    void Phase84()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().CloseMonitorTuto();

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = true;

        phaseNum++;
    }

    void Phase85()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        phaseNum++;
    }

    void Phase86()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = false;

        phaseNum++;

        Manager(4);
    }

    void Phase87()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(-2.88f, -3.23f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.73f, 0.88f); ;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.CB01Button).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase88()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.CB01Button).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }
    
    void Phase89()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.92f, -1.8f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.22f, 0.28f); ;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.createHUButton).interactable = true;

        phaseNum++;
    }

    void Phase90()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.createHUButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToDropdown(gameObjectsManager.packMatDropdown).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase91()
    {
        gameObjectsManager.GameObjectToDropdown(gameObjectsManager.packMatDropdown).interactable = false;

        gameObjectsManager.GameObjectToDropdown(gameObjectsManager.refDropdown).interactable = true;

        phaseNum++;
    }

    void Phase92()
    {
        gameObjectsManager.GameObjectToDropdown(gameObjectsManager.refDropdown).interactable = false;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.createHUPlusButton).interactable = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.createHUMinusButton).interactable = true;

        phaseNum++;
    }

    void Phase93()
    {
        gameObjectsManager.GameObjectToButton(gameObjectsManager.createHUPlusButton).interactable = false;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.createHUMinusButton).interactable = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(9.09f, -1.81f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.96f, 0.3f); ;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.buttonOK).interactable = true;

        phaseNum++;
    }

    void Phase94()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.buttonOK).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
       
        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase95()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2bis).enabled = true;

        phaseNum++;
    }

    void Phase96()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2bis).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase97()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2bis).enabled = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles2Colis2).enabled = true;
        articlesNum = "5";
        canColis1 = true;
        canOuvrirFermer = true;

        phaseNum++;
    }

    void Phase98()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2bis).enabled = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles2Colis2).enabled = false;
            canColis1 = false;
            canOuvrirFermer = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            Debug.Log("Phase 98 second");
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }
    #endregion

    #region Colis3
    void Phase99()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = true;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis3).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase100()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis3).enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

        phaseNum++;
    }

    void Phase101()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask).transform.localPosition = new Vector2(-2.12f, 5.04f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask).transform.localScale = new Vector2(0.8f, 0.8f); ;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask).enabled = true;

            phaseNum += 3;

            StartCoroutine(NewPhase(1.5f));
        }
    }

    void Phase105()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(2.97f, -0.92f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.54f, 0.54f); ;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(3.76f, -1.59f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.newColisButton).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase106()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.newColisButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(6.52f, -0.25f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.repackTabMinusButton).interactable = true;

            gameObjectsManager.quantity1 = "0";
            gameObjectsManager.quantity2 = "10";

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase107()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = false;

        phaseNum++;
    }

    void Phase108()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToButton(gameObjectsManager.repackTabMinusButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.11f, -1.43f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.repackPrintHUButton).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase109()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.doigtClickSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.repackPrintHUButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }
        
        if (canPlaySecond)
        {
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase110()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis3bis).enabled = true;

        phaseNum++;
    }

    void Phase111()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis3bis).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase112()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask1).interactable = true;

        phaseNum++;
    }

    void Phase113()
    {
        gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask1).interactable = false;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.returnMecaButton).interactable = true;

        phaseNum++;
    }

    void Phase114()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().CloseMonitorTuto();

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis3bis).enabled = true;

        phaseNum++;
    }

    void Phase115()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        phaseNum++;
    }

    void Phase116()
    {
        if (canPlayFirst)
        {
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis3).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase117()
    {
        canJeter = true;

        phaseNum++;
    }

    void Phase118()
    {
        if (canPlayFirst)
        {
            canJeter = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(-4.67f, 5.51f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.33f, 0.33f); ;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.quitButtonWorkView).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase119()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.quitButtonWorkView).interactable = false;

        phaseNum++;
    }

    void Phase120()
    {
        StartCoroutine(ReturnToMenu(0));

        phaseNum++;
    }
    #endregion
}
