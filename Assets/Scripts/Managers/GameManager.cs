using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public enum Emotions
{
    Angry,
    Love,
    Exclamation_Mark,
    Question_Mark,
    Speaking
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Emotions")]
    [Tooltip("[Angry] - [S2] - [!] - [?] - [...]")]
    [SerializeField] Sprite[] emotions;

    public Sprite EmotionBallon(Emotions e)
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
