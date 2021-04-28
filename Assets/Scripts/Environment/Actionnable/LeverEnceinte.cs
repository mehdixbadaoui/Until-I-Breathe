using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LeverEnceinte : MonoBehaviour
{
    public bool isOn = false;
    private bool notActivated = true;
    public List<GameObject> lights;
    public List<SoundKill> ColliderEnceintes;
    public PlayableDirector Clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn && notActivated)
        {
            notActivated = false;
            StartCoroutine(DisplayClip());
        }
    }
    IEnumerator DisplayClip()
    {
        Movement.canMove = false;
        if (Clip != null)
        {
            Clip.Play();

            yield return new WaitForSeconds((float)Clip.duration);

            Clip.Stop();

        }
        else
        {
            foreach (SoundKill ColliderEnceinte in ColliderEnceintes)
                ColliderEnceinte.isPlaying = false;
        }

        foreach (GameObject light in lights)
            light.GetComponent<Light>().color = Color.green;

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
}
