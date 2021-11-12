using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public enum NpcType
{
    None,
    Speaker,
    Shopkeeper,//dialog + shop
    Quest
}

[System.Serializable]
public class NpcQuest
{
    public bool             sumStep=true;
    public int              step;
    public QuestScriptable  quest;
    public List<Dialogue>   dialogues;
}

[CreateAssetMenu(fileName = "Scriptables/Npc", menuName = "Scriptable/Npc", order = 1)]
public class NpcScriptable : MobScriptable
{
    [Header("NPC")]
    [SerializeField]
    private NpcType _npcType;
    public  NpcType Type { get { return _npcType;}}

    [Header("Speaker")]
    [SerializeField]
    private List<Dialogue> _dialogues;
    public  List<Dialogue> Dialogues { get { return _dialogues;}}

    public Sprite Portrait(int dialogueIndex)
    {
        return _portrait[(int)Dialogues[dialogueIndex].portraitFeeling];
    }

    [Header("Shopkeeper")]
    [SerializeField]
    private List<ClothesScriptable> storeList = new List<ClothesScriptable>();
    public  List<ClothesScriptable> StoreList { get { return storeList; } }

    [Header("Quest")]
    [SerializeField]
    private List<NpcQuest> questList = new List<NpcQuest>();
    public  List<NpcQuest> Quests { get { return questList; } } 
}
