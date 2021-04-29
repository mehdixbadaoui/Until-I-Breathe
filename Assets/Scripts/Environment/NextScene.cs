using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NextScene : MonoBehaviour
{

    //public GameObject text;
    public GameObject background;
    public GameObject Uni;
    //public GameObject progressBars;
    public GameObject maincamera; 
    private AsyncOperation async;
    private int loadProgress;

    public PlayableDirector Clip;

    private bool quit;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void LoadScenes(int s)
    {
        if (background)
            background.SetActive(true);
        async = SceneManager.LoadSceneAsync(s);
        async.allowSceneActivation = false;
        StartCoroutine(DisplayLoadingScreen());
    }
    IEnumerator DisplayLoadingScreen()
    {
        Movement.canMove = false;
        if (Clip != null)
        {
            Debug.Log(" A CLIP IS PLAYING");

            Clip.Play();

            yield return new WaitForSeconds((float)Clip.duration);

            Clip.Stop();
        }
        async.allowSceneActivation = true;
        Movement.canMove = true;

/*        while (!async.isDone)
        {
            loadProgress = (int)(async.progress * 100);
            yield return null;
        }
        if (async.progress >= 0.9f)
        {
            Debug.Log("Loading complete");
        }*/

    }

    void OnTriggerEnter(Collider other)
    {
        AkSoundEngine.PostEvent("Stop_music_event", GameObject.FindGameObjectWithTag("WwiseSound") );
        //maincamera.GetComponent<AkAmbient>().Stop(10);  
        if (other.tag == "uni")
        {
            Uni = other.gameObject;
            LoadScenes( SceneManager.GetActiveScene().buildIndex + 1 );
        }
    }
}
