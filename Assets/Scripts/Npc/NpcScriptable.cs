using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Npc", menuName = "Scriptable/Npc", order = 1)
,HelpURL("https://docs.unity3d.com/ScriptReference/Editor.html")
]
public class NpcScriptable : ScriptableObject
{
    [SerializeField] string _name;
    public string Name { get { return _name;}}

    [SerializeField] Sprite  _sprite;
    public Sprite  Sprite { get { return _sprite;}}

    [SerializeField] Sprite[] _portrait;
    public Sprite Portrait(int i)
    {
        if(i < 0 || i >= _portrait.Length)
            i = 0;

        return _portrait[i];
    }


}
