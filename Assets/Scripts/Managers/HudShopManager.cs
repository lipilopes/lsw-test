using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudShopManager : MonoBehaviour
{
    public static HudShopManager Instance;

    [SerializeField] 
    private GameObject         storePanelGo;
    [SerializeField] 
    private GameObject         storeContent;
    [SerializeField] 
    private GameObject         itemExample;

    private List<GameObject>   itemExamplePool      =   new List<GameObject>();

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    public void CloseShop()
    {
        storePanelGo.SetActive(false);

        int count = itemExamplePool.Count;
        for (int i = 0; i < count; i++)
        {
            itemExamplePool[i].SetActive(false);
        }
    }

    public void OpenShop(List<ClothesScriptable> itemList)
    {
        storeContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                   
        UpdateItens(GameManager.Instance.GetStoreData(itemList));

        storePanelGo.SetActive(true);
    }

    ItemShopContent GetItemListPool(GameObject pool,int comparePoolCount,Transform transform)
    {
        int poolCount = itemExamplePool.Count;

        GameObject obj;
        Sprite playerP = GameManager.Instance.Player.GetComponent<PlayerManager>().GetScriptable.Portrait(PortraitFeeling.Smile);
        if(poolCount < comparePoolCount)
        {
            int count = comparePoolCount - poolCount;
            
            for (int i = 0; i < count; i++)
            {           
                obj = Instantiate(pool, transform) as GameObject;

                obj.name += " "+i;

                obj.SetActive(false);

                obj.GetComponent<ItemShopContent>().UpdatePortrait(playerP);

                itemExamplePool.Add(obj);
            }
        }

        for (int i = 0; i < poolCount; i++)
        {           
            if(!itemExamplePool[i].activeSelf)
            {
                obj = itemExamplePool[i];
                return obj.GetComponent<ItemShopContent>();
            }           
        }

        obj = Instantiate(pool, transform) as GameObject;

        obj.name += " "+poolCount+1;

        itemExamplePool.Add(obj);

        return obj.GetComponent<ItemShopContent>();  
    }

    void UpdateItens(List<StoreData> list)
    {
        int count = list.Count;
        Transform transform = storeContent.transform;
        GameManager gm = GameManager.Instance;
        for (int i = 0; i < count; i++)
        {          
            if(list[i].showInShop)
            {
                ClothesScriptable item = list[i].clothe;

                ItemShopContent itemC = GetItemListPool(itemExample,count,transform);             

                itemC.AddItem(!gm.HasItem(item), list[i].price,item);  
                itemC.gameObject.SetActive(true);
                
                #if UNITY_EDITOR
                    itemC.gameObject.name = item.Name;
                #endif
            }         
        }

        RectTransform rt = storeContent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 827);//content default size
        float size = (float)count/3;//listCount/ content horizontal max size 
        if (size > 1)              
            rt.sizeDelta = new Vector2(0, 827 * (size+0.2f));

        rt.localPosition = new Vector3(0, 0, 0); 
    }
}
