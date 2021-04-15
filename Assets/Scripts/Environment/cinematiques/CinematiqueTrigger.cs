using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CinematiqueTrigger : MonoBehaviour
{
    private bool Played = false;
    public PlayableDirector Clip;

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
        if (!Played)
        {
            Played = true;
            StartCoroutine(Cinematic(Clip));
        }
    }

    private IEnumerator Cinematic(PlayableDirector playable)
    {
        Movement.canMove = false;

        playable.Play();

        yield return new WaitForSeconds((float)playable.duration);

        playable.Stop();


        Movement.canMove = true;
    }

}
