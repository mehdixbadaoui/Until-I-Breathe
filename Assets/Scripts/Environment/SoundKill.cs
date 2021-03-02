using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundKill : MonoBehaviour
{

    private GameMaster gm;
    private List<GameObject> all_hooks;
    private List<string> all_hooks_tags;
    public GameObject player;
    private GrapplingHook grapplin;

    private bool killUni = false;
    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        // Game manager
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        // Uni
        player = GameObject.FindGameObjectWithTag("uni");

        // Grapplin
        grapplin = player.GetComponent<GrapplingHook>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isOn && killUni)
        {
            killUni = false;
            gm.Die();
        }

        if (isOn && all_hooks != null)
        {
            for (int hookId = 0; hookId < all_hooks.Count ; ++hookId)
            {
                if ( Movement.isGrapplin && grapplin.hook_detector.GetComponent<hook_detector>().nearest_hook == all_hooks[hookId])
                {
                    grapplin.CutRope();
                }

                all_hooks[hookId].tag = "Untagged";
            }
        }
        if (!isOn && all_hooks != null)
        {
            for (int hookId = 0; hookId < all_hooks.Count; ++hookId)
            {
                all_hooks[hookId].tag = all_hooks_tags[hookId];
            }
        }

    }


    private void OnTriggerStay(Collider other)
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
}
