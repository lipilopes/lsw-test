using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NpcManager : MonoBehaviour
{
    [SerializeField] NpcScriptable  scriptable;
    [Header("Interaction")]
    [SerializeField] bool           playerInRange = false;
    [SerializeField] Emotions       defaultEmotion = Emotions.Exclamation_Mark;

    bool interaction = false;

    EmotionBallonManager    emotion;
    SpriteRenderer          spriteRender; 

    private void Awake() 
    {
        spriteRender = GetComponent<SpriteRenderer>();
        emotion      = GetComponent<EmotionBallonManager>();

        LoadScriptable();

        playerInRange = false;
        Emotion();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
            Interaction();  
    }

    void LoadScriptable()
    {
        if(scriptable == null)
            return;

        name = "Npc - "+scriptable.name;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;
            Emotion();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = false;
            Emotion();
            
            if(interaction)
                Interaction();
        }

        print(other.gameObject.name);
    }

    private void Emotion()
    {
        if(emotion)
            emotion.EmotionBallon(playerInRange,defaultEmotion);
    }

    private void Interaction()
    {
        interaction = !interaction;

        if(interaction)
        {
            MobsClothes mC = GetComponent<MobsClothes>();
            ClothesScriptable tshirt = mC!=null ? mC.Tshirt : null;
            ClothesScriptable glasses = mC!=null ? mC.Glasses : null;

            HudManager.Instance.ChatPanel(
                scriptable.Dialogues[0].text,
                scriptable.Name,
                scriptable.Portrait(scriptable.Dialogues[0].portraitFeeling),
                tshirt,
                glasses);
                
            emotion.EmotionBallon(true,scriptable.Dialogues[0].emotionBallon);
        }
        else
        {
            HudManager.Instance.CloseChatPanel();

            emotion.EmotionBallon(true,defaultEmotion);
        }
    }

}
