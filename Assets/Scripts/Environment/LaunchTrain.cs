using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTrain : MonoBehaviour
{
    public GameObject Locomotive;
    public GameObject Track;
    public float speed = 20f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            BezierSpline track_bz = Track.GetComponent<BezierSpline>();
            Locomotive.GetComponent<SplineWalker>().spline = track_bz;
            Locomotive.GetComponent<SplineWalker>().progress = 0;
            Locomotive.GetComponent<SplineWalker>().speed = speed;
            //Locomotive.GetComponent<SplineWalker>().speed = 0;
            //StartCoroutine(SlowStart());

            //if (Locomotive.GetComponent<SplineWalker>().lookForward)
            //{
            //    transform.LookAt(Locomotive.transform.localPosition + track_bz.GetDirection(Locomotive.GetComponent<SplineWalker>().progress));
            //}
        }
    }

    IEnumerator SlowStart()
    {
        float speed = Locomotive.GetComponent<SplineWalker>().speed;

        float time = 0;
        float waitTime = 3f;

        //while (time < waitTime && Locomotive.GetComponent<SplineWalker>().speed < 20)
        //{
        //    Locomotive.GetComponent<SplineWalker>().speed = Mathf.Lerp(0/*Locomotive.GetComponent<SplineWalker>().speed*/, 20, time / waitTime);
        //    time += Time.deltaTime;
        //    yield return null;
        //}
        Locomotive.GetComponent<SplineWalker>().speed = speed;
        yield return null;


    }
}
