using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NpcManager))]
public class NpcInteractions : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField]
    private bool           playerInRange = false;

    private bool interaction = false;
    private bool inChat = false;

    private EmotionBalloonManager    emotion;
    private EmotionBalloonManager    playerEmotion;
    private NpcScriptable           scriptable;
    private NpcManager              manager;

    protected void Start() 
    {
        emotion         = GetComponent<EmotionBalloonManager>();
        playerEmotion   = GameManager.Instance.Player.GetComponent<EmotionBalloonManager>();
        manager         = GetComponent<NpcManager>();
        scriptable      = manager.Scriptable;
        
        playerInRange = false;

        Emotion(false);
    }

    protected virtual void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
            Interaction();  
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;
            Emotion();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = false;
            Emotion(false);
            
            Interaction(false);
        }
    }

    protected void Emotion(bool active=true,Emotions e = Emotions.Default)
    {
        if(emotion)       
            emotion.EmotionBalloon(active,e);          
    }


    public virtual void  Interaction(bool? value=null)
    {
        if(inChat == false || value == false)
        {
            if(value == null)
                interaction = !interaction;
            else
                interaction = (bool)value;
        }


        switch (scriptable.Type)
        {
            default:

            break;

            case NpcType.Speaker:
                OpenChat();
            break;

            case NpcType.Shopkeeper:
                OpenChat(true);
            break;
        }      
    }

    void InteractionEnd()
    {
        playerEmotion.EmotionBalloon(false);
        
        switch (scriptable.Type)
        {
            default:

            break;

            case NpcType.Speaker:     
                inChat = false;
                HudChatManager.Instance.CloseChatPanel(); 
            break;

            case NpcType.Shopkeeper:
                HudShopManager.Instance.CloseShop();
            break;
        }


    }

    void OpenChat(bool openShop=false)
    {
        if(interaction)
        {
            int i = -1;

            if(inChat)
            {
                i = HudChatManager.Instance.NextDialogue(); 
            }
            else
            {
                inChat = true; 

                i = HudChatManager.Instance.SetDialogue(scriptable,manager.GetClothes);          
            }

            if(i == -1)
            {
                playerEmotion.EmotionBalloon(false);
                inChat = false;
                Emotion();

                if(openShop)
                    OpenShop();

                return;
            }

            Dialogue d = scriptable.Dialogues[i];  
            playerEmotion.EmotionBalloon(true,d.playerEmotionBalloon); 
            Emotion(true,d.emotionBalloon);
        }
        else
        {           
            InteractionEnd();          
        }        
    }

    void OpenShop()
    {
        HudShopManager.Instance.CloseShop();
        
        if(scriptable.StoreList.Count > 0)
            HudShopManager.Instance.OpenShop(scriptable.StoreList);
    }
}
