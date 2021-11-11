using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables", menuName = "Scriptable/Mob", order = 1)
,HelpURL("https://docs.unity3d.com/ScriptReference/Editor.html")]
public class MobScriptable : ScriptableObject
{
    [SerializeField]
    protected   string _name;
    public      string Name { get { return _name;}}

    [Tooltip("[Idle] - [Smile] - [Talk] - [Angry]")]
    [SerializeField] 
    protected   Sprite[]    _portrait;
    public      Sprite      Portrait(PortraitFeeling feeling)
    {
        return _portrait[(int)feeling];
    }
}
