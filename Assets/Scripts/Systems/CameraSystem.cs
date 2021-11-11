using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSystem : MonoBehaviour
{
    [Header("Follow Cam")]
    [SerializeField]
    private Transform  target;
	[SerializeField]
    private float      smoothSpeed = 0.125f;
	[SerializeField]
    private Vector3    offset;

    private Vector3 TargetPosition { get { return (target.position + offset); } }


    public enum ShakeCamType
    {
        Crazy,
        Horizontal,
        Vertical
    }

    #if UNITY_EDITOR
    [Header("Only Test")]
    [ContextMenuItem("Shake This Cam", "TESTESHAKE")]
    [SerializeField]
    private float          _duration = 1;
    [Range (0,3)]
    [SerializeField]
    private float          _magnitude =1;
    [SerializeField]
    private ShakeCamType   _shakeCamType;
    [SerializeField]
    private bool           _followPlayer;


    void TESTESHAKE()
    {
        StartCoroutine(Shake(_duration, _magnitude,_shakeCamType,_followPlayer));
    }
    #endif

    void FixedUpdate ()
	{
		Vector3 desiredPosition     = TargetPosition;
		Vector3 smoothedPosition    = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position          = smoothedPosition;
	}

    public void ChangeTarget(Transform newTarget,bool flash= false)
    {
        if(newTarget == null || newTarget == target)
            return;

        target = newTarget;

        if(flash)
            transform.localPosition = TargetPosition;
    }

    #region  Shake Cam
    public IEnumerator Shake(float duration, float magnitude,ShakeCamType shakeType = ShakeCamType.Crazy,bool followPlayer=false,Transform newTarget = null,bool flash = true)
    {
        if(newTarget!=null)
           ChangeTarget(newTarget,flash);

        Vector3 originalPos = transform.localPosition;

        float   elapsed = 0f;
        float   minX    = -1;
        float   maxX    = 1;              
        float   minY    = -1;
        float   maxY    = 1;

            switch (shakeType)
            {
                case ShakeCamType.Horizontal:
                    minY = 0;
                    maxY = 0;
                 break;

                 case ShakeCamType.Vertical:
                    minX = 0;
                    maxX = 0;
                 break;

                 default:
                 break;
            }

        while (elapsed < duration)
        {
            float x = (followPlayer ? TargetPosition.x : originalPos.x) + Random.Range(minX , maxX) * magnitude;
            float y = (followPlayer ? TargetPosition.y : originalPos.y) + Random.Range(minY , maxY) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed +=Time.deltaTime;

             yield return null;
        }
    }
    #endregion
}
