using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionBallonManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer emoteSpriteRender;

    public void EmotionBallon(bool active,Emotions emotion = Emotions.None)
    {
        if(active)
            emoteSpriteRender.sprite = GameManager.Instance.EmotionBallon(emotion);

        emoteSpriteRender.gameObject.SetActive(active);
    }
}
