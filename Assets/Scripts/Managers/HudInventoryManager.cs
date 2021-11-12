using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HudInventoryManager : HudShopManager
{
    public static HudInventoryManager Instance;

    Animator anim;

    bool inventortyOpen = false;

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

        anim = panelGo.GetComponent<Animator>();
    }

    public override void Close()
    {
        inventortyOpen = false;
        
        int count = itemExamplePool.Count;
        for (int i = 0; i < count; i++)
        {
            itemExamplePool[i].SetActive(false);
        }
    }

    public virtual void Open()
    {
        inventortyOpen = !inventortyOpen;

        anim.SetBool("Open",inventortyOpen);

        if(inventortyOpen)
        {
            content.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                   
            UpdateItens(GameManager.Instance.GetInventory);

            anim.SetBool("Open",true);
        }
        else
            Close();
    }

    ItemContent GetItemListPool(GameObject pool,int comparePoolCount,Transform transform)
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

                obj.GetComponent<ItemContent>().UpdatePortrait(playerP);

                itemExamplePool.Add(obj);
            }
        }

        for (int i = 0; i < poolCount; i++)
        {           
            if(!itemExamplePool[i].activeSelf)
            {
                obj = itemExamplePool[i];
                return obj.GetComponent<ItemContent>();
            }           
        }

        obj = Instantiate(pool, transform) as GameObject;

        obj.name += " "+poolCount+1;

        itemExamplePool.Add(obj);

        return obj.GetComponent<ItemContent>();  
    }

    protected void UpdateItens(List<ClothesScriptable> list)
    {
        int count = list.Count;
        Transform transform = content.transform;
        GameManager gm = GameManager.Instance;
        for (int i = 0; i < count; i++)
        {          
                ClothesScriptable item = list[i];

                ItemContent itemC = GetItemListPool(itemExample,count,transform);             

                itemC.AddItem(gm.CanEquipeCloth(item) ,item);  

                itemC.gameObject.SetActive(true);
                
                #if UNITY_EDITOR
                    itemC.gameObject.name = item.Name;
                #endif      
        }

        RectTransform rt = content.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 356);//content default size
        float size = (float)count/2.5f;//listCount/ content horizontal max size 
        if (size > 1)              
            rt.sizeDelta = new Vector2(0, 356 * (size));

        rt.localPosition = new Vector3(0, 0, 0); 
    }

    public void UpdateItem(ClothesScriptable item,bool canEquipe = false)
    {
        ItemContent itemC = itemExamplePool.Find(x => x.GetComponent<ItemContent>().Item == item).GetComponent<ItemContent>();           

        itemC.AddItem(canEquipe,item);
    }
}
