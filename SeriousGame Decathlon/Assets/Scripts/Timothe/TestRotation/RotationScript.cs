using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public GameObject cube;

    public List<SquareFace> squareList;
    SquareFace actualFace;

    // Start is called before the first frame update
    void Start()
    {
        squareList = new List<SquareFace>();
        squareList.Add(CreateFace(0, "Up", true));
        squareList.Add(CreateFace(0, "Down", false));
        squareList.Add(CreateFace(0, "Right", false));
        squareList.Add(CreateFace(0, "Left", false));
        squareList.Add(CreateFace(0, "Forward", false));
        squareList.Add(CreateFace(180, "Backward", false));

        squareList[0] = CreateVoison(squareList[0],squareList[5], squareList[4], squareList[2], squareList[3]);
        squareList[1] = CreateVoison(squareList[1], squareList[4], squareList[5], squareList[2], squareList[3]);
        squareList[2] = CreateVoison(squareList[2], squareList[0], squareList[1], squareList[4], squareList[5]);
        squareList[3] = CreateVoison(squareList[3], squareList[0], squareList[1], squareList[5], squareList[4]);
        squareList[4] = CreateVoison(squareList[4], squareList[0], squareList[1], squareList[3], squareList[2]);
        squareList[5] = CreateVoison(squareList[5], squareList[0], squareList[1], squareList[2], squareList[3]);

        actualFace = GetCurrentFace();
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
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
        }
    }

    void ChangeRotation(int xAxis, int yAxis, int rotation)
    {
        //int nbFace = GetCurrentFaceId();
        actualFace.fullRotation = actualFace.fullRotation % 360;
        int nbQuaterRotateMore = Mathf.RoundToInt(actualFace.fullRotation / 90)%4;

        if (actualFace != null)
        {
            actualFace.isCurrentlyPick = false;
            SquareFace newFace = new SquareFace();
            if (xAxis != 0)
            {
                if (xAxis > 0)
                {
                    switch(nbQuaterRotateMore)
                    {
                        case 0:
                            newFace = actualFace.rightVoisin;
                            GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                            GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                            break;
                        case 1:
                            newFace = actualFace.upVoisin;
                            GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                            GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                            break;
                        case 2:
                            newFace = actualFace.leftVoisin;
                            GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                            GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                            break;
                        case 3:
                            newFace = actualFace.downVoisin;
                            GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                            GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                            break;
                    }
                }
                else
                {
                    switch (nbQuaterRotateMore)
                    {
                        case 0:
                            newFace = actualFace.leftVoisin;
                            GetVoisonFromRotation(actualFace, "Right").fullRotation -= 90;
                            GetVoisonFromRotation(actualFace, "Left").fullRotation -= 90;
                            break;
                        case 1:
                            newFace = actualFace.downVoisin;
                            GetVoisonFromRotation(actualFace, "Up").fullRotation -= 90;
                            GetVoisonFromRotation(actualFace, "Down").fullRotation -= 90;
                            break;
                        case 2:
                            newFace = actualFace.rightVoisin;
                            GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                            GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                            break;
                        case 3:
                            newFace = actualFace.upVoisin;
                            GetVoisonFromRotation(actualFace, "Up").fullRotation += 90;
                            GetVoisonFromRotation(actualFace, "Down").fullRotation += 90;
                            break;
                    }
                }
                actualFace = newFace;
            }
            else if (yAxis != 0)
            {
                if(yAxis > 0)
                {
                    switch (nbQuaterRotateMore)
                    {
                        case 0:
                            newFace = actualFace.upVoisin;
                            GetVoisonFromRotation(actualFace, "Right").fullRotation += 90;
                            GetVoisonFromRotation(actualFace, "Left").fullRotation += 90;
                            break;
                        case 1:
                            newFace = actualFace.leftVoisin;
                            actualFace.upVoisin.fullRotation -= 90;
                            actualFace.downVoisin.fullRotation -= 90;
                            break;
                        case 2:
                            newFace = actualFace.downVoisin;
                            actualFace.rightVoisin.fullRotation -= 90;
                            actualFace.leftVoisin.fullRotation -= 90;
                            break;
                        case 3:
                            newFace = actualFace.rightVoisin;
                            actualFace.upVoisin.fullRotation += 90;
                            actualFace.downVoisin.fullRotation += 90;
                            break;
                    }
                }
                else
                {
                    switch (nbQuaterRotateMore)
                    {
                        case 0:
                            newFace = actualFace.downVoisin;
                            actualFace.rightVoisin.fullRotation -= 90;
                            actualFace.leftVoisin.fullRotation -= 90;
                            break;
                        case 1:
                            newFace = actualFace.rightVoisin;
                            actualFace.upVoisin.fullRotation += 90;
                            actualFace.downVoisin.fullRotation += 90;
                            break;
                        case 2:
                            newFace = actualFace.upVoisin;
                            actualFace.rightVoisin.fullRotation += 90;
                            actualFace.leftVoisin.fullRotation += 90;
                            break;
                        case 3:
                            newFace = actualFace.leftVoisin;
                            actualFace.upVoisin.fullRotation -= 90;
                            actualFace.downVoisin.fullRotation -= 90;
                            break;
                    }
                }
                actualFace = newFace;
            }
            else if (rotation != 0)
            {
                if(rotation > 0)
                {
                    actualFace.fullRotation += 90;
                    //actualFace.upVoisin.upVoisin.fullRotation += 90;
                }
                else
                {
                    actualFace.fullRotation -= 90;
                    //actualFace.upVoisin.upVoisin.fullRotation -= 90;
                }
            }
            actualFace.isCurrentlyPick = true;
            Debug.Log(actualFace.face);
            Debug.Log(actualFace.fullRotation);
        }
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
                        break;
                    case 1:
                        return currentFace.leftVoisin;
                        break;
                    case 2:
                        return currentFace.downVoisin;
                        break;
                    case 3:
                        return currentFace.rightVoisin;
                        break;
                }
                break;
            case "Down":
                switch (nbRota)
                {
                    case 0:
                        return currentFace.downVoisin;
                        break;
                    case 1:
                        return currentFace.rightVoisin;
                        break;
                    case 2:
                        return currentFace.upVoisin;
                        break;
                    case 3:
                        return currentFace.leftVoisin;
                        break;
                }
                break;
            case "Right":
                switch (nbRota)
                {
                    case 0:
                        return currentFace.rightVoisin;
                        break;
                    case 1:
                        return currentFace.upVoisin;
                        break;
                    case 2:
                        return currentFace.leftVoisin;
                        break;
                    case 3:
                        return currentFace.downVoisin;
                        break;
                }
                break;
            case "Left":
                switch (nbRota)
                {
                    case 0:
                        return currentFace.leftVoisin;
                        break;
                    case 1:
                        return currentFace.downVoisin;
                        break;
                    case 2:
                        return currentFace.rightVoisin;
                        break;
                    case 3:
                        return currentFace.upVoisin;
                        break;
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

