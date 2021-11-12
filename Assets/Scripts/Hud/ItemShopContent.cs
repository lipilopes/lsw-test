using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemShopContent : ItemContent
{
    [Header("Top Panel")]
    [SerializeField]
    protected TextMeshProUGUI   _priceText;
    [SerializeField]
    protected TextMeshProUGUI   _nameText;
  

    public void AddItem(bool canBuy,int price,ClothesScriptable _item)
    {
        if(_item != item)
        {
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(() =>  SelectItem(item));
        }
       
        item = _item;

        Button.interactable = canBuy;
        _itemImage.gameObject.SetActive(canBuy);
     
        _nameText.text      = item.Name;
        _itemImage.sprite   = item.PortraitSprite;

        if(item.isMultColor)
            _itemImage.color = item.GetColor;

        if(canBuy)
        {
            _portraitImage.color = new Color(0.61f, 0.61f, 0.61f, 1);  
            _priceText.text = ""+price;
        }           
        else
        {
            _portraitImage.color    = new Color(0,0,0,1); 
            _priceText.text         = ""; 
        }        
    }

    public override void SelectItem(ClothesScriptable item)
    {
        if(GameManager.Instance.BuyItem(item))
        {
            Button.interactable     = false;
            _portraitImage.color    = new Color(0,0,0,1);
            _priceText.text         = "";

            HudShopManager.Instance.UpdateCoinShop();
        }
    }

}
