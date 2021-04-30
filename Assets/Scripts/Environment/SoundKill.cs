using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundKill : MonoBehaviour
{
    //Set this in unity
    public float ForceOndeBefore;
    public float ForceOndeAfter;

    // Cones
    private List<GameObject> cones = new List<GameObject>();

    // Previous color
    private Color previousColor;
    [Range(0.0f, 1.0f)]  public float previousColor_H = 0.5f;
    public float previousColor_H_notuseful;
    private float previousColor_S;
    private float previousColor_V;

    private GameMaster gm;
    private Animator myAnimator;
    public Ragdoll ragdoll;
    private List<GameObject> all_hooks;
    private List<string> all_hooks_tags;
    private GameObject player;
    private GrapplingHook grapplin;
    //Movement script
    private Movement movements;

    private RigidbodyConstraints previousContraints;

    //private bool killUni = false;
    public bool isPlaying = false;
    private bool haschanged = false;
    private bool isDying = false;

    // // Timers
    // public float timeOnePeriode;
    // public float soundlength;
    // public float timerSound = 0;
    // private float timerPlay = 0;

    // Start is called before the first frame update
    void Start()
    {
        previousColor = transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.GetColor("AlwaysKillingColor");
        Color.RGBToHSV(previousColor , out previousColor_H_notuseful, out previousColor_S , out previousColor_V );

        //List of hooks and tags
        all_hooks = new List<GameObject>();
        all_hooks_tags = new List<string>();

        // Game manager
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        // Uni Animator
        myAnimator = GameObject.FindGameObjectWithTag("uni").GetComponentInChildren<Animator>();

        // Uni
        player = GameObject.FindGameObjectWithTag("uni");

        //Get the cones shaders
        int count = transform.parent.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.parent.transform.GetChild(i);
            if (child.tag == "cone")
                cones.Add(child.gameObject);
        } 

        // Grapplin
        grapplin = player.GetComponent<GrapplingHook>();

        //Get rigidbodyCharacter component
        movements = player.GetComponent<Movement>();

        //Get the ragdoll
        ragdoll = myAnimator.gameObject.GetComponent<Ragdoll>();

    }

    // Update is called once per frame
    void Update()
    {
        // Si l'encinte s'allume
        if (isPlaying)
        {

            transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetFloat("AlwaysKillingForce", /*Mathf.Lerp(ForceOndeBefore, ForceOndeAfter, Time.time - startTime )*/ ForceOndeAfter);
            transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetColor("AlwaysKillingColor", Color.HSVToRGB( 0 , 100000 , 100000 ) );

            foreach (GameObject cone in cones)
            {
                if (!cone.activeSelf)
                    cone.SetActive(true);
            }



            if (all_hooks != null && !haschanged)
            {
                for (int hookId = 0; hookId < all_hooks.Count; ++hookId)
                {
                    // Si Uni est accrochée au hook, alors on la détache
                    if (Movement.isGrapplin && grapplin.hookObject == all_hooks[hookId])
                    {
                        grapplin.CutRope();
                        movements.JumpAfterGrapplin();
                    }

                    //Le tag du hook disparait
                    all_hooks[hookId].tag = "Untagged";

                    //Si le hook était dékà dans la liste du hook_detector on le supprime
                    if (grapplin.hook_detector.GetComponent<hook_detector>().all_hooks.Contains(all_hooks[hookId]))
                    {
                        grapplin.hook_detector.GetComponent<hook_detector>().all_hooks.Remove(all_hooks[hookId]);
                        all_hooks[hookId].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);

                    }


                }
                haschanged = true;
            }
        }
        // Si l'encinte s'eteint
        else
        {

            transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetFloat("AlwaysKillingForce", /*Mathf.Lerp(ForceOndeBefore, ForceOndeAfter, Time.time - startTime )*/ ForceOndeBefore);
            transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetColor("AlwaysKillingColor", Color.HSVToRGB( previousColor_H, previousColor_S, 0) );

            foreach (GameObject cone in cones)
            {
                if (cone.activeSelf)
                    cone.SetActive(false);
            }

            if (all_hooks != null && haschanged)
            {
                haschanged = false;

                for (int hookId = 0; hookId < all_hooks.Count; ++hookId)
                {
                    // On rend le tag au hook
                    all_hooks[hookId].tag = all_hooks_tags[hookId];


                    //Si le hook était dékà dans la liste du hook_detector on le supprime
                    if (grapplin.hook_detector.GetComponent<Collider>().bounds.Intersects(all_hooks[hookId].GetComponent<Collider>().bounds))
                    {
                        grapplin.hook_detector.GetComponent<hook_detector>().all_hooks.Add(all_hooks[hookId]);
                    }
                }



            }
        }






    }
    private void FixedUpdate()
    {

    }

    public void ShutDown()
    {
        isPlaying = false;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (!all_hooks.Contains(other.gameObject) && (other.tag == "hook" || other.tag == "movable_hook" || other.tag == "lever"))
        {
            all_hooks.Add(other.gameObject);
            all_hooks_tags.Add(other.gameObject.tag);
        }
    }


    private void OnTriggerExit(Collider other)

    {
        if (all_hooks.Contains(other.gameObject))
        {
            all_hooks.Remove(other.gameObject);
        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "uni" && isPlaying && !isDying)
        {
            isDying = true;
            StartCoroutine(Kill());
        }

    }

    private IEnumerator Kill()
    {
        Movement.canMove = false;

        if (grapplin.isGrappling)
        {
            grapplin.CutRope();
        }


        myAnimator.Play("DeathImpact", 0);
        //myAnimator.Play("BreathingDead", 1);


        //Wait for the beginning of BreathingDead
        yield return new WaitWhile(() => myAnimator.GetCurrentAnimatorStateInfo(0).IsName("DeathImpact"));

        //new WaitForSeconds(myAnimator.GetCurrentAnimatorStateInfo(1).length + myAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime);

        //Wait for the end of BreathingDead
        yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorStateInfo(0).length /*+ myAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime*/ );

        myAnimator.enabled = false;
        ragdoll.RagOn();

        ragdoll.AddForceToRagdoll(new Vector3(5000,1000,0));

        //previousContraints = player.GetComponent<Rigidbody>().constraints;
        //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None ;

        //player.GetComponent<Rigidbody>().AddForce(new Vector3(5 , 0 , 0));

        yield return new WaitForSeconds(3.0f);

        //player.GetComponent<Rigidbody>().constraints = previousContraints;

        ragdoll.RagOff();
        myAnimator.enabled = true;

        gm.Die();

        isDying = false;

        Movement.canMove = true;
    }

}
