using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisScript : MonoBehaviour
{
    private bool isMoving;

    public Colis colisScriptable;
    public MenuCirculaire menuCirculaire;

    public bool estSecoue;
    private int changeDirection;
    private bool goRight;

    private float deltaTimeShake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(menuCirculaire.isOpen && isMoving)
        {
            isMoving = false;
        }

        deltaTimeShake += Time.deltaTime;
        if (Input.touchCount > 0)
        {
            touchObject();
            if (isMoving)
            {
                Vector3 ancientPosition = transform.position;

                transform.position = new Vector3(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)).x, transform.position.y, 0);

                //Vérification colis secoué
                if(transform.position.x - ancientPosition.x > 0 && !goRight)
                {
                    goRight = true;
                    if(deltaTimeShake <= 0.2f)
                    {
                        changeDirection++;
                        deltaTimeShake = 0;
                    }
                }
                else if(transform.position.x - ancientPosition.x < 0 && goRight)
                {
                    goRight = false;
                    if (deltaTimeShake <= 0.2f)
                    {
                        changeDirection++;
                        deltaTimeShake = 0;
                    }
                }

                if(changeDirection >= 3)
                {
                    Debug.Log("Est secoué");
                    estSecoue = true;
                }
                else if(deltaTimeShake >= 1f && estSecoue)
                {
                    Debug.Log("Est plus secoué");
                    estSecoue = false;
                }
            }
        }
        else
        {
            isMoving = false;
        }
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider.gameObject == gameObject)
            {
                isMoving = true;
                Debug.Log("Touched it");
            }
        }
    }
}
