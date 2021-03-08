using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    // Beathing mecanic
    private Breathing_mechanic bm;

    // Game Manager
    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        // Game master
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "uni")
        {
            gm.Die();
        }
    }
}
