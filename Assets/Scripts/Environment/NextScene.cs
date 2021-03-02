using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    //public GameObject text;
    public GameObject background;
    //public GameObject progressBars;

    private AsyncOperation async;
    private int loadProgress;

    private bool quit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScenes(int s)
    {
        if (!quit)
        {
            //SoundController.PlaySound(soundGame.ButtomClick);
            //progressBars.gameObject.SetActive(true);
            background.SetActive(true);
            //text.gameObject.SetActive(true);

            async = SceneManager.LoadSceneAsync(s);
            async.allowSceneActivation = true;
            StartCoroutine(DisplayLoadingScreen());
        }
    }
    IEnumerator DisplayLoadingScreen()
    {
        while (!async.isDone)
        {
            loadProgress = (int)(async.progress * 100);
            //text.text = "Loading Progress " + loadProgress + "%";
            //progressBars.size = loadProgress;
            Debug.Log(loadProgress);
            yield return null;
        }
        if (async.progress >= 0.9f)
        {
            Debug.Log("Loading complete");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni")
        {
            LoadScenes( SceneManager.GetActiveScene().buildIndex + 1 );
        }
    }
}
