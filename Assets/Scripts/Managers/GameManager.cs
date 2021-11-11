using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public enum Emotions
{
    None,
    Angry,
    Love,
    Exclamation_Mark,
    Question_Mark,
    Speaking
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

    [Header("Emotions")]
    [Tooltip("[Angry] - [S2] - [!] - [?] - [...]")]
    [SerializeField] Sprite[] emotions;

    public Sprite EmotionBallon(Emotions e)
    {
       return emotions[(int)e-1];
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
