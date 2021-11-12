using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudChatManager : MonoBehaviour
{
    public static HudChatManager Instance;

    [SerializeField] 
    private Sprite  nullSprite;
    public  Sprite  NullSprite{ get { return nullSprite; } }

    [Header("Chat Panel")]
    [SerializeField]
    private GameObject         chatPanelGo;
    [Space]
    [SerializeField]
    private Image              playerPortraitImage;
    [SerializeField]
    private Image              playerTshirtImage;
    [SerializeField]
    private Image              playerGlassesImage;
    [Space]
    [SerializeField]
    private Image              npcPortraitImage;
    [SerializeField]
    private Image              npcTshirtImage;
    [SerializeField]
    private Image              npcGlassesImage;
    [Space]
    [SerializeField]
    private TextMeshProUGUI    chatText;
    [SerializeField]
    private TextMeshProUGUI    speakerNameText;   
    [Space]
    [SerializeField]
    private int                indexDialogue;

    private PlayerManager      playerManager;
    private NpcScriptable      currentNpcDialogue;
    private ClothesScriptable? currentTshirt;
    private ClothesScriptable? currentGlasses;

    private void Awake() 
    {
         if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

        playerManager = GameManager.Instance.Player.GetComponent<PlayerManager>();

        ResetListenerPanel();
    }

    public int SetDialogue(NpcScriptable scriptable,MobsClothes clothes)
    {
        currentNpcDialogue  = scriptable;
        indexDialogue       = -1;//Next Dialogue -> ++
        currentTshirt       = clothes.Tshirt;
        currentGlasses      = clothes.Glasses;

        chatPanelGo.SetActive(true);

        return NextDialogue();
    }

    public int NextDialogue()
    {
        indexDialogue++;

        if(indexDialogue >= currentNpcDialogue.Dialogues.Length)
        {
            CloseChatPanel();

            return -1;
        }
        
        ChatPanel(currentNpcDialogue.Dialogues[indexDialogue]);

        return indexDialogue;    
    }

    void ChatPanel(Dialogue dialogue)
    {  
        string speakerName = "";

        if(dialogue.playerSpeaking)
        {      
            speakerName                 = playerManager.GetScriptable.Name;
            playerPortraitImage.color   = new Color(1,1,1, 1);
            npcPortraitImage.color      = new Color(1,1,1, 0.65f);
        }
        else
        {        
            speakerName                 = currentNpcDialogue.Name;
            playerPortraitImage.color   = new Color(1,1,1, 0.65f);
            npcPortraitImage.color      = new Color(1,1,1, 1);
        }   

        Dialogue d = currentNpcDialogue.Dialogues[indexDialogue];

        UpdatePlayerPortrait(d.showPlayerPortrait); 

        UpdateNpcPortrait(d.showNpcPortrait);          

        chatText.text           = dialogue.text;
        speakerNameText.text    = speakerName;
    }

    public void UpdatePlayerPortrait(bool active = true)
    {
        ClothesScriptable?  mobTshirt       = playerManager.GetClothes.Tshirt;
        ClothesScriptable?  mobGlasses      = playerManager.GetClothes.Glasses;

        if(active)
            playerPortraitImage.sprite  = playerManager.GetScriptable.Portrait(currentNpcDialogue.Dialogues[indexDialogue].playerPortraitFeeling);  
        else
            playerPortraitImage.color   = new Color(1,1,1, 0);

        playerTshirtImage.sprite   = mobTshirt == null || !active ? NullSprite : mobTshirt.PortraitSprite;
        if(playerTshirtImage.sprite != NullSprite)
            playerTshirtImage.color = mobTshirt.GetColor;
        else
            playerTshirtImage.color = new Color(0,0,0,0);
                
        playerGlassesImage.sprite  = mobGlasses == null || !active ? NullSprite : mobGlasses.PortraitSprite;
        if(playerGlassesImage.sprite != NullSprite)
            playerGlassesImage.color = mobGlasses.GetColor;
        else
            playerGlassesImage.color = new Color(0,0,0,0); 
    }

    void UpdateNpcPortrait(bool active = true)
    {
        if(active)
            npcPortraitImage.sprite     = currentNpcDialogue.Portrait(indexDialogue);  
        else
            npcPortraitImage.color      = new Color(1,1,1, 0);

        npcTshirtImage.sprite   =  !active || currentTshirt == null ? NullSprite : currentTshirt.PortraitSprite;
        if(npcTshirtImage.sprite != NullSprite)
            npcTshirtImage.color = currentTshirt.GetColor;
        else
            npcTshirtImage.color = new Color(0,0,0,0);

        npcGlassesImage.sprite  = !active || currentGlasses == null ? NullSprite : currentGlasses.PortraitSprite;
        if(npcGlassesImage.sprite != NullSprite)
            npcGlassesImage.color = currentGlasses.GetColor;
        else
            npcGlassesImage.color = new Color(0,0,0,0);
    }

    void ResetListenerPanel()
    {
        playerPortraitImage.sprite  = playerManager.GetScriptable.Portrait(PortraitFeeling.Idle);
        playerTshirtImage.color     = new Color(0,0,0,0);

        playerGlassesImage.sprite   = NullSprite;
        playerGlassesImage.color    = new Color(0,0,0,0);

        npcPortraitImage.sprite     = NullSprite;
        npcTshirtImage.color        = new Color(0,0,0,0);

        npcGlassesImage.sprite      = NullSprite;
        npcGlassesImage.color       = new Color(0,0,0,0);
    }

    public void CloseChatPanel()
    {
        ResetListenerPanel();
        chatPanelGo.SetActive(false);
    }

    // public void ChatPanel(string conversation,string speakName,Sprite speakP,ClothesScriptable? speakTshirt,ClothesScriptable? speakGlasses,Sprite? npcP=null,ClothesScriptable? npcTshirt=null,ClothesScriptable? npcGlasses=null)
    // {
    //     chatPanelGo.SetActive(true);

        
    //     playerPortraitImage.sprite = speakP;
    //     playerTshirtImage.sprite   = speakTshirt == null ? NullSprite : speakTshirt.PortraitSprite;
    //     if(playerTshirtImage.sprite != NullSprite)
    //         playerTshirtImage.color = speakTshirt.GetColor;
    //     else
    //         playerTshirtImage.color = new Color(0,0,0,0);
            
    //     playerGlassesImage.sprite  = speakGlasses == null ? NullSprite : speakGlasses.PortraitSprite;
    //     if(playerGlassesImage.sprite != NullSprite)
    //         playerGlassesImage.color = speakGlasses.GetColor;
    //     else
    //         playerGlassesImage.color = new Color(0,0,0,0);

    //     npcPortraitImage.sprite = npcP == null ? NullSprite : npcP;
    //     npcTshirtImage.sprite   = npcP == null || npcTshirt == null ? NullSprite : npcTshirt.PortraitSprite;
    //     if(npcTshirtImage.sprite != NullSprite)
    //         npcTshirtImage.color = npcTshirt.GetColor;
    //     else
    //         npcTshirtImage.color = new Color(0,0,0,0);

    //     npcGlassesImage.sprite  = npcP == null || npcGlasses == null ? NullSprite : npcGlasses.PortraitSprite;
    //     if(npcGlassesImage.sprite != NullSprite)
    //         npcGlassesImage.color= npcGlasses.GetColor;
    //     else
    //         npcGlassesImage.color = new Color(0,0,0,0);

    //     chatText.text       = conversation;
    //     speakNameText.text  = speakName;
    // }

}
