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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    [SerializeField] 
    private GameObject              player;
    [SerializeField]
    private int                     playerCoins;
    [SerializeField]
    private int                     playerFriendsPoints;
    [SerializeField]
    private List<ClothesScriptable> playerInventory = new List<ClothesScriptable>();

    [Header("Data")]
    [SerializeField]
    private List<StoreData> storeDatas = new List<StoreData>();

    [Header("Emotions")]
    [SerializeField] 
    private Sprite[] emotions;

    public GameObject   Player          { get { return player; } }
    public int          Coins           { get { return playerCoins; } }
    public int          FriendsPoints   { get { return playerFriendsPoints; } }

    public  List<StoreData> GetStoreDatas  { get { return storeDatas; } }
    //public  List<StoreData> GetShowInStore { get { return storeDatas.FindAll(x => x.showInShop == true); } }

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);   

        LoadPlayerPrefs(); 
    }

    public bool HasItem(ClothesScriptable item)
    {
        return PlayerPrefs.GetInt("Inventory"+item) == 1;
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
    }

    public void SetCoin(int value)
    {
        playerCoins += value;
        PlayerPrefs.SetInt("PlayerCoins",playerCoins);
    }

    public void SetFriendsPoints(int value)
    {
        playerFriendsPoints += value;
        PlayerPrefs.SetInt("PlayerFriendsPoints",playerFriendsPoints);
    }

    public bool BuyItem(ClothesScriptable item)
    {
        int price = GetStoreData(item).price;

        if(Coins >= price)
        {
            PlayerPrefs.SetInt("Inventory"+item,1);
            playerInventory.Add(item);
            SetCoin(-price);

            Player.GetComponent<MobsClothes>().ChangeClothe(item);
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

    public Sprite EmotionBalloon(Emotions e)
    {
        return emotions[(int)e];
    }
}
