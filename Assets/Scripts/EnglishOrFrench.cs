using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishOrFrench : MonoBehaviour
{

    private GameMaster gm;

    public GameObject[] French;
    public GameObject[] English;


    // Start is called before the first frame update
    void Start()
    {

        // Game manager
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        if (gm.isInEnglish)
        {
            foreach (GameObject GO in French )
            {
                Destroy(GO);
            }
        }
        else
        {
            foreach (GameObject GO in English)
            {
                Destroy(GO);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
