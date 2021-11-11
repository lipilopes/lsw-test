using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D),typeof(MobsClothes))]
public class NpcManager : MonoBehaviour
{
    [SerializeField] 
    private NpcScriptable scriptable;
    public  NpcScriptable Scriptable { get{ return scriptable;} }

    private MobsClothes clothes;
    public  MobsClothes GetClothes { get{ return clothes;} }

    private void Start() 
    {
        clothes = GetComponent<MobsClothes>();
        LoadScriptable();
    }

    void LoadScriptable()
    {
        if(scriptable == null)
            return;

        #if UNITY_EDITOR
            name = "Npc - "+scriptable.name;
        #endif
    }  
}
