using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************************
 *      Rescence tout ce qui concerne les places de tapis et les colis s'y trouvant       *
 ******************************************************************************************/
public class CartonVideLink : MonoBehaviour
{
    public bool isFree1 = true;
    public bool isFree2 = true;
    public bool isFree3 = true;

    public GameObject tapis1GameObject;
    public GameObject tapis2GameObject;
    public GameObject tapis3GameObject;

    public GameObject ColisPrefab;
    public ManagerColisVider managerVide;

    public RemplissageColisGTP[] csTab = new RemplissageColisGTP[3];

    public void PutAnotherColis(Vector3 position)
    {
        GameObject gm = Instantiate(ColisPrefab, position, Quaternion.identity);
        gm.GetComponent<CartonVide>().cvl = this;
        gm.GetComponent<CartonVide>().mcv = managerVide;
    }
}
