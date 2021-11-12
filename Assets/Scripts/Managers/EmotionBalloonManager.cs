using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionBalloonManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer emoteSpriteRender;
    [SerializeField]
    private Emotions       defaultEmotion = Emotions.Exclamation_Mark;

    private bool isPlayer = false;

    private void Start() 
    {
        isPlayer = GetComponent<PlayerManager>();    
    }

    public void EmotionBalloon(bool active,Emotions emotion = Emotions.None)
    {
        if(emoteSpriteRender == null)
            return;

        if(active && emotion == Emotions.Default)
        {
            if(isPlayer)
                active = false;
            else
                emotion = defaultEmotion;
        }

        if(active)
            emoteSpriteRender.sprite = GameManager.Instance.EmotionBalloon(emotion);

        emoteSpriteRender.gameObject.SetActive(active);
    }
}
