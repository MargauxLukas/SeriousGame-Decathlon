using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementNuages : MonoBehaviour
{
    public float speed;
    public float diffWithPlanete;
    public bool isStar;
    public int nombreEtoiles;

    public List<GameObject> etoilesDisponibles;
    public Transform parentEtoiles;

    private void Start()
    {
        //Placement des étoiles
        for(int i = 0; i < nombreEtoiles; i++)
        {
            GameObject gm = Instantiate(etoilesDisponibles[Random.Range(0, etoilesDisponibles.Count - 1)], parentEtoiles);
            gm.transform.localPosition = new Vector3(Random.Range(10.37f - (12.3f * (1 + ((diffWithPlanete + speed) / speed))), 10.37f *3), Random.Range(-15f, 6f), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStar)
        {
            if (transform.localPosition.x <= 10.37f -(12.3f * (1 + ((Mathf.Abs(diffWithPlanete)+Mathf.Abs(speed))/ Mathf.Abs(speed)))))
            {
                transform.localPosition = new Vector3(10.37f, transform.localPosition.y, 0);
            }
            transform.localPosition += new Vector3(1, 0, 0) * (speed + diffWithPlanete);
        }
        else
        {
            if (transform.localPosition.x >= 10.37f)
            {
                transform.localPosition = new Vector3(-1.97f, transform.localPosition.y, 0);
            }
            transform.localPosition += new Vector3(1, 0, 0) * speed;
        }
    }
}
