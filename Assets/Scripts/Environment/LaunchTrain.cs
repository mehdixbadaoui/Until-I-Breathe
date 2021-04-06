using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTrain : MonoBehaviour
{
    public GameObject Locomotive;
    public GameObject Track;
    public float speed = 20f;
    public float time = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            BezierSpline track_bz = Track.GetComponent<BezierSpline>();
            Locomotive.GetComponent<SplineWalker>().spline = track_bz;
            Locomotive.GetComponent<SplineWalker>().progress = 0;
            //Locomotive.GetComponent<SplineWalker>().speed = speed;

            Locomotive.GetComponent<SplineWalker>().speed = 0;
            StartCoroutine(SlowStart());

            //if (Locomotive.GetComponent<SplineWalker>().lookForward)
            //{
            //    transform.LookAt(Locomotive.transform.localPosition + track_bz.GetDirection(Locomotive.GetComponent<SplineWalker>().progress));
            //}
        }
    }

    IEnumerator SlowStart()
    {
        float current_speed = Locomotive.GetComponent<SplineWalker>().speed;

        float t = 0;

        while (t < time && current_speed < speed)
        {
            Locomotive.GetComponent<SplineWalker>().speed = Mathf.Lerp(0/*Locomotive.GetComponent<SplineWalker>().speed*/, speed, t / time);
            t += Time.deltaTime;
            yield return null;
        }
        Locomotive.GetComponent<SplineWalker>().speed = speed;
        yield return null;


    }
}
