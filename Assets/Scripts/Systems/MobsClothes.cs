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
    [SerializeField] GameObject tshirtObj;

    [SerializeField] ClothesScriptable tshirt;
    public ClothesScriptable Tshirt{ get { return tshirt; } }

    Animator tshirtAnim;
    SpriteRenderer tshirtSpriteR;

    [Header("Glasses")]
    [SerializeField] GameObject glassesObj;

    [SerializeField] ClothesScriptable glasses;
    public ClothesScriptable Glasses{ get { return glasses; } }

    Animator glassesAnim;
    SpriteRenderer glassesSpriteR;

    private void Start() 
    {
        tshirtAnim  = tshirtObj.GetComponent<Animator>();
        glassesAnim = glassesObj.GetComponent<Animator>();

        tshirtSpriteR  = tshirtObj.GetComponent<SpriteRenderer>();
        glassesSpriteR = glassesObj.GetComponent<SpriteRenderer>();

        if(tshirt!=null)
            ChangeTshirt(tshirt);

        if(glasses!=null)
            ChangeGlasses(glasses);
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
        

        return true;
    }

    public bool ChangeGlasses(ClothesScriptable newGlasses)
    {
        #if !UNITY_EDITOR
            if(glasses == newGlasses)
                return false;
        #endif

        if(newGlasses == null || newGlasses.ClothesType != ClothesEnum.Glasses || glassesObj==null || glasses.ControllerPath == null || glassesAnim == null || glassesSpriteR == null)
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

        return true;
    }
}
