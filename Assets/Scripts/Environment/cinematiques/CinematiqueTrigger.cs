using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CinematiqueTrigger : MonoBehaviour
{
    private bool Played = false;
    public PlayableDirector Clip;

    public bool blockMovements = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Played && other.CompareTag("uni"))
        {
            Played = true;
            StartCoroutine(Cinematic(Clip));
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (Played && other.CompareTag("uni") && blockMovements)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    private IEnumerator Cinematic(PlayableDirector playable)
    {
        if(blockMovements)
            Movement.canMove = false;

        playable.Play();

        yield return new WaitForSeconds((float)playable.duration);

        playable.Stop();


        if (blockMovements)
        {
            Movement.canMove = true;
        }
        else
            transform.parent.gameObject.SetActive(false);
    }

}
