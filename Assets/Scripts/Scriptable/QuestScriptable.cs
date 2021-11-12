using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StepQuest
{
    public NpcScriptable npcTalk;

}

[CreateAssetMenu(fileName = "Quest_", menuName = "Scriptable/Quest", order = 1)]
public class QuestScriptable : ScriptableObject
{
    [SerializeField]
    private string _questName;
    public  string Name { get { return _questName;}}

    [SerializeField,TextArea]
    private string _questDescription;
    public  string Description { get { return _questDescription;}}
    [Space]
    [SerializeField]
    private int _bonusCoinComplete;
    public  int BonusCoinComplete { get { return _bonusCoinComplete;}}
    [Space]
    [SerializeField]
    private int _bonusFriendPointCompletete;
    public  int BonusFriendPointComplete { get { return _bonusFriendPointCompletete;}}
    [Space]
    [SerializeField]
    private StepQuest[] _steps;
    public  StepQuest[] Steps { get { return _steps;}}

    public int StepsToComplete { get { return _steps.Length-1;} }
}
