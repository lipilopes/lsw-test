using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public enum Emotions
{
    None,
    Default,
    Angry,
    Love,
    Exclamation_Mark,
    Question_Mark,
    Speaking,
}

[System.Serializable] 
public enum PortraitFeeling 
{
    Idle,
    Smile,
    Talk,
    Angry
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject player;
    public GameObject Player { get { return player; } }

    [Header("Emotions")]
    [SerializeField] 
    private Sprite[] emotions;

    public Sprite EmotionBalloon(Emotions e)
    {
        return emotions[(int)e];
    }
   
    private void Awake() 
    {
         if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);       
    }


}