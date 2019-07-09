using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public List<SquareFace> squareList;
    public SquareFace actualFace;

    public GameObject cartonObj;

    public List<Sprite> cartonsSprites;
    public SpriteRenderer carton;

    private bool lastTopViewFaceIsWrong;

    public void TouchOtherColis(SquareFace newFace)
    {
        actualFace = newFace;
    }


    // Start is called before the first frame update
    void Start()
    {
        lastTopViewFaceIsWrong = false;

        squareList = new List<SquareFace>();
        squareList.Add(CreateFace(0, "Up", true));
        squareList.Add(CreateFace(0, "Down", false));
        squareList.Add(CreateFace(0, "Right", false));
        squareList.Add(CreateFace(0, "Left", false));
        squareList.Add(CreateFace(0, "Forward", false));
        squareList.Add(CreateFace(0, "Backward", false));

        squareList[0] = CreateVoison(squareList[0],squareList[5], squareList[4], squareList[2], squareList[3]);
        squareList[1] = CreateVoison(squareList[1], squareList[4], squareList[5], squareList[2], squareList[3]);
        squareList[2] = CreateVoison(squareList[2], squareList[0], squareList[1], squareList[4], squareList[5]);
        squareList[3] = CreateVoison(squareList[3], squareList[0], squareList[1], squareList[5], squareList[4]);
        squareList[4] = CreateVoison(squareList[4], squareList[0], squareList[1], squareList[3], squareList[2]);
        squareList[5] = CreateVoison(squareList[5], squareList[0], squareList[1], squareList[2], squareList[3]);

        actualFace = GetCurrentFace();

        if (cartonObj != null)
        {
            cartonsSprites = cartonObj.GetComponent<ColisScript>().colisScriptable.carton.spriteCartonsListe;
            carton = cartonObj.GetComponent<SpriteRenderer>();
        }
    }

    private void resetAll()
    {
        lastTopViewFaceIsWrong = false;

        squareList = new List<SquareFace>();
        squareList.Add(CreateFace(0, "Up", false));
        squareList.Add(CreateFace(0, "Down", false));
        squareList.Add(CreateFace(0, "Right", false));
        squareList.Add(CreateFace(0, "Left", false));
        squareList.Add(CreateFace(0, "Forward", false));
        squareList.Add(CreateFace(0, "Backward", false));

        squareList[0] = CreateVoison(squareList[0], squareList[5], squareList[4], squareList[2], squareList[3]);
        squareList[1] = CreateVoison(squareList[1], squareList[4], squareList[5], squareList[2], squareList[3]);
        squareList[2] = CreateVoison(squareList[2], squareList[0], squareList[1], squareList[4], squareList[5]);
        squareList[3] = CreateVoison(squareList[3], squareList[0], squareList[1], squareList[5], squareList[4]);
        squareList[4] = CreateVoison(squareList[4], squareList[0], squareList[1], squareList[3], squareList[2]);
        squareList[5] = CreateVoison(squareList[5], squareList[1], squareList[0], squareList[2], squareList[3]);
    }

    SquareFace CreateFace(float fullRotation, string face, bool isCurrentlyPick)
    {
        SquareFace newFace = new SquareFace();
        newFace.fullRotation = fullRotation;
        newFace.face = face;
        newFace.isCurrentlyPick = isCurrentlyPick;
        return newFace;
    }

    SquareFace CreateVoison(SquareFace currentFace, SquareFace upVoisin, SquareFace downVoisin, SquareFace rightVoisin, SquareFace leftVoisin)
    {
        SquareFace newFace = currentFace;
        newFace.upVoisin = upVoisin;
        newFace.downVoisin = downVoisin;
        newFace.rightVoisin = rightVoisin;
        newFace.leftVoisin = leftVoisin;
        return newFace;
    }

    public void UpdateSprite(List<Sprite> spriteCartonListe, SpriteRenderer spriteCarton)
    {
        if (spriteCartonListe.Count > 0)
        {
            switch (actualFace.face)
            {
                case "Up":
                    spriteCarton.sprite = spriteCartonListe[0];
                    break;
                case "Down":
                    spriteCarton.sprite = spriteCartonListe[1];
                    break;
                case "Right":
                    spriteCarton.sprite = spriteCartonListe[2];
                    break;
                case "Left":
                    spriteCarton.sprite = spriteCartonListe[3];
                    break;
                case "Forward":
                    spriteCarton.sprite = spriteCartonListe[4];
                    break;
                case "Backward":
                    spriteCarton.sprite = spriteCartonListe[5];
                    break;
            }
            //Debug.Log(spriteCartonListe[0]);
        }
        if(spriteCarton != null)
            spriteCarton.gameObject.transform.eulerAngles = new Vector3(0, 0, -actualFace.fullRotation);
        //Debug.Log(actualFace.face);
        //Debug.Log(actualFace.fullRotation);
    }

    public SquareFace UpdateVueHaut(List<Sprite> spriteCartonListe, SpriteRenderer spriteCarton, SquareFace theNewFace)
    {
        if (lastTopViewFaceIsWrong)
        {
            lastTopViewFaceIsWrong = false;
            int xAxis = xAxisMajeur;
            int yAxis = yAxisMajeur;
            int rotation = RotaAxis;

            if (xAxisMajeur > 0)
            {
                rotation = 90;
                xAxis = 0;
            }
            else if (xAxisMajeur < 0)
            {
                rotation = -90;
                xAxis = 0;
            }

            if (RotaAxis > 0)
            {
                xAxis = 1;
                rotation = 0;
            }
            else if (RotaAxis < 0)
            {
                xAxis = -1;
                rotation = 0;
            }

            Debug.Log(xAxis);
            Debug.Log(yAxis);
            Debug.Log(rotation);

            theNewFace.fullRotation = theNewFace.fullRotation % 360;
            if (theNewFace.fullRotation >= 360)
            {
                theNewFace.fullRotation -= 360;
            }
            else if (theNewFace.fullRotation < 0)
            {
                theNewFace.fullRotation += 360;
            }
            int nbQuaterRotateMore = Mathf.RoundToInt(theNewFace.fullRotation / 90) % 4;

            if (theNewFace != null)
            {
                theNewFace.isCurrentlyPick = false;
                SquareFace newFace = new SquareFace();
                if (xAxis != 0)
                {
                    if (xAxis > 0)
                    {
                        newFace = theNewFace.rightVoisin;
                        /*switch(nbQuaterRotateMore)
                        {
                            case 0:
                                newFace = actualFace.rightVoisin;
                                //GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                                //GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                                break;
                            case 1:
                                newFace = actualFace.upVoisin;
                                //GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                                //GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                                break;
                            case 2:
                                newFace = actualFace.leftVoisin;
                                //GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                                //GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                                break;
                            case 3:
                                newFace = actualFace.downVoisin;
                                //GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                                //GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                                break;
                        }*/
                    }
                    else
                    {
                        newFace = theNewFace.leftVoisin;
                        /*switch (nbQuaterRotateMore)
                        {
                            case 0:
                                newFace = actualFace.leftVoisin;
                                //GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                                //GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                                break;
                            case 1:
                                newFace = actualFace.downVoisin;
                                //GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                                //GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                                break;
                            case 2:
                                newFace = actualFace.rightVoisin;
                                //GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                                //GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                                break;
                            case 3:
                                newFace = actualFace.upVoisin;
                                //GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                                //GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                                break;
                        }*/
                    }
                    theNewFace = newFace;
                }
                else if (yAxis != 0)
                {
                    if (yAxis > 0)
                    {
                        newFace = theNewFace.upVoisin;
                        /*switch (nbQuaterRotateMore)
                        {
                            case 0:
                                newFace = actualFace.upVoisin;
                                //GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                                //GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                                break;
                            case 1:
                                newFace = actualFace.leftVoisin;
                                //GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                                //GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                                break;
                            case 2:
                                newFace = actualFace.downVoisin;
                                //GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                                //GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                                break;
                            case 3:
                                newFace = actualFace.rightVoisin;
                                //GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                                //GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                                break;
                        }*/
                    }
                    else
                    {
                        newFace = theNewFace.downVoisin;
                        /*switch (nbQuaterRotateMore)
                        {
                            case 0:
                                newFace = actualFace.downVoisin;
                                GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                                GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                                break;
                            case 1:
                                newFace = actualFace.rightVoisin;
                                GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                                GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                                break;
                            case 2:
                                newFace = actualFace.upVoisin;
                                GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                                GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                                break;
                            case 3:
                                newFace = actualFace.leftVoisin;
                                GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                                GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                                break;
                        }*/
                    }
                    theNewFace = newFace;
                }
                else if (rotation != 0)
                {
                    if (rotation > 0)
                    {
                        theNewFace.fullRotation += 90;
                        //GetVoisonFromRotation(GetVoisonFromRotation(actualFace, "Right"), "Right").fullRotation += 90;
                    }
                    else
                    {
                        theNewFace.fullRotation -= 90;
                        //GetVoisonFromRotation(GetVoisonFromRotation(actualFace, "Right"), "Right").fullRotation += 90;
                    }
                }
                theNewFace.isCurrentlyPick = true;
            }

            if (spriteCartonListe.Count > 0)
            {
                switch (theNewFace.face)
                {
                    case "Up":
                        Debug.Log("Up affichage");
                        spriteCarton.sprite = spriteCartonListe[0];
                        break;
                    case "Down":
                        Debug.Log("Down affichage");
                        spriteCarton.sprite = spriteCartonListe[1];
                        break;
                    case "Right":
                        Debug.Log("Right affichage");
                        spriteCarton.sprite = spriteCartonListe[2];
                        break;
                    case "Left":
                        Debug.Log("Left affichage");
                        spriteCarton.sprite = spriteCartonListe[3];
                        break;
                    case "Forward":
                        Debug.Log("For affichage");
                        spriteCarton.sprite = spriteCartonListe[4];
                        break;
                    case "Backward":
                        Debug.Log("Back affichage");
                        spriteCarton.sprite = spriteCartonListe[5];
                        break;
                }
            }
            if (spriteCarton != null)
                spriteCarton.gameObject.transform.eulerAngles = new Vector3(0, 0, -theNewFace.fullRotation);
            //Debug.Log(actualFace.face);
            //Debug.Log(actualFace.fullRotation);
            Debug.Log(theNewFace);
            return theNewFace;
        }
        return theNewFace;
    }

    public Sprite GetUpFace(List<Sprite> spriteCartonListe)
    {
        switch (actualFace.upVoisin.face)
        {
            case "Up":
                //Debug.Log("Up affichage");
                return spriteCartonListe[0];
            case "Down":
                //Debug.Log("Down affichage");
                return spriteCartonListe[1];
            case "Right":
                //Debug.Log("Right affichage");
                return spriteCartonListe[2];
            case "Left":
                //Debug.Log("Left affichage");
                return spriteCartonListe[3];
            case "Forward":
                //Debug.Log("For affichage");
                return spriteCartonListe[4];
            case "Backward":
                //Debug.Log("Back affichage");
                return spriteCartonListe[5];
        }

        return spriteCartonListe[5];
    }

    public void ColisEnter()
    {
        if (cartonObj != null)
        {
            cartonsSprites = cartonObj.GetComponent<ColisScript>().colisScriptable.carton.spriteCartonsListe;
            carton = cartonObj.GetComponent<SpriteRenderer>();
        }

        if (cartonObj.GetComponent<ColisScript>().colisScriptable.isBadOriented)
        {
            resetAll();
            squareList[5].isCurrentlyPick = true;
            actualFace = GetCurrentFace();
            UpdateSprite(cartonsSprites, carton);
            cartonObj.GetComponent<ColisScript>().colisScriptable.UpdateRotation(squareList);
            cartonObj.GetComponent<ColisScript>().Tourner();
        }
        else
        {
            resetAll();
            squareList[4].isCurrentlyPick = true;
            actualFace = GetCurrentFace();
            //Debug.Log(actualFace.face);
            UpdateSprite(cartonsSprites, carton);
            cartonObj.GetComponent<ColisScript>().colisScriptable.UpdateRotation(squareList);
            cartonObj.GetComponent<ColisScript>().Tourner();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeRotation(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeRotation(-1, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            ChangeRotation(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeRotation(0, -1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeRotation(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeRotation(0, 0, -90);
            UpdateSprite(new List<Sprite>(), carton);
        }*/
    }

    public void GetxAxis(int xAxis)
    {
        xAxisMajeur = xAxis; 
    }

    public void GetyAxis(int xAxis)
    {
        yAxisMajeur = xAxis;
    }

    public void GetRotaAxis(int xAxis)
    {
        RotaAxis = xAxis;
    }

    private int xAxisMajeur;
    private int yAxisMajeur;
    private int RotaAxis;

    public void ChangeRotation()
    {
        if (TutoManager.instance != null) {TutoManager.instance.Manager(26);}
        if (cartonObj != null)
        {
            cartonsSprites = cartonObj.GetComponent<ColisScript>().colisScriptable.carton.spriteCartonsListe;
            carton = cartonObj.GetComponent<SpriteRenderer>();
        }

        int xAxis = xAxisMajeur;
        int yAxis = yAxisMajeur;
        int rotation = RotaAxis;
        //int nbFace = GetCurrentFaceId();
        actualFace.fullRotation = actualFace.fullRotation % 360;
        if(actualFace.fullRotation >= 360)
        {
            actualFace.fullRotation -= 360;
        }
        else if(actualFace.fullRotation < 0)
        {
            actualFace.fullRotation += 360;
        }
        int nbQuaterRotateMore = Mathf.RoundToInt(actualFace.fullRotation / 90)%4;

        if (actualFace != null)
        {
            Debug.Log("Test Face : " + actualFace.face);
            actualFace.isCurrentlyPick = false;
            SquareFace newFace = new SquareFace();
            if (xAxis != 0)
            {
                if (xAxis > 0)
                {
                    newFace = actualFace.rightVoisin;
                    /*switch(nbQuaterRotateMore)
                    {
                        case 0:
                            newFace = actualFace.rightVoisin;
                            //GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                            //GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                            break;
                        case 1:
                            newFace = actualFace.upVoisin;
                            //GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                            //GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                            break;
                        case 2:
                            newFace = actualFace.leftVoisin;
                            //GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                            //GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                            break;
                        case 3:
                            newFace = actualFace.downVoisin;
                            //GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                            //GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                            break;
                    }*/
                }
                else
                {
                    newFace = actualFace.leftVoisin;
                    /*switch (nbQuaterRotateMore)
                    {
                        case 0:
                            newFace = actualFace.leftVoisin;
                            //GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                            //GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                            break;
                        case 1:
                            newFace = actualFace.downVoisin;
                            //GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                            //GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                            break;
                        case 2:
                            newFace = actualFace.rightVoisin;
                            //GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                            //GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                            break;
                        case 3:
                            newFace = actualFace.upVoisin;
                            //GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                            //GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                            break;
                    }*/
                }
                actualFace = newFace;
            }
            else if (yAxis != 0)
            {
                if(yAxis > 0)
                {
                    newFace = actualFace.upVoisin;
                    /*switch (nbQuaterRotateMore)
                    {
                        case 0:
                            newFace = actualFace.upVoisin;
                            //GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                            //GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                            break;
                        case 1:
                            newFace = actualFace.leftVoisin;
                            //GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                            //GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                            break;
                        case 2:
                            newFace = actualFace.downVoisin;
                            //GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                            //GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                            break;
                        case 3:
                            newFace = actualFace.rightVoisin;
                            //GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                            //GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                            break;
                    }*/
                }
                else
                {
                    newFace = actualFace.downVoisin;
                    /*switch (nbQuaterRotateMore)
                    {
                        case 0:
                            newFace = actualFace.downVoisin;
                            GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                            GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                            break;
                        case 1:
                            newFace = actualFace.rightVoisin;
                            GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                            GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                            break;
                        case 2:
                            newFace = actualFace.upVoisin;
                            GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                            GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                            break;
                        case 3:
                            newFace = actualFace.leftVoisin;
                            GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                            GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                            break;
                    }*/
                }
                actualFace = newFace;
            }
            else if (rotation != 0)
            {
                if(rotation > 0)
                {
                    actualFace.fullRotation += 90;
                    //GetVoisonFromRotation(GetVoisonFromRotation(actualFace, "Right"), "Right").fullRotation += 90;
                }
                else
                {
                    actualFace.fullRotation -= 90;
                    //GetVoisonFromRotation(GetVoisonFromRotation(actualFace, "Right"), "Right").fullRotation += 90;
                }
            }
            actualFace.isCurrentlyPick = true;
        }
        UpdateSprite(cartonsSprites, carton);
        cartonObj.GetComponent<ColisScript>().colisScriptable.UpdateRotation(squareList);
        cartonObj.GetComponent<ColisScript>().Tourner();

        lastTopViewFaceIsWrong = true;
    }

    SquareFace GetVoisonFromRotation(SquareFace currentFace, string faceNeeded)
    {
        currentFace.fullRotation = currentFace.fullRotation % 360;
        int nbRota = Mathf.RoundToInt(currentFace.fullRotation / 90) % 4;
        switch (faceNeeded)
        {
            case "Up":
                switch(nbRota)
                {
                    case 0:
                        return currentFace.upVoisin;
                    case 1:
                        return currentFace.leftVoisin;
                    case 2:
                        return currentFace.downVoisin;
                    case 3:
                        return currentFace.rightVoisin;
                }
                break;
            case "Down":
                switch (nbRota)
                {
                    case 0:
                        return currentFace.downVoisin;
                    case 1:
                        return currentFace.rightVoisin;
                    case 2:
                        return currentFace.upVoisin;
                    case 3:
                        return currentFace.leftVoisin;
                }
                break;
            case "Right":
                switch (nbRota)
                {
                    case 0:
                        return currentFace.rightVoisin;
                    case 1:
                        return currentFace.upVoisin;
                    case 2:
                        return currentFace.leftVoisin;
                    case 3:
                        return currentFace.downVoisin;
                }
                break;
            case "Left":
                switch (nbRota)
                {
                    case 0:
                        return currentFace.leftVoisin;
                    case 1:
                        return currentFace.downVoisin;
                    case 2:
                        return currentFace.rightVoisin;
                    case 3:
                        return currentFace.upVoisin;
                }
                break;
        }
        return null;
    }

    SquareFace GetCurrentFace()
    {
        foreach(SquareFace face in squareList)
        {
            if(face.isCurrentlyPick)
            {
                return face;
            }
        }
        return null;
    }

    int GetCurrentFaceId()
    {
        for(int i = 0; i < squareList.Count; i++)
        {
            if(squareList[i].isCurrentlyPick)
            {
                return i;
            }
        }
        return -1;
    }
}

public class SquareFace
{
    public float rotationX;
    public float rotationY;
    public float fullRotation;
    public string face;
    public SquareFace upVoisin;
    public SquareFace downVoisin;
    public SquareFace rightVoisin;
    public SquareFace leftVoisin;
    public bool isCurrentlyPick;
}

