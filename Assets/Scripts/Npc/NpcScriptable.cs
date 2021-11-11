using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables/Npc", menuName = "Scriptable/Npc", order = 1)]
public class NpcScriptable : MobScriptable
{
    [SerializeField] Dialogue[] _dialogues;
    public Dialogue[] Dialogues { get { return _dialogues;}}
}
