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
    [SerializeField] string _name;
    public string Name { get { return _name;}}

    [SerializeField] Sprite _portraitSprite;
    public Sprite PortraitSprite { get { return _portraitSprite;}}

    [SerializeField] ClothesEnum _clothesType;
    public ClothesEnum ClothesType { get { return _clothesType;}}

    // [Tooltip("Shop Icon")]
    // [SerializeField] Sprite _iconSprite;
    // public Sprite IconSprite { get { return _iconSprite;}}

    [Header("Color")]
    [SerializeField] bool _isMultColor;
    public bool isMultColor { get { return _isMultColor;}}

    [SerializeField] Color _color;
    public Color GetColor { get { return _isMultColor ? _color : Color.white;}}
    [Space]
    [Tooltip("Name of OverrideController in Resources folder")]
    [SerializeField] string ControllerName;

    /// <summary>
    /// animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(ControllerPath);
    /// </summary>
    public  virtual string ControllerPath{ get {return "OverrideController/Clothes/"+_clothesType+"/"+ControllerName;}}
}

