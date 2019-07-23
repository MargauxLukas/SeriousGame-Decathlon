using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerColisVider : MonoBehaviour
{
    private List<bool> etatColis = new List<bool>();

    public GameObject colisAnimationVenir;

    public List<Transform> positionParEmmplacements;
    public List<GameObject> emplacementsScripts;

    public List<Colis> colisVider;
    private int nbCurrentColis = 0;

    // Start is called before the first frame update
    void Start()
    {
        etatColis.Add(false);
        etatColis.Add(false);
        FaireVenirNouveauColis(0);
    }

    void FaireVenirNouveauColis(int emplacement)
    {
        etatColis[emplacement] = true;
        StartCoroutine(colisAnimationVenir.GetComponent<AnimationFaireVenirColis>().AnimationColis(emplacementsScripts[emplacement]));
    }


    void FairePartirUnColis(int emplacement)
    {
        etatColis[emplacement] = false;
        emplacementsScripts[emplacement].SetActive(false);
        if(colisVider != null)
        {
            emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis = colisVider[nbCurrentColis];
            nbCurrentColis++;
        }
        if (emplacementsScripts[(emplacement + 1) % 2] != null)
        {
            ActiverAutreColis((emplacement + 1) % 2);
        }
    }

    IEnumerator ActiverAutreColis(int emplacement)
    {
        for(int m = 0; m < 100; m++)
        {
            emplacementsScripts[emplacement].transform.position += new Vector3(0, 1, 0)  * Time.deltaTime * 0.3f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
