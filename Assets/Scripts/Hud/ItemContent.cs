using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemContent : MonoBehaviour
{
    [SerializeField]
    protected Button             _button;
    [Header("Circle")]
    [SerializeField]
    protected Image   _portraitImage;
    [SerializeField]
    protected Image  _itemImage;

    protected ClothesScriptable item;
  
    public Button Button { get {return _button;}}

    public ClothesScriptable Item { get {return item;}}

    public void UpdatePortrait(Sprite img)
    {
        _portraitImage.sprite = img;
    }

    public void AddItem(bool canSelect,ClothesScriptable _item)
    {
        if(_item != item)
        {
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(() =>  SelectItem(item));
        }
       
        item = _item;
        Button.interactable = canSelect;
        _itemImage.gameObject.SetActive(canSelect);
     
        _itemImage.sprite    = item.PortraitSprite;

        if(item.isMultColor)
            _itemImage.color     = item.GetColor;

        if(canSelect)
            _portraitImage.color = new Color(0.61f, 0.61f, 0.61f, 1);                      
        else
            _portraitImage.color = new Color(0,0,0,1);  
    }

    public virtual void SelectItem(ClothesScriptable _item)
    {
        ClothesScriptable beforeItem = GameManager.Instance.PlayerClothes.GetCurrentItem(_item.ClothesType);

        if(GameManager.Instance.PlayerEquipeCloth(_item))
        {
            Button.interactable     = false;
            _portraitImage.color    = new Color(0,0,0,1);

            HudInventoryManager.Instance.UpdateItem(beforeItem, true);
        }      
    }
}
