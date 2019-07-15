using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneAffichageAnomalieFiche : MonoBehaviour
{
    public Text zoneNombreAnomaliePresente;
    public Text zoneAffichageAnomalie;

    private void Update()
    {
        zoneAffichageAnomalie     .CrossFadeAlpha(0, 2f, false);
        zoneNombreAnomaliePresente.CrossFadeAlpha(0, 2f, false);
    }
}
