using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBreathe : MonoBehaviour
{
    private Breathing_mechanic bm;
    public float BreathSpeed = 1f;
    public float InhaleSpeed = 1f;

    private Inputs inputs;

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
        bm = FindObjectOfType<Breathing_mechanic>();

        inputs.Uni.Inhale.performed += ctx => StartCoroutine(Inhale());

    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("uni") && enabled)
        {
            //DISABLE AIR LOSS
            bm.can_breath = true;
            bm.hold = false;

            if (bm.breath < bm.max_breath)
                bm.breath += BreathSpeed * Time.deltaTime;

            else if (bm.breath > bm.max_breath)
                bm.breath = bm.max_breath;


        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("uni") && enabled)
        {
            bm.can_breath = false;
            bm.hold = true;
        }
    }

    IEnumerator Inhale()
    {
        if (!bm.can_breath)
        {
            bm.breath -= 80 / 6;
        }
        else
        {

            float startTime = Time.time;
            while(Time.time < startTime + InhaleSpeed && bm.breath < bm.max_breath)
            {
                bm.breath += (Time.time - startTime) / InhaleSpeed;
                yield return null;
            }

            if (bm.breath > bm.max_breath)
                bm.breath = bm.max_breath;

        }
    }

}
