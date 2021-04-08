using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverEnceinte : MonoBehaviour
{
    public bool isOn = false;
    private bool notActivated = true;
    public List<GameObject> lights;
    public List<SoundKill> ColliderEnceintes;

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

            foreach (GameObject light in lights)
                light.GetComponent<Light>().color = Color.green;

            foreach (SoundKill ColliderEnceinte in ColliderEnceintes)
                ColliderEnceinte.isPlaying = false;
        }
    }
}
