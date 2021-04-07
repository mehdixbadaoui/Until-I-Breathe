using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundKill : MonoBehaviour
{
    //Set this in unity
    public float ForceOndeBefore;
    public float ForceOndeAfter;
    private Color previousColor;

    private GameMaster gm;
    private List<GameObject> all_hooks;
    private List<string> all_hooks_tags;
    private GameObject player;
    private GrapplingHook grapplin;
    //Movement script
    private Movement movements;

    private bool killUni = false;
    public bool isPlaying = false;
    private bool haschanged = false;

    // Timers
    public float timeOnePeriode;
    public float soundlength;
    private float timerSound = 0;
    private float timerPlay = 0;

    // Start is called before the first frame update
    void Start()
    {
        previousColor = transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.GetColor("Color_EDC6F8A5");

        //List of hooks and tags
        all_hooks = new List<GameObject>();
        all_hooks_tags = new List<string>();

        // Game manager
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        // Uni
        player = GameObject.FindGameObjectWithTag("uni");

        // Grapplin
        grapplin = player.GetComponent<GrapplingHook>();

        //Get rigidbodyCharacter component
        movements = player.GetComponent<Movement>();

    }

    // Update is called once per frame
    void Update()
    {
        // Si l'encinte s'allume
        if (isPlaying)
        {

            if (killUni)
            {
                killUni = false;
                gm.Die();
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
        timerSound += Time.deltaTime;
        if (timerSound > timeOnePeriode)
        {
            if (isPlaying)
                Debug.LogError("The soundlength is too long");

            isPlaying = true;
            timerSound = 0;
        }

        if (isPlaying)
        {
            timerPlay += Time.deltaTime;
            transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetFloat("Vector1_641F3F66", /*Mathf.Lerp(ForceOndeBefore, ForceOndeAfter, Time.time - startTime )*/ ForceOndeAfter);
            transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetColor("Color_EDC6F8A5", Color.red * 30);

            if (timerPlay > soundlength)
            {
                isPlaying = false;
                timerPlay = 0;
            }
        }
        else
        {
            transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetFloat("Vector1_641F3F66", /*Mathf.Lerp(ForceOndeBefore, ForceOndeAfter, Time.time - startTime )*/ ForceOndeBefore);
            transform.parent.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetColor("Color_EDC6F8A5", previousColor);
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (!all_hooks.Contains(other.gameObject) && (other.tag == "hook" || other.tag == "movable_hook" || other.tag == "lever"))
        {
            all_hooks.Add(other.gameObject);
            all_hooks_tags.Add(other.gameObject.tag);
        }
        else if (other.tag == "uni")
        {
            killUni = true;
        }
    }


    private void OnTriggerExit(Collider other)

    {
        if (all_hooks.Contains(other.gameObject))
        {
            all_hooks.Remove(other.gameObject);
        }


        else if (other.tag == "uni")
        {
            killUni = false;
        }

    }
}
