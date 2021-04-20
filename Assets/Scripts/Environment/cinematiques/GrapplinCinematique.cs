using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GrapplinCinematique : MonoBehaviour
{
    private bool Played = false;
    public PlayableDirector Clip1;
    public PlayableDirector Clip2;

    private GameObject uni;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {

        // Find the first character GameObject
        uni = GameObject.FindGameObjectWithTag("uni");

        // Get the animator 
        myAnimator = uni.GetComponentInChildren<Animator>();
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
            StartCoroutine(Cinematic(Clip1 , Clip2));
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (Played)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    private IEnumerator Cinematic(PlayableDirector playable1 , PlayableDirector playable2)
    {
        Movement.canMove = false;

        myAnimator.Play("idle&run" , 0);

        playable1.Play();

        yield return new WaitForSeconds((float)playable1.duration);

        yield return new WaitWhile(() => !Movement.isGrapplin);

        playable1.Stop();

        playable2.Play();

        yield return new WaitForSeconds((float)playable2.duration);

        playable2.Stop();


        Movement.canMove = true;
    }

}
