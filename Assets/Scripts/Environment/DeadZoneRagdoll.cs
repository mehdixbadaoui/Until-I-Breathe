using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneRagdoll : MonoBehaviour
{

    // Game Manager
    private GameMaster gm;
    private GrapplingHook grapplin;
    private Ragdoll ragdoll;
    private Animator myAnimator;
    public bool droite = true;

    // Start is called before the first frame update
    void Start()
    {
        // Game master
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        // Get uni Breathing Mecanic
        grapplin = GameObject.FindGameObjectWithTag("uni").GetComponent<GrapplingHook>();

        // Get the animator 
        myAnimator = GameObject.FindGameObjectWithTag("uni").GetComponentInChildren<Animator>();

        //Get the ragdoll
        ragdoll = myAnimator.gameObject.GetComponent<Ragdoll>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni")
        {
            StartCoroutine(DeadDestroyed());
        }
    }


    IEnumerator DeadDestroyed()
    {
        Movement.canMove = false;

        if (grapplin.isGrappling)
            grapplin.CutRope();

        myAnimator.enabled = false;
        ragdoll.RagOn();

        if (droite)
            ragdoll.AddForceToRagdoll(new Vector3(0, 0, -1000));
        else
            ragdoll.AddForceToRagdoll(new Vector3(0, 0, 1000));

        yield return new WaitForSeconds(3.0f);


        ragdoll.RagOff();
        myAnimator.enabled = true;

        gm.Die();
        Movement.canMove = true;
    }

}
