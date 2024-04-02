using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class RestDialogue : ScriptableObject
{

    [TextArea]
   public List<string> ListOfDialogues;
    public List<bool> DialogueOrder;

}
