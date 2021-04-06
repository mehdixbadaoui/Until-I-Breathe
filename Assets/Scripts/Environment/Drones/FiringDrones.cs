using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringDrones : MonoBehaviour
{
    public Transform player;

    public float timeToSpotPlayer = .5f;
    public Light spotlight;
    public LayerMask obsMask;

    public float timeToKillPlayer = .5f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    float playerVisibleTimer;
    bool detected;
    bool dead;

    Color originalSpotlightColour;
    GameMaster GM;

    void Start()
    {
        GM = FindObjectOfType<GameMaster>();
        originalSpotlightColour = spotlight.color;
    }

    void Update()
    {
        if (detected)
        {
            playerVisibleTimer += Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            StartCoroutine(CallShootWithDelay());
        }
    }

    IEnumerator CallShootWithDelay()
    {
        muzzleFlash.Play();
        GameObject impactGO = Instantiate(impactEffect, player.position, Quaternion.identity);
        Destroy(impactGO, 1f);
        yield return new WaitForSeconds(timeToKillPlayer);
        GM.Die();
        dead = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            Debug.DrawLine(transform.position, player.position);
            if (!Physics.Linecast(transform.position, player.position, obsMask))
            {
                detected = true;
            }
            else
            {
                detected = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        detected = false;
    }
}
