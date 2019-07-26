using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonMettreSurPalette : MonoBehaviour
{
    public TapisRoulant tapisRoule;
    public void PoserSurPalette()
    {
        if(TutoManagerRecep.instance != null) { TutoManagerRecep.instance.Manager(13); }
        if(tapisRoule.lastColis != null)
        {
            if(tapisRoule.lastColis.GetComponent<ScriptColisRecep>() != null)
            {
                if(!tapisRoule.lastColis.GetComponent<ScriptColisRecep>().colisScriptable.estAbime && tapisRoule.lastColis.GetComponent<ScriptColisRecep>().colisScriptable.carton.codeRef != "CBGrand")
                {
                    Scoring.instance.RecepRenvoieColis();
                }
                else
                {
                    Scoring.instance.RecepMalus(100);
                    Scoring.instance.AffichageErreur("Tu as posé un colis abimé ou trop grand sur le convoyeur");
                }
                tapisRoule.turnMenu.SetActive(false);
                tapisRoule.colisSurLeTapis.Remove(tapisRoule.lastColis);
                Destroy(tapisRoule.lastColis);
            }
        }
    }
}
