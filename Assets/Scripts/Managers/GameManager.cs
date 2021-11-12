using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public enum Emotions
{
    None,
    Default,
    Angry,
    Love,
    Exclamation_Mark,
    Question_Mark,
    Speaking,
}

[System.Serializable] 
public enum PortraitFeeling 
{
    Idle,
    Smile,
    Talk,
    Angry
}

[System.Serializable]
public class StoreData
{
    public ClothesScriptable    clothe;
    public int                  price;
    public bool                 showInShop = true;
}

[System.Serializable]
public class QuestManager
{
    public QuestScriptable    quest;
    public int                currentStep   = -1;//-1 not started
    public bool               completed     = false;

    public void SetStep(int step,bool save=true)
    {
        currentStep = step;
        if(currentStep >= quest.StepsToComplete)
            completed = true;
        
        if(save)
            PlayerPrefs.SetInt(quest.name+"Step",currentStep);
    }
}



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    [SerializeField] 
    private GameObject              player;
    MobsClothes playerClothes;
    [SerializeField]
    private int                     playerCoins;
    [SerializeField]
    private int                     playerFriendsPoints;
    [SerializeField]
    private List<ClothesScriptable> playerInventory = new List<ClothesScriptable>();

    [Header("Data")]
    [SerializeField]
    private List<StoreData> storeDatas = new List<StoreData>();

    [Header("Questions")]
    [SerializeField]
    private List<QuestManager> quests = new List<QuestManager>();

    [Header("Emotions")]
    [SerializeField] 
    private Sprite[] emotions;

    public GameObject   Player          { get { return player; } }
    public int          Coins           { get { return playerCoins; } }
    public int          FriendsPoints   { get { return playerFriendsPoints; } }

    public  List<StoreData>         GetStoreDatas       { get { return storeDatas; } }
    //public  List<StoreData> GetShowInStore            { get { return storeDatas.FindAll(x => x.showInShop == true); } }
    public  List<ClothesScriptable> GetInventory        { get { return playerInventory; } }
    public  MobsClothes             PlayerClothes       { get { return playerClothes; } }
    public  int                     PlayerCoins         { get { return playerCoins; } }
    public  int                     PlayerFriendsPoints { get { return playerFriendsPoints; } }

    public bool PlayerEquipeCloth(ClothesScriptable item)
    {
       return PlayerClothes.ChangeClothe(item);
    }

    public bool CanEquipeCloth(ClothesScriptable item)
    {
        switch (item.ClothesType)
        {
            default: return true;
            case ClothesEnum.Tshirt:  return !(item == PlayerClothes.Tshirt);
            case ClothesEnum.Glasses: return !(item == PlayerClothes.Glasses);
        }
    }

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);  

        playerClothes = Player.GetComponent<MobsClothes>(); 

        LoadPlayerPrefs(); 
    }

    void LoadPlayerPrefs()
    {
        playerCoins          =   PlayerPrefs.GetInt("PlayerCoins",0);
        playerFriendsPoints  =   PlayerPrefs.GetInt("PlayerFriendsPoints",0);

        int count = GetStoreDatas.Count;
        for (int i = 0; i < count; i++)
        {
            ClothesScriptable item = GetStoreDatas[i].clothe;
            if(PlayerPrefs.GetInt("Inventory"+item) == 1)
                playerInventory.Add(item);
        }

        count = quests.Count;
        for (int i = 0; i < count; i++)
        {
           quests[i].SetStep(PlayerPrefs.GetInt(quests[i].quest.name+"Step",-1),false);
        }
    }

    #region Player panel
    public bool HasItem(ClothesScriptable item)
    {
        return PlayerPrefs.GetInt("Inventory"+item) == 1;
    }

    public void SetCoin(int value)
    {
        playerCoins += value;
        PlayerPrefs.SetInt("PlayerCoins",playerCoins);

        HudPlayerPanel.Instance.UpdateCoins(playerCoins);
    }

    public void SetFriendsPoints(int value)
    {
        playerFriendsPoints += value;
        PlayerPrefs.SetInt("PlayerFriendsPoints",playerFriendsPoints);
        HudPlayerPanel.Instance.UpdateFriendsPoints(playerFriendsPoints);
    }
    #endregion

    #region Store
    public bool BuyItem(ClothesScriptable item)
    {
        int price = GetStoreData(item).price;

        if(Coins >= price)
        {
            PlayerPrefs.SetInt("Inventory"+item,1);
            playerInventory.Add(item);
            SetCoin(-price);

            PlayerEquipeCloth(item);
            return true;
        }

        return false;
    }

    public ClothesScriptable GetClothesByName(string name)
    {
        return GetStoreDatas.Find(x => x.clothe.Name == name).clothe;
    }

    public StoreData GetStoreData(ClothesScriptable item)
    {
        return GetStoreDatas.Find(x => x.clothe == item);
    }

    public List<StoreData> GetStoreData(List<ClothesScriptable> item)
    {
        List<StoreData> r = new List<StoreData>();
        int count = item.Count;
        for (int i = 0; i < count; i++)
            r.Add(GetStoreData(item[i]));

        return r;
    }

    public bool GetShowInStore(ClothesScriptable item)
    {
        return GetStoreData(item).showInShop;
    }
    #endregion

    #region Quest
    public QuestScriptable GetQuest(QuestScriptable item)
    {
        return quests.Find(x => x.quest == item).quest;
    }

    public bool MatchNpcQuest(NpcQuest item)
    {
        if(item != null)
        {
            QuestManager q = quests.Find(x => x.quest == item.quest && x.completed == false);

            if(q !=null && q.currentStep == item.step)
                return true;
        }      

        return false;
    }

    public void SetQuestStep(QuestScriptable item,int step)
    {
        int i = quests.FindIndex(x => x.quest == item);
        
        if(quests[i].completed)
            return;

        quests[i].currentStep = step;
        PlayerPrefs.SetInt(item.name+"Step",step);
    }

    public void Quest(List<NpcQuest> nq)
    {     
        NpcQuest npcQuest = null;

        if(nq.Count > 0)
        {
            foreach(NpcQuest t in nq) 
            {
                if(MatchNpcQuest(t))
                {
                    npcQuest = t;
                    break;
                }
            }
        }

        if(npcQuest == null)
            return;
            
        QuestScriptable quest   = npcQuest.quest;
        int i = quests.FindIndex(x => x.quest == quest);

        int currentStep = quests[i].currentStep;

        Debug.Log("Quest() step "+currentStep);

        if(currentStep == -1 && npcQuest.step == currentStep)
        {
            HudQuestManager.Instance.Open(quest);
        }
        else
        { 
            if(npcQuest.sumStep)
            {
                currentStep++;
                quests[i].SetStep(currentStep);
            }

            //Dialog

            if(quests[i].completed)
            {
                GameManager.Instance.SetCoin(quest.BonusCoinComplete);
                GameManager.Instance.SetFriendsPoints(quest.BonusFriendPointComplete);
                HudQuestManager.Instance.Open(quest, true);
            }              
          
            Debug.Log(quest.name+" - "+currentStep+"/"+quest.StepsToComplete);
        }
    }
    #endregion

    public Sprite EmotionBalloon(Emotions e)
    {
        return emotions[(int)e];
    }
}
