using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Dalogue", menuName = "NewDialogue")]
public class Dialogue : ScriptableObject
{
    public PersonnageDialogue persoParlant;
    public PersonnageDialogue persoEcoutant;

    [TextArea(5, 3)]
    public List<string> phraseDites;
    public List<string> choixPossibles;

    public List<Dialogue> dialoguesSuivant;

    public Dialogue ChoixDialogueSuivant(int choix)
    {
        if (dialoguesSuivant[choix - 1] != null)
        {
            return dialoguesSuivant[choix - 1];
        }
        else
        {
            return null;
        }
    }

    public void AttributionDialogue()
    {

    }

}
