using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringDrones : MonoBehaviour
{
    // PlayerDetection Script
    PlayerDetection playerDetectionScript;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float delay;

    bool stop;

    // Start is called before the first frame update
    void Start()
    {
        // Ref to the PlayerDetection Script
        playerDetectionScript = GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {            
        //keeps track of the coroutine instantiated
        IEnumerator shoot = CallShootWithDelay();

        if (playerDetectionScript.detected)
        {
            StartCoroutine(shoot);
        }

        if (stop)
        {
            StopCoroutine(shoot);
        }
    }

    IEnumerator CallShootWithDelay()
    {
        yield return new WaitForSeconds(delay);
        Shoot();
    }

    void Shoot()
    {
        muzzleFlash.Play();
        GameObject impactGO = Instantiate(impactEffect, playerDetectionScript.visibleTargets[0].transform.position, Quaternion.identity);
        Destroy(impactGO, 1f);
        stop = true;
    }
}
