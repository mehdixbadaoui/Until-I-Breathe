using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetDeadDrones : MonoBehaviour
{
    public Vector3 offsetDeadRobotRotation;
    public Vector3 offsetDeadRobotTranslation;

    [SerializeField] private bool blockForFirstDrone = false;
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
        if(other.tag == "uni" && blockForFirstDrone)
        {
            StartCoroutine(waitForPushE()); 
        }
    }

    private IEnumerator waitForPushE()
    {
        Movement.canMove = false;
        uni.GetComponent<Rigidbody>().velocity =  Vector3.zero;

        myAnimator.Play("idle&run", 0);

        yield return waitForKeyPress(KeyCode.E);
        Movement.canMove = true;
        blockForFirstDrone = false; 
    }
    private IEnumerator waitForKeyPress(KeyCode key)
    {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

        // now this function returns
    }
}
