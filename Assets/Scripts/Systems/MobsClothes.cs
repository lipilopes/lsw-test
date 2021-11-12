using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsClothes : MonoBehaviour
{
    #if UNITY_EDITOR
    void TestClothes()
    {
        ChangeTshirt(tshirt);

        ChangeGlasses(glasses);
    }

    [ContextMenuItem("Test Clothes", "TestClothes")]
    #endif
    [Header("Tshirt")]
    [SerializeField]
    private GameObject tshirtObj;

    [SerializeField]
    private ClothesScriptable tshirt;
    public  ClothesScriptable Tshirt{ get { return tshirt; } }

    private Animator        tshirtAnim;
    private SpriteRenderer  tshirtSpriteR;

    [Header("Glasses")]
    [SerializeField]
    private GameObject glassesObj;

    [SerializeField]
    private ClothesScriptable glasses;
    public  ClothesScriptable Glasses{ get { return glasses; } }

    private Animator glassesAnim;
    private SpriteRenderer glassesSpriteR;

    bool isPlayer = false;

    private void Start() 
    {
        if(tshirtObj == null)
            return;

        tshirtAnim  = tshirtObj.GetComponent<Animator>();
        glassesAnim = glassesObj.GetComponent<Animator>();

        tshirtSpriteR  = tshirtObj.GetComponent<SpriteRenderer>();
        glassesSpriteR = glassesObj.GetComponent<SpriteRenderer>();

        isPlayer = GetComponent<PlayerManager>();

        if(isPlayer)
        {
            string ts  = PlayerPrefs.GetString("PlayerPants");
            string gss = PlayerPrefs.GetString("PlayerGlasses");
            
            if(ts != "")
                tshirt  = GameManager.Instance.GetClothesByName(ts);
            if(gss != "")
                glasses = GameManager.Instance.GetClothesByName(gss);
        }

        if(tshirt!=null)
            ChangeTshirt(tshirt);

        if(glasses!=null)
            ChangeGlasses(glasses);
    }

    public ClothesScriptable GetCurrentItem(ClothesEnum itemType)
    {
        switch (itemType)
        {
            default:                    return Tshirt;
            case ClothesEnum.Tshirt:    return Tshirt;
            case ClothesEnum.Glasses:   return Glasses;
        }       
    }

    public void AnimClothes(float speed,float horizontal,float vertical)
    {
        if(tshirtObj.activeInHierarchy && tshirtAnim != null)
        {
            tshirtAnim.SetFloat("Horizontal",horizontal);
            tshirtAnim.SetFloat("Vertical",vertical);
            tshirtAnim.SetFloat("Speed",speed);
        }

        if(glassesObj.activeInHierarchy && glassesAnim != null)
        {
            glassesAnim.SetFloat("Horizontal",horizontal);
            glassesAnim.SetFloat("Vertical",vertical);
            glassesAnim.SetFloat("Speed",speed);
        }
        
    }

    public bool ChangeTshirt(ClothesScriptable newTshirt)
    {
        #if !UNITY_EDITOR
            if(tshirt == newTshirt)
                return false;
        #endif

        if(newTshirt == null || newTshirt.ClothesType != ClothesEnum.Tshirt || newTshirt.ControllerPath == null || tshirtAnim == null || tshirtSpriteR == null)
        {
            tshirt = null;

            tshirtObj.SetActive(false);

            #if UNITY_EDITOR
                tshirtObj.name = "Pants Empty";
            #endif
        }
        else
        {
            tshirt = newTshirt;

            tshirtObj.SetActive(true);

            tshirtAnim.runtimeAnimatorController = Resources.Load(tshirt.ControllerPath) as RuntimeAnimatorController;

            tshirtSpriteR.color = tshirt.GetColor;

            #if UNITY_EDITOR
                tshirtObj.name = "Pants "+tshirt.Name;
            #endif
        }

        if(isPlayer)
            PlayerPrefs.SetString("PlayerPants",""+tshirt.Name);
        
        return true;
    }

    public bool ChangeGlasses(ClothesScriptable newGlasses)
    {
        #if !UNITY_EDITOR
            if(glasses == newGlasses)
                return false;
        #endif

        if(newGlasses == null || newGlasses.ClothesType != ClothesEnum.Glasses || glassesObj==null || newGlasses.ControllerPath == null || glassesAnim == null || glassesSpriteR == null)
        {
            glasses = null;

            glassesObj.SetActive(false);

            #if UNITY_EDITOR
                glassesObj.name = "Glasses Empty";
            #endif
        }
        else
        {
            glasses = newGlasses;

            glassesObj.SetActive(true);

            glassesAnim.runtimeAnimatorController = Resources.Load(glasses.ControllerPath) as RuntimeAnimatorController;

            glassesSpriteR.color = glasses.GetColor;

            #if UNITY_EDITOR
                glassesObj.name = "Glasses "+glasses.Name;
            #endif
        }

        if(isPlayer)
            PlayerPrefs.SetString("PlayerGlasses",""+glasses.Name);

        return true;
    }

    public bool ChangeClothe(ClothesScriptable newClothe)
    {
        #if !UNITY_EDITOR
            if(tshirt == newTshirt)
                return false;
        #endif

        switch (newClothe.ClothesType)
        {
            default: return false;
            case ClothesEnum.Tshirt:  return ChangeTshirt(newClothe);
            case ClothesEnum.Glasses: return ChangeGlasses(newClothe);
        }   
    }


}
