using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ObjectDetector : MonoBehaviour
{

    private Inputs inputs;

    // List of objects in the object detector
    public List<GameObject> listObj = new List<GameObject>();

    SphereCollider sphere_collider;
    private GameObject uni;
    private PlayEventSounds playEvent;

    public float BoxFallDuration = 0.5f;

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

        sphere_collider = GetComponent<SphereCollider>();
        uni = GameObject.FindGameObjectWithTag("uni");
        playEvent = uni.GetComponent<PlayEventSounds>(); 
        inputs.Uni.PressButton.performed += ctx => Interact();
    }

    void Interact()
    {
        if (listObj != null)
        {
            for (int index = 0; index < listObj.Count; index++)
            {
                AkSoundEngine.PostEvent("Levier_event", uni);
                if (listObj[index].CompareTag("button"))
                {
                    for (int i = 0; i < listObj[index].GetComponent<button_detector>().caisses.Length; i++)
                    {
                        StartCoroutine(boxStop(listObj[index].GetComponent<button_detector>().caisses[i].GetComponent<Rigidbody>()));
                    }

                    foreach (GameObject light in listObj[index].GetComponent<button_detector>().lights)
                        light.GetComponent<Light>().color = Color.green;
                }
                else if (listObj[index].CompareTag("Levier_Level_1_3"))
                {
                    playEvent.PlayEventWithoutRTPC("Enceinte_shutdown_event", GameObject.FindGameObjectWithTag("WwiseSound"));
                    playEvent.PlayEventWithoutRTPC("Alarme_event", GameObject.FindGameObjectWithTag("WwiseSound"));
                    GameObject.FindGameObjectWithTag("globalLight1_3").SetActive(false); 
                    listObj[index].GetComponent<RisingFogLever>().LaunchGaz();
                    StartCoroutine(listObj[index].GetComponent<Lever>().RotateDoor());
                }
                else if (listObj[index].CompareTag("lever") && listObj[index].GetComponent<Lever>() && !listObj[index].GetComponent<Lever>().activated)
                {
                    listObj[index].GetComponent<Lever>().activated = true;

                    if (listObj[index].GetComponent<Lever>().Clip)
                    {
                        StartCoroutine(Cinematic(listObj[index].GetComponent<Lever>().Clip));
                    }
                    else
                    {
                        listObj[index].GetComponent<Lever>().Unlock();
                        if (listObj[index].GetComponent<Blackout>())
                            listObj[index].GetComponent<Blackout>().BO();
                    }
                }
                // Si c'est le levier blackout sans le truc lever
                else if (listObj[index].CompareTag("lever") && listObj[index].GetComponent<Blackout>())
                {
                            listObj[index].GetComponent<Blackout>().BO();
                }

                else if (listObj[index].GetComponent<LeverEnceinte>())
                {
                    listObj[index].GetComponent<LeverEnceinte>().isOn = true;
                }

                // else if (listObj[index].CompareTag("RisingFogLever"))
                // {
                //     listObj[index].GetComponent<RisingFogLever>().LaunchGaz();

                // }

                else if (listObj[index].CompareTag("Button_Platform"))
                {
                    listObj[index].GetComponent<ButtonPlatforms>().Increment();
                }

                else if (listObj[index].CompareTag("Lever_Platform"))
                {
                    listObj[index].GetComponent<LeverPlatforms>().GoTo();

                }
            }
        }
     }

    private IEnumerator boxStop(Rigidbody box)
    {
        Movement.canMove = false;

        box.isKinematic = false;
        box.useGravity = true;

        yield return new WaitForSeconds(BoxFallDuration);

        box.isKinematic = true;

        Movement.canMove = true;
    }
    private IEnumerator Cinematic(PlayableDirector playable)
    {
        Movement.canMove = false;

        if (playable)
        {
            playable.Play();

            yield return new WaitForSeconds((float)playable.duration);

            playable.Stop();
        }

        Movement.canMove = true;

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider col)
    {


        if (col.CompareTag("blowable") || col.CompareTag("fan")
            || col.CompareTag("button") || col.CompareTag("lever")
            || col.CompareTag("Button_Platform") || col.CompareTag("Lever_Platform")
            || col.CompareTag("RisingFogLever") || col.CompareTag("Levier_Level_1_3") || col.GetComponent<LeverEnceinte>() )
        {
            listObj.Add(col.gameObject);
        }

        /*

                if (col.CompareTag("blowable") || col.CompareTag("fan") )
                {
                    FindObjectOfType<Breathing_mechanic>().setBlowObj(col.gameObject);
                }


                if (col.CompareTag("button") || col.CompareTag("lever"))
                {

                    FindObjectOfType<button_detector>().setObj(col.gameObject);
                }*/




    }
    void OnTriggerExit(Collider col)
    {

        if (col.CompareTag("blowable") || col.CompareTag("fan") || col.CompareTag("button") || col.CompareTag("lever") || col.CompareTag("Button_Platform") || col.CompareTag("Lever_Platform") || col.CompareTag("Levier_Level_1_3"))
        {
            listObj.Remove(col.gameObject);
        }
/*
        if (col.CompareTag("blowable") || col.CompareTag("fan"))
        {
            FindObjectOfType<Breathing_mechanic>().setBlowObj(null);
        }



        if (col.CompareTag("button") || col.CompareTag("lever"))
        {
            FindObjectOfType<button_detector>().setObj(null);
        }*/

    }
}
