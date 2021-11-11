using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public enum ClothesEnum
{
    Tshirt,
    Glasses
}


[CreateAssetMenu(fileName = "", menuName = "Scriptable/Clothes", order = 1)]
public class ClothesScriptable : ScriptableObject
{
    [SerializeField]
    private string _name;
    public  string Name { get { return _name; } }

    [SerializeField]
    private Sprite _portraitSprite;
    public  Sprite PortraitSprite { get { return _portraitSprite; } }

    [SerializeField]
    private ClothesEnum _clothesType;
    public  ClothesEnum ClothesType { get { return _clothesType; } }

    // [Tooltip("Shop Icon")]
    // [SerializeField] Sprite _iconSprite;
    // public Sprite IconSprite { get { return _iconSprite;}}

    [Header("Color")]
    [SerializeField]
    private bool _isMultColor;
    public  bool isMultColor { get { return _isMultColor; } }

    [SerializeField]
    private Color _color;
    public  Color GetColor { get { return _isMultColor ? _color : Color.white; } }
    [Space]
    [Tooltip("Name of OverrideController in Resources folder")]
    [SerializeField]
    private string ControllerName;

    /// <summary>
    /// animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(ControllerPath);
    /// </summary>
    public  virtual string ControllerPath{ get {return "OverrideController/Clothes/"+_clothesType+"/"+ControllerName;}}
}

