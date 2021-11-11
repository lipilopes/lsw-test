using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance;

    [SerializeField] Sprite             nullSprite;

    [Header("Chat Panel")]
    [SerializeField] GameObject         chatPanelGo;
    [Space]
    [SerializeField] Image              speakingPortraitImage;
    [SerializeField] Image              speakingTshirtImage;
    [SerializeField] Image              speakingGlassesImage;
    [Space]
    [SerializeField] Image              listenerPortraitImage;
    [SerializeField] Image              listenerTshirtImage;
    [SerializeField] Image              listenerGlassesImage;
    [Space]
    [SerializeField] TextMeshProUGUI    chatText;
    [SerializeField] TextMeshProUGUI    speakNameText;

    public Sprite NullSprite{ get{return nullSprite;}}

    private void Awake() 
    {
         if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

            
    }


    public void ChatPanel(string conversation,string speakName,Sprite speakP,ClothesScriptable? speakTshirt,ClothesScriptable? speakGlasses,Sprite? listenerP=null,ClothesScriptable? listenerTshirt=null,ClothesScriptable? listenerGlasses=null)
    {
        chatPanelGo.SetActive(true);

        speakingPortraitImage.sprite = speakP;
        speakingTshirtImage.sprite   = speakTshirt == null ? NullSprite : speakTshirt.PortraitSprite;
        if(speakingTshirtImage.sprite != NullSprite)
            speakingTshirtImage.color = speakTshirt.GetColor;
        else
            speakingTshirtImage.color = new Color(0,0,0,0);
            
        speakingGlassesImage.sprite  = speakGlasses == null ? NullSprite : speakGlasses.PortraitSprite;
        if(speakingGlassesImage.sprite != NullSprite)
            speakingGlassesImage.color = speakGlasses.GetColor;
        else
            speakingGlassesImage.color = new Color(0,0,0,0);

        listenerPortraitImage.sprite = listenerP == null ? NullSprite : listenerP;
        listenerTshirtImage.sprite   = listenerP == null || listenerTshirt == null ? NullSprite : listenerTshirt.PortraitSprite;
        if(listenerTshirtImage.sprite != NullSprite)
            listenerTshirtImage.color = listenerTshirt.GetColor;
        else
            listenerTshirtImage.color = new Color(0,0,0,0);

        listenerGlassesImage.sprite  = listenerP == null || listenerGlasses == null ? NullSprite : listenerGlasses.PortraitSprite;
        if(listenerGlassesImage.sprite != NullSprite)
            listenerGlassesImage.color= listenerGlasses.GetColor;
        else
            listenerGlassesImage.color = new Color(0,0,0,0);

        chatText.text       = conversation;
        speakNameText.text  = speakName;
    }

    public void CloseChatPanel()
    {
        chatPanelGo.SetActive(false);
    }

}
