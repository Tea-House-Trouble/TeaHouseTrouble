using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoRun : MonoBehaviour
{
    public static Transform PlayerTransform;

    //public BezierSpline spline;
    //public float duration;
    //private float progress;

    //public SplinePlayerRunMode mode;
    //public bool lookForward;
    //private bool goingForward = true;

    private void Update()
    {
        transform.position = transform.position + new Vector3(0.4f, 0, 0);
        //if (goingForward)
        //{
        //    progress += Time.deltaTime / duration;

        //    if (progress > 1f)
        //    {
        //        if (mode == SplinePlayerRunMode.Once)
        //        {
        //            progress = 1f;
        //        }
        //        else if (mode == SplinePlayerRunMode.Loop)
        //        {
        //            progress -= 1f;
        //        }
        //        else
        //        {
        //            progress = 2f - progress;
        //            goingForward = false;
        //        }
        //    }
        //}
        //else
        //{
        //    progress -= Time.deltaTime / duration;

        //    if (progress < 0f)
        //    {
        //        progress = -progress;
        //        goingForward = true;
        //    }
        //}

        //Vector3 position = spline.GetPoint(progress);
        //transform.localPosition = position;

        //if (lookForward)
        //{
        //    transform.LookAt(position + spline.GetDirection(progress));
        //}
    }


    private void Awake()
    {
        PlayerTransform = transform;
    }
}
