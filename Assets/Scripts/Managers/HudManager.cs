using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance;

    [Header("Chat Panel")]
    [SerializeField] GameObject         chatPanelGo;
    [SerializeField] Image              speakingPortraitImage;
    [SerializeField] Image              listenerPortraitImage;
    [SerializeField] TextMeshProUGUI    chatText;
    [SerializeField] TextMeshProUGUI    speakNameText;
    [SerializeField] Sprite             NullSprite;

    private void Awake() 
    {
         if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);       
    }


    public void ChatPanel(string conversation,string speakName,Sprite speakP,Sprite? listenerP=null)
    {
        Debug.Log("Chat");
        chatPanelGo.SetActive(true);

        speakingPortraitImage.sprite = speakP;

        listenerPortraitImage.sprite = listenerP == null ? NullSprite : listenerP;

        chatText.text       = conversation;
        speakNameText.text  = speakName;
    }

    public void CloseChatPanel()
    {
        chatPanelGo.SetActive(false);
    }

}
