using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTrain : MonoBehaviour
{
    public GameObject Locomotive;
    public GameObject Track;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            BezierSpline track_bz = Track.GetComponent<BezierSpline>();
            Locomotive.GetComponent<SplineWalker>().spline = track_bz;
            Locomotive.GetComponent<SplineWalker>().progress = 0;

            //if (Locomotive.GetComponent<SplineWalker>().lookForward)
            //{
            //    transform.LookAt(Locomotive.transform.localPosition + track_bz.GetDirection(Locomotive.GetComponent<SplineWalker>().progress));
            //}
        }
    }
}
