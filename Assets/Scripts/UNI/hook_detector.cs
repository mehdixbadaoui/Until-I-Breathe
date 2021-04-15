using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class hook_detector : MonoBehaviour
{
    private Inputs inputs;

    /*[HideInInspector] */public GameObject nearest_hook;
    private GameObject player;
    public List<GameObject> all_hooks;
    

    [HideInInspector] public bool nearHook = false;

    //HINTS & DEAD ROBOTS
    [HideInInspector] public bool nearHint = false;
    [HideInInspector] public bool nearDead = false;
    [HideInInspector] public Transform hintPosition;
    [HideInInspector] public Transform deadPosition;

    [SerializeField] private int index = 0;

    private GameMaster GM;
    public bool gamepad = false;

    public List<GameObject> hooks_up;
    public List<GameObject> hooks_down;
    public List<GameObject> hooks_right;
    public List<GameObject> hooks_left;

    private bool b_up;
    private bool b_down;
    private bool b_right;
    private bool b_left;

    Vector3 LastPosition;
    private void Awake()
    {
        inputs = new Inputs();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        all_hooks = new List<GameObject>();
        hooks_up = new List<GameObject>();
        hooks_down = new List<GameObject>();
        hooks_right = new List<GameObject>();
        hooks_left = new List<GameObject>();
        
        player = GameObject.FindGameObjectWithTag("uni");

        GM = FindObjectOfType<GameMaster>();

        b_up = true;
        b_down = true;
        b_right = true;
        b_left = true;

        //inputs.Uni.NextHook.performed += ctx => NextIndex();
        //inputs.Uni.PrevHook.performed += ctx => PrevIndex();

        //inputs.Uni.rightup.performed += ctx => up();
        //inputs.Uni.rightdown.performed += ctx => down();
        //inputs.Uni.rightright.performed += ctx => right();
        //inputs.Uni.rightleft.performed += ctx => left();
    }

    //Some Lazy Coding bc i gave up
    void right()
    {
        if(hooks_right.Any())
            nearest_hook = hooks_right[0];
        UpdateLists();
    }

    void left()
    {
        if(hooks_left.Any())
            nearest_hook = hooks_left[0];
        UpdateLists();
    }

    void up()
    {
        if(hooks_up.Any())
            nearest_hook = hooks_up[0];
        UpdateLists();
    }

    void down()
    {
        if(hooks_down.Any())
            nearest_hook = hooks_down[0];
        UpdateLists();
    }

    void UpdateLists()
    {
        all_hooks = all_hooks.OrderBy(o => Vector3.Distance(o.transform.position, nearest_hook.transform.position)).ToList();
        //nearest_hook = all_hooks[index % all_hooks.Count];
        hooks_up.Clear();
        hooks_down.Clear();
        hooks_right.Clear();
        hooks_left.Clear();

        foreach (GameObject h in all_hooks)
        {
            if (h.transform.position.y > nearest_hook.transform.position.y && !hooks_up.Contains(h))
                hooks_up.Add(h);
            else if (h.transform.position.y < nearest_hook.transform.position.y && !hooks_down.Contains(h))
                hooks_down.Add(h);

            if (h.transform.position.z > nearest_hook.transform.position.z && !hooks_right.Contains(h))
                hooks_right.Add(h);
            else if (h.transform.position.z < nearest_hook.transform.position.z && !hooks_left.Contains(h))
                hooks_left.Add(h);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
        if (all_hooks.Count != 0)
        {

            //IF NO HOOK IS SELECTED 
            if (!nearest_hook)
            {

                int j = 0;

                nearest_hook = all_hooks[j];
                UpdateLists();

                while (j < all_hooks.Count && (Physics.Raycast(gameObject.GetComponent<Collider>().bounds.center, (all_hooks[j].transform.position - gameObject.GetComponent<Collider>().bounds.center).normalized,
                    Vector3.Distance(all_hooks[j].transform.position, gameObject.GetComponent<Collider>().bounds.center)) ||
                    Physics.Raycast(all_hooks[j].transform.position, (gameObject.GetComponent<Collider>().bounds.center - all_hooks[j].transform.position).normalized,
                    Vector3.Distance(all_hooks[j].transform.position, gameObject.GetComponent<Collider>().bounds.center))))
                {
                    j++;

                }
                if (j >= all_hooks.Count)
                {
                    nearest_hook = null;
                    UpdateLists();
                }

                else
                {
                    nearest_hook = all_hooks[j];
                    UpdateLists();
                }


            }

            //SELECT UHOOK WITH GAMEPAD
            if (GM.gamepad)
            {
                //SHITCODING
                if (inputs.Uni.rightup.ReadValue<float>() == 1 && b_up)
                {
                    up();
                    b_up = false;
                }
                else if (inputs.Uni.rightup.ReadValue<float>() == 0)
                    b_up = true;

                if (inputs.Uni.rightdown.ReadValue<float>() == 1 && b_down)
                {
                    down();
                    b_down = false;
                }
                else if (inputs.Uni.rightdown.ReadValue<float>() == 0)
                    b_down = true;

                if (inputs.Uni.rightleft.ReadValue<float>() == 1 && b_left)
                {
                    left();
                    b_left = false;
                }
                else if (inputs.Uni.rightleft.ReadValue<float>() == 0)
                    b_left = true;

                if (inputs.Uni.rightright.ReadValue<float>() == 1 && b_right)
                {
                    right();
                    b_right = false;
                }
                else if (inputs.Uni.rightright.ReadValue<float>() == 0)
                    b_right = true;

            }
                

            //SELECT HOOK WITH MOUSE CURSOR
            else
            {
                all_hooks = all_hooks.OrderBy(o => Vector3.Distance(Camera.main.WorldToScreenPoint(o.transform.position), Mouse.current.position.ReadValue())).ToList();
                //CHOOSE THE NEAREST HOOK TO THE CURSOR
                //if (Vector3.Distance(Mouse.current.position.ReadValue(), LastPosition) > .1f)
                //{
                //JUST PICK THE NEAREST ONE
                //nearest_hook = all_hooks[0];

                //PICK THE NEAREST AVAILABLE HOOK
                int i = 0;

                    nearest_hook = all_hooks[i];
                    while ( i < all_hooks.Count && (Physics.Raycast(gameObject.GetComponent<Collider>().bounds.center, (all_hooks[i].transform.position - gameObject.GetComponent<Collider>().bounds.center).normalized,
                        Vector3.Distance(all_hooks[i].transform.position, gameObject.GetComponent<Collider>().bounds.center)) ||
                        Physics.Raycast(all_hooks[i].transform.position, (gameObject.GetComponent<Collider>().bounds.center - all_hooks[i].transform.position).normalized,
                        Vector3.Distance(all_hooks[i].transform.position, gameObject.GetComponent<Collider>().bounds.center))))
                    {
                        i++;

                    }
                    if (i >= all_hooks.Count)
                        nearest_hook = null;
                    else
                        nearest_hook = all_hooks[i];

                //IF OBSTACLE BETWEEN UNI AND HOOK
                if (nearest_hook)
                {
                    if (Physics.Raycast(gameObject.GetComponent<Collider>().bounds.center, (nearest_hook.transform.position - gameObject.GetComponent<Collider>().bounds.center).normalized,
                                Vector3.Distance(nearest_hook.transform.position, gameObject.GetComponent<Collider>().bounds.center)) ||
                                Physics.Raycast(nearest_hook.transform.position, (gameObject.GetComponent<Collider>().bounds.center - nearest_hook.transform.position).normalized,
                                Vector3.Distance(nearest_hook.transform.position, gameObject.GetComponent<Collider>().bounds.center)))
                    {
                        nearest_hook = null;
                        UpdateLists();
                    }
                }

            }

            //}
        }
        else
        {
            nearest_hook = null;
            UpdateLists();
        }


        //CHANGE THE APPEARANCE OF THE SELECTED HOOK
        foreach(var h in all_hooks)
        {
            if(h == nearest_hook) h.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
            else h.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
        }

    }

    public GameObject nh()
    {
        return nearest_hook;
    }

    //void NextIndex()
    //{
    //    Debug.Log(inputs.Uni.NextHook.ReadValue<Vector2>());
    //    if (index == all_hooks.Count - 1)
    //        index = 0;
    //    else
    //        index++;
    //    //nearest_hook = all_hooks[all_hooks.IndexOf(nearest_hook) + 1];
    //}

    //void PrevIndex()
    //{
    //    Debug.Log(inputs.Uni.NextHook.ReadValue<Vector2>());
    //    if (index == 0)
    //        index += all_hooks.Count;
    //    else
    //        index--;
    //    //nearest_hook = all_hooks[all_hooks.IndexOf(nearest_hook) - 1];
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (!all_hooks.Contains(other.gameObject) && (other.CompareTag("hook") || other.CompareTag("movable_hook") || other.CompareTag("boxhook")) )
        {
            all_hooks.Add(other.gameObject);
            nearHook = true;
        }
        
        else

        // HINTS
        if (other.tag == "Hint")
        {
            hintPosition = other.gameObject.transform;
            nearHint = true;
        }

        // DEAD ROBOT
        if (other.tag == "Dead")
        {
            deadPosition = other.gameObject.transform;
            nearDead = true;
        }

       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("hook") || other.CompareTag("movable_hook") || other.CompareTag("boxhook"))
        {
            nearHook = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == nearest_hook)
        {
            nearest_hook = null;
        }

        if (all_hooks.Contains(other.gameObject))
        {
            all_hooks.Remove(other.gameObject);
            other.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
            nearHook = false;
        }
       
        // HINTS
        if (other.CompareTag("Hint"))
        {
            nearHint = false;
        }

        //DEAD
        if (other.CompareTag("Dead"))
        {
            nearDead = false;
        }
    }
}
