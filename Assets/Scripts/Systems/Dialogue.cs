using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public bool             playerSpeaking = false;
    public PortraitFeeling  portraitFeeling;
    public Emotions         emotionBallon = Emotions.None;
    [TextArea]
    public string           text;
}
