using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public static TutoManager instance;
    public DialogueManager dialogueManager;
    public GameObjectsManager gameObjectsManager;
    public List<Dialogue> listDialogues;
    public int phaseNum = 0;
    public int dialogNum = 0;

    [Header("Menu Colis")]
    public bool canJeter = false;
    public bool canVider = false;
    public bool canOuvrirFermer = false;
    public bool canOpenTurnMenu = false;

    [Header("Menu Articles")]
    public bool canColis1 = false;
    public bool canColis2 = false;
    public bool canInfo = false;
    public bool canCloseFicheInfo = false;

    [Header("Launch Dialogue & Phases")]
    public bool canPlayFirst = true;
    public bool canPlaySecond = false;
    public int  interactionNum = 0;

    //Déplacement doigt
    private Vector3 fingerPosition;
    private Vector3 targetPosition;
    private float fingerSpeed;

    //Position Colis
    private Vector3 colisPosition;
    private Vector3 articlesPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
    }

    public void DialogueIsFinished()
    {
        //Debug.Log("Interaction : " + interactionNum + " Phase : " + phaseNum);
        canPlayFirst  = false;
        canPlaySecond = true;

        Manager(interactionNum);
    }

    public void Manager(int interaction)
    {
        interactionNum = interaction;
        Debug.Log("Interaction : " + interactionNum + " Phase : " + phaseNum);
        switch (interaction)
        {
            //Initial State
            case (0):
                switch (phaseNum)
                {
                    case (0):
                        Phase00();
                        break;
                }
                break;

            //Interaction Pédale
            case (1):
                switch (phaseNum)
                {
                    case (1):
                        Phase01();
                        break;

                    case (64):
                        Phase64();
                        break;
                }
                break;

            //Scan HU
            case (2):
                switch (phaseNum)
                {
                    case (2):
                        Phase02();
                        break;
                }
                break;

            //Ouverture Ecran
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
                }
                break;

            //Skip automatique
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
                }
                break;

            //Interaction onglet Recount
            case (5):
                switch (phaseNum)
                {
                    case (10):
                        Phase10();
                        break;

                    case (31):
                        Phase31();
                        break;
                }
                break;

            //Interaction bouton Recount
            case (6):
                switch (phaseNum)
                {
                    case (12):
                        Phase12();
                        break;
                }
                break;

            //Fermeture Ecran
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
                }
                break;

            //Scan RFID
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

            //Ouverture Menu circulaire Colis
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
                }
                break;

            //Interaction bouton OuvrirFermer
            case (10):
                switch (phaseNum)
                {
                    case (21):
                        Phase21();
                        break;

                    case (48):
                        Phase48();
                        break;
                }
                break;

            //Interaction bouton Vider
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

            //Ouverture Menu circulaire Articles
            case (12):
                switch (phaseNum)
                {
                    case (24):
                        Phase24();
                        break;

                    case (28):
                        Phase28();
                        break;
                }
                break;

            //Interaction bouton Info Articles
            case (13):
                switch (phaseNum)
                {
                    case (25):
                        Phase25();
                        break;

                    case (71):
                        Phase71();
                        break;

                    case (72):
                        Phase72();
                        break;
                }
                break;

            //Fermeture Fiche info
            case (14):
                switch (phaseNum)
                {
                    case (27):
                        Phase27();
                        break;
                }
                break;

            //Interaction bouton Colis1
            case (15):
                switch (phaseNum)
                {
                    case (29):
                        Phase29();
                        break;
                }
                break;

            //Interaction bouton Inventory
            case (16):
                switch (phaseNum)
                {
                    case (33):
                        Phase33();
                        break;
                }
                break;

            //Interaction bouton Print HU
            case (17):
                switch (phaseNum)
                {
                    case (34):
                        Phase34();
                        break;
                }
                break;

            //Collage HU
            case (18):
                switch (phaseNum)
                {
                    case (37):
                        Phase37();
                        break;
                }
                break;

            //Interaction toggle End Task
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
                }
                break;

            //Interaction onglet Filling Rate
            case (20):
                switch (phaseNum)
                {
                    case (41):
                        Phase41();
                        break;
                }
                break;

            //Interaction bouton %
            case (21):
                switch (phaseNum)
                {
                    case (43):
                        Phase43();
                        break;
                }
                break;

            //Interaction toggle Can meca open
            case (22):
                switch (phaseNum)
                {
                    case (44):
                        Phase44();
                        break;
                }
                break;

            //Interaction bouton Return to meca
            case (23):
                switch (phaseNum)
                {
                    case (58):
                        Phase58();
                        break;
                }
                break;

            //Renvoi colis droite
            case (24):
                switch (phaseNum)
                {
                    case (60):
                        Phase60();
                        break;
                }
                break;

            //Interaction bouton Tourner
            case (25):
                switch (phaseNum)
                {
                    case (50):
                        Phase50();
                        break;
                }
                break;

            //Interaction flèches
            case (26):
                switch (phaseNum)
                {

                }
                break;

            //Colis bien orienté
            case (27):
                switch (phaseNum)
                {
                    case (51):
                        Phase51();
                        break;
                }
                break;

            //Fermeture menu flèches
            case (28):
                switch (phaseNum)
                {
                    case (52):
                        Phase52();
                        break;
                }
                break;

            //Renvoi colis haut
            case (29):
                switch (phaseNum)
                {
                    case (62):
                        Phase62();
                        break;
                }
                break;

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
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", true);
        }
        else
        {
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).SetBool("endLoop", false);
        }

        yield return new WaitForSeconds(Time.fixedDeltaTime);
        StartCoroutine(MoveDoigt());
    }

    IEnumerator NewPhase(float time)
    {
        yield return new WaitForSeconds(time);

        phaseNum++;
        canPlayFirst = true;
        canPlaySecond = false;

        Manager(4);
    }

        /*****************/
       /*    Colis 1    */
      /*****************/

    void Phase00()
    {
        phaseNum++;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask).transform.localPosition = new Vector2(11.67f,-4);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.circleSpriteMask).transform.localScale = new Vector2(0.9f, 0.9f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask).enabled = true;

        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.1f,-4.5f);
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = true;

        canPlayFirst = true;
        canPlaySecond = false;
    }

    void Phase01()
    {
        if (canPlayFirst)
        {        
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.circleSpriteMask).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(5.3f, 1.24f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.44f, 0.84f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localPosition = new Vector2(8.61f, -1.44f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localScale = new Vector2(1.54f, 1.25f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(5.75f, 1f, 30f);
            fingerPosition = new Vector3(5.75f, 1f, 30f);
            targetPosition = new Vector3(8.75f, -1.4f, 30f);
            fingerSpeed = 4f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = true;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

   void Phase02()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = false;

            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.4f, 2.94f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(2f, 1.2f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(11f, 2.45f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
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
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(6f, 3.5f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(5.8f, 1.15f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase04()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(6.05f, 3.64f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(5.56f, 1.18f);
            
            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(2.24f, -4.23f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.2f, 0.48f);

            StartCoroutine(NewPhase(2.5f));
        }
    }

    void Phase05()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

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

    void Phase06()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().CloseMonitorTuto();

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(6.85f, -2.64f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1f, 0.48f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

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
            canPlayFirst = true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase08()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().OpenMonitorTuto();

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(6.23f, 1.64f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.72f, 0.33f);
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
            Debug.Log("Et dans la 2e phase tu rentres ?");
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(7.75f, 1.22f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase10()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(3.1f, -0.3f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(2.5f, 0.83f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(8.02f, 0.2f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.2f, 0.36f);

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(9.5f, -0.28f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.recountButton).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase12()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.recountButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(0.32f, 1.03f, 30f);
            fingerPosition = new Vector3(0.32f, 1.03f, 30f);
            targetPosition = new Vector3(9.3f, 1.03f, 30f);
            fingerSpeed = 4f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase13()
    {
        if (canPlayFirst)
        {
            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(8.61f, -1.44f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.54f, 1.25f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localPosition = new Vector2(6.85f, -2.64f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localScale = new Vector2(1f, 0.48f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(9.3f, -1.9f, 30f);
            fingerPosition = new Vector3(9.3f, -1.9f, 30f);
            targetPosition = new Vector3(6.72f, -1.9f, 30f);
            fingerSpeed = 8f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase14()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = false;

            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.4f, 2.94f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(2f, 1.2f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(11f, 2.45f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase15()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
            gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(1.77f, 0.17f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.32f, 0.4f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localPosition = new Vector2(2.22f, -4.25f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localScale = new Vector2(1.12f, 0.5f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = true;

            StartCoroutine(NewPhase(3f));
        }
    }

    void Phase16()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask02).enabled = false;

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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3 (0.5f, 0.2f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;

            StartCoroutine(NewPhase(2f));
        }
    }

    void Phase18()
    {
        colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.menuCirculaireColis).transform.localPosition = colisPosition;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.menuCirculaireColis).enabled = true;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.menuColis44SpriteMaskTuto).enabled = true;

        targetPosition = new Vector3(7f, 0.42f, 30f);
        fingerSpeed = 4;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition += (targetPosition - gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition).normalized * Time.fixedDeltaTime * fingerSpeed;

        StartCoroutine(NewPhase(2f));
    }

    void Phase19()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.menuCirculaireColis).enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.menuColis44SpriteMaskTuto).enabled = false;

        fingerSpeed = 0;

        colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3(0.5f, 0.2f, 0);

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

        phaseNum++;
    }

    void Phase20()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

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
            canPlayFirst = true;
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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(2.48f, -1.84f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.76f, 0.4f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(3.16f, -1.89f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticlesColis1).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase24()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

        canInfo = true;

        phaseNum++;
    }

    void Phase25()
    {
        canInfo = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(0.55f, -0.96f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.46f, 1.65f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

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
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(4.31f, -0.91f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            canCloseFicheInfo = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase27()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            canCloseFicheInfo = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(3.16f, -1.89f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase28()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

        canColis1 = true;

        phaseNum++;
    }

    void Phase29()
    {
        if (canPlayFirst)
        {
            canColis1 = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticlesColis1).enabled = false;

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

    void Phase30()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = true;

        phaseNum++;
    }

    void Phase31()
    {
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(8.02f, -0.6f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.2f, 0.36f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(9.36f, -1.05f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.inventoryButton).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase33()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.inventoryButton).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.95f, -0.2f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.2f, 0.36f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.2f, -0.69f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.printHUButton).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase34()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.printHUButton).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = false;

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

    void Phase35()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(7.2f, 2.08f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.52f, 0.66f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

        colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localPosition = colisPosition;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask02).transform.localScale = new Vector2(1.54f, 1.25f);
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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = new Vector3(7.63f, 1.84f, 30f);
            fingerPosition = new Vector3(7.63f, 1.84f, 30f);
            targetPosition = new Vector3(7.63f, -1.58f, 30f);
            fingerSpeed = 4f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase37()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = false;

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

    void Phase38()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(11.64f, 4.56f);
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.4f, 0.38f);
        gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.27f, 4.1f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask1).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase40()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask1).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(2.39f, 1.64f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.72f, 0.33f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(3.96f, 1.11f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.fillTab).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase41()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(4.52f, -0.18f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(3.1f, 1f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            phaseNum++;
            canPlayFirst = true;
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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(3.62f, 0.48f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(0.6f, 0.41f);

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(4.41f, -0.08f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.fill50Button).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase43()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.fill50Button).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.3f, -0.2f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.9f, 0.4f);

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(9.5f, -0.64f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToToggle(gameObjectsManager.mecaOpenToggle).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase44()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToToggle(gameObjectsManager.mecaOpenToggle).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.fillTab).interactable = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.27f, 3.26f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask2).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase45()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask2).interactable = false;

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
            canPlayFirst = true;
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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = colisPosition;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(1.47f, 1.47f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = colisPosition + new Vector3(1.74f, -0.41f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.turnRightButton).interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.turnLeftButton).interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.turnUpButton).interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.turnDownButton).interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftRotationButton).interactable = true;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotationButton).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase51()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;


            gameObjectsManager.GameObjectToButton(gameObjectsManager.turnRightButton).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.turnLeftButton).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.turnUpButton).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.turnDownButton).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.leftRotationButton).interactable = false;
            gameObjectsManager.GameObjectToButton(gameObjectsManager.rightRotationButton).interactable = false;

            canOpenTurnMenu = false;
            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(4.31f, -0.91f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase52()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

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

    void Phase53()
    {
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.27f, 2.4f);
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

        gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask3).interactable = true;

        phaseNum++;
    }

    void Phase54()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToToggle(gameObjectsManager.toggleEndTask3).interactable = false;

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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(10.4f, 2.94f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(2f, 1.2f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;

            Manager(4);
        }
    }

    void Phase56()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

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
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtClick).transform.localPosition = new Vector2(12.32f, -2.25f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = true;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.returnMecaButton).interactable = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase58()
    {
        if (canPlayFirst)
        {
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtClick).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtClick).enabled = false;

            gameObjectsManager.GameObjectToButton(gameObjectsManager.returnMecaButton).interactable = false;

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

    void Phase59()
    {
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;

        colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
        gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3(0.5f, 0.2f, 30f);
        fingerPosition = colisPosition + new Vector3(0.5f, 0.2f, 30f);
        targetPosition = new Vector3(12.74f, -2.05f, 30f);
        fingerSpeed = 4f;
        gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
        gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;
        StartCoroutine(MoveDoigt());

        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

        phaseNum++;
    }

    void Phase60()
    {
        if (canPlayFirst)
        {
            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = false;

            dialogueManager.LoadDialogue(listDialogues[dialogNum]);
            dialogNum++;
        }

        if (canPlaySecond)
        {
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.blackScreen).transform.localPosition = new Vector3(-20.1f, 0.23f, 30f);
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = true;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localPosition = new Vector2(-15.58f, 3.03f);
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.squareSpriteMask01).transform.localScale = new Vector2(2.4f, 1.73f);
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = true;

            phaseNum++;
            canPlayFirst = true;
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
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.blackScreen).enabled = false;
            gameObjectsManager.GameObjectToSpriteMask(gameObjectsManager.squareSpriteMask01).enabled = false;

            colisPosition = gameObjectsManager.GameObjectToTransform(gameObjectsManager.colis1).transform.localPosition;
            gameObjectsManager.GameObjectToTransform(gameObjectsManager.doigtStay).transform.localPosition = colisPosition + new Vector3(0.5f, 0.2f, 30f);
            fingerPosition = colisPosition + new Vector3(0.5f, 0.2f, 30f);
            targetPosition = new Vector3(-19.97f, 2.5f, 30f);
            fingerSpeed = 4f;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = true;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = true;
            StartCoroutine(MoveDoigt());

            gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis1).enabled = true;

            phaseNum++;
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

    void Phase62()
    {
        if (canPlayFirst)
        {
            fingerSpeed = 0;
            gameObjectsManager.GameObjectToSpriteRenderer(gameObjectsManager.doigtStay).enabled = false;
            gameObjectsManager.GameObjectToAnimator(gameObjectsManager.doigtStay).enabled = false;

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
            canPlayFirst = true;
            canPlaySecond = false;
        }
    }

            /*****************/
           /*   Colis 2     */
          /*****************/

    void Phase64()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        Manager(4);
    }

    void Phase65()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = true;
    }

    void Phase66()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().OpenBigMonitor();
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Enable Fond noir
        //New position (PCB HU + PCBs écran) + Enable Spritemask x2
        Manager(4);
    }

    void Phase67()
    {
        phaseNum++;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Disable fond noir
        //Disable Spritemask
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
    }

    void Phase68()
    {
        phaseNum++;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = true;
        canOuvrirFermer = true;
        canVider = true;
    }

    void Phase69()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis2).enabled = false;
        canOuvrirFermer = false;
        canVider = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        Manager(4);
    }

    void Phase70()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles1Colis2).enabled = true;
        canInfo = true;
    }

    void Phase71()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles1Colis2).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles2Colis2).enabled = true;
    }

    void Phase72()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles2Colis2).enabled = false;
        canInfo = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
    }

    void Phase73()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.screen).interactable = true;
    }
}
