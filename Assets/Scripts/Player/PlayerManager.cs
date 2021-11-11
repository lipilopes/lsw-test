using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobsClothes),typeof(EmotionBalloonManager),typeof(PlayerMoviment))]
public class PlayerManager : MonoBehaviour
{
   [SerializeField] 
   private  MobScriptable  scriptable;
   
   private  MobsClothes    clothes;

   private void Start() 
   {
      clothes = GetComponent<MobsClothes>();
   }
   
   public MobScriptable GetScriptable{ get { return scriptable;} }

   public MobsClothes GetClothes{ get { return clothes;} }
}
