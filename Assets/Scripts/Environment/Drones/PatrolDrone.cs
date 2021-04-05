using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDrone : MonoBehaviour
{
    public Transform player;

    public float timeToSpotPlayer = .5f;
    public Light spotlight;
    public LayerMask viewMask;

    float playerVisibleTimer;
    bool detected;

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
            GM.Die();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            Debug.DrawLine(transform.position, player.position);
            if (!Physics.Linecast(transform.position, player.position, viewMask))
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
