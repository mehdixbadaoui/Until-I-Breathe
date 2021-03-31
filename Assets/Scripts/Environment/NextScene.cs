using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    //public GameObject text;
    public GameObject background;
    //public GameObject progressBars;
    public GameObject maincamera; 
    private AsyncOperation async;
    private int loadProgress;

    private bool quit;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera"); 
    }

    public void LoadScenes(int s)
    {
        if (background)
            background.SetActive(true);
        async = SceneManager.LoadSceneAsync(s);
        async.allowSceneActivation = true;
        StartCoroutine(DisplayLoadingScreen());
    }
    IEnumerator DisplayLoadingScreen()
    {
        while (!async.isDone)
        {
            loadProgress = (int)(async.progress * 100);
            yield return null;
        }
        if (async.progress >= 0.9f)
        {
            Debug.Log("Loading complete");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //AkSoundEngine.PostEvent("Stop_music_event", maincamera);
        maincamera.GetComponent<AkAmbient>().Stop(10);  
        if (other.tag == "uni")
        {
            
            LoadScenes( SceneManager.GetActiveScene().buildIndex + 1 );
        }
    }
}
