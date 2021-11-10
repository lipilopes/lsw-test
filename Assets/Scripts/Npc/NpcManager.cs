using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class NpcManager : MonoBehaviour
{
    [SerializeField] NpcScriptable scriptable;
    [Header("Interaction")]
    [SerializeField] bool playerInRange = false;
    [SerializeField] SpriteRenderer emoteSpriteRender;

    SpriteRenderer spriteRender;
    bool interaction = false; 

    private void Awake() 
    {
        spriteRender = GetComponent<SpriteRenderer>();

        LoadScriptable();

        playerInRange = false;
        Emote();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            Interaction();
        }    
    }

    void LoadScriptable()
    {
        if(scriptable == null)
            return;

        spriteRender.sprite = scriptable.Sprite;
        name = "Npc - "+scriptable.name;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;
            Emote();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = false;
            Emote();
        }

        print(other.gameObject.name);
    }

    private void Emote()
    {
        emoteSpriteRender.gameObject.SetActive(playerInRange);
    }

    private void Interaction()
    {
        interaction = !interaction;

        Debug.Log("Interaction -> "+interaction);

        if(interaction)
            HudManager.Instance.ChatPanel("TESTTEEE",scriptable.Name,scriptable.Portrait(0));
        else
            HudManager.Instance.CloseChatPanel();

        emoteSpriteRender.sprite = GameManager.Instance.EmotionBallon(interaction ? Emotions.Speaking : Emotions.Exclamation_Mark);
    }

}
