using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{   [TextArea]
    public string           text;
    public bool             playerSpeaking          = false;
    [Header("Portraits")]
    public bool             showNpcPortrait         = true;
    public PortraitFeeling  portraitFeeling;
    [Space]
    public bool             showPlayerPortrait      = true;
    public PortraitFeeling  playerPortraitFeeling;
    [Header("Balloons")]
    public Emotions         emotionBalloon          = Emotions.None;
    public Emotions         playerEmotionBalloon    = Emotions.None;

}
