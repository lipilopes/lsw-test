using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobsClothes),typeof(MobsClothes),typeof(PlayerMoviment))]
[RequireComponent(typeof(EmotionBallonManager))]
public class PlayerManager : MonoBehaviour
{
   [SerializeField] MobScriptable scriptable;
   

   public MobScriptable GetScriptable{get { return scriptable;} }
}
