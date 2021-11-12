using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemShopContent : MonoBehaviour
{
    [SerializeField]
    protected Button             _button;
    [Header("Top Panel")]
    [SerializeField]
    protected TextMeshProUGUI   _priceText;
    [SerializeField]
    protected TextMeshProUGUI   _nameText;
    [Header("Circle")]
    [SerializeField]
    protected Image   _portrait;
    [SerializeField]
    protected Image   _item;
  
    public Button Button{ get {return _button;}}

    public void UpdatePortrait(Sprite img)
    {
        _portrait.sprite = img;
    }

    public void AddItem(bool canBuy,int price,ClothesScriptable item)
    {
        Button.interactable = canBuy;
        _item.gameObject.SetActive(canBuy);
     
        _nameText.text  = item.Name;
        _item.sprite    = item.PortraitSprite;

        if(item.isMultColor)
            _item.color     = item.GetColor;

        if(canBuy)
        {
            _portrait.color = new Color(0.61f, 0.61f, 0.61f, 1); 
            Button.onClick.AddListener(() =>  BuyItem(item));   
            _priceText.text = ""+price;
        }           
        else
        {
            _portrait.color = new Color(0,0,0,1); 
            _priceText.text = ""; 
        } 
       
    }

    public void BuyItem(ClothesScriptable item)
    {
        if(GameManager.Instance.BuyItem(item))
        {
            Button.interactable = false;
            _portrait.color = new Color(0,0,0,1);
            _priceText.text = "";
        }
    }

}
