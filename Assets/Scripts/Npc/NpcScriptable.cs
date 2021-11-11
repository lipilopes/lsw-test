using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables/Npc", menuName = "Scriptable/Npc", order = 1)]
public class NpcScriptable : MobScriptable
{
    [SerializeField]
    private Dialogue[] _dialogues;
    public  Dialogue[] Dialogues { get { return _dialogues;}}

    public Sprite Portrait(int dialogueIndex)
    {
        return _portrait[(int)Dialogues[dialogueIndex].portraitFeeling];
    }
}
