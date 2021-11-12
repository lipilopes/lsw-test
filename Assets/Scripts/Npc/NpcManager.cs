using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D),typeof(MobsClothes))]
public class NpcManager : MonoBehaviour
{
    #if UNITY_EDITOR
    Vector3 _respaw = Vector3.zero;
    Vector3 _test   = new Vector3(4.1f,-1.31f,0);
    void TESTENPC()
    {
        if(_respaw  == Vector3.zero)
            _respaw = this.transform.position;

        if(this.transform.position != _respaw)
            this.transform.position = _respaw;
        else
            this.transform.position = _test;
    }

    [ContextMenuItem("Test NPC", "TESTENPC")]
    #endif
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
