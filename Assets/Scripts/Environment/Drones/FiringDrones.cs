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

    // Start is called before the first frame update
    void Start()
    {
        // Ref to the PlayerDetection Script
        playerDetectionScript = GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {            
        // //keeps track of the coroutine instantiated
        // IEnumerator shoot = CallShootWithDelay();

        // if (playerDetectionScript.detected)
        // {
        //     //keeps track of the coroutine instantiated
        //     IEnumerator shoot = Shoot();
        //     StartCoroutine(shoot);
        // }
    }

    IEnumerator CallShootWithDelay()
    {
        yield return new WaitForSeconds(delay);

        Debug.Log(playerDetectionScript.visibleTargets[0].transform.name);

        //muzzleFlash.Play();
        //Instantiate(impactEffect, playerDetectionScript.visibleTargets[0].transform.position, Quaternion.identity);
    }
}
