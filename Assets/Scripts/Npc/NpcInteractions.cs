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

        if(interaction)
        {
            int i = -1;

            if(inChat)
            {
                i = HudManager.Instance.NextDialogue(); 
            }
            else
            {
                inChat = true; 

                i = HudManager.Instance.SetDialogue(scriptable,manager.GetClothes);          
            }

            if(i == -1)
            {
                playerEmotion.EmotionBalloon(false);
                inChat = false;
                Emotion();
                return;
            }

            Dialogue d = scriptable.Dialogues[i];           
            playerEmotion.EmotionBalloon(true,d.playerEmotionBalloon);     
            Emotion(true,d.emotionBalloon);

        }
        else
        {
            playerEmotion.EmotionBalloon(false);

            inChat = false;

            HudManager.Instance.CloseChatPanel();           
        }
    }
}
