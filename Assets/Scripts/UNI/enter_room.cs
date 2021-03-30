using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enter_room : MonoBehaviour
{
    //private Collider collider;
    public GameObject facade;
    Renderer[] rends;
    private GameObject uni;
    Breathing_mechanic breathing_mechanic;

    // parameters for ambiant interior  sounds 
    private CheckLenghtSound checkLenghtSound;
    private GameObject room_inside;
    private float distanceEndRoomUniLeft;
    private float distanceEndRoomUniRight;
    public float ambiant_interior_volume;
    private BoxCollider room_inside_collider;
    public float dstUniFromEndofRoom = 7f; 
    public float musicVolume;
    public float coefficientAttenuation = 1.5f; 
    private void Start()
    {
        
        room_inside = this.gameObject;
        room_inside_collider = room_inside.GetComponent<BoxCollider>();
        uni = GameObject.FindGameObjectWithTag("uni");
        checkLenghtSound = uni.GetComponent<CheckLenghtSound>();
        if (facade)
            rends = facade.GetComponentsInChildren<Renderer>();

    }
    
    void OnTriggerStay(Collider col)
    {
        
        if (col.CompareTag("uni"))
        {
            distanceEndRoomUniLeft = uni.transform.position.z - room_inside_collider.bounds.min.z;
            distanceEndRoomUniRight = room_inside_collider.bounds.max.z - uni.transform.position.z; 

            //FADE THE FRONT WALL
            if (facade)
            {
                foreach (Renderer rend  in rends)
                {
                    Vector4 source = rend.material.GetColor("_BaseColor");
                    Vector4 target = new Vector4(source.x, source.y, source.z, 0);
                    if (rend.material.GetColor("_BaseColor").a != 0)
                    {
                        //StartCoroutine(fade(rend.material, source, target, .3f));
                        rend.material.SetColor("_BaseColor", Vector4.Lerp(rend.material.GetColor("_BaseColor"), target, .3f));
                    }
                }

            }

            //Ambiant music for interior
            
            bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject("Ambiant_interior_event", room_inside);
            if(distanceEndRoomUniLeft <= dstUniFromEndofRoom)
            {
                ambiant_interior_volume = distanceEndRoomUniLeft * 100f / dstUniFromEndofRoom;
                
            }
            else if(distanceEndRoomUniRight <= dstUniFromEndofRoom)
            {
                ambiant_interior_volume = distanceEndRoomUniRight * 100f / dstUniFromEndofRoom;
            }
            else
            {
                ambiant_interior_volume = 100f; 
            }
            musicVolume = 100f - ambiant_interior_volume / coefficientAttenuation; 
            AkSoundEngine.SetRTPCValue("Ambiant_music_Sound", ambiant_interior_volume);
            AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
            if (!isSoundFinished)
                AkSoundEngine.PostEvent("Ambiant_interior_event", room_inside);

        }

        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("uni"))
        {
           
            if (facade)
            {
                foreach (Renderer rend in rends)
                {
                    Vector4 source = rend.material.GetColor("_BaseColor");
                    Vector4 target = new Vector4(source.x, source.y, source.z, 1f);

                    StartCoroutine(fade(rend.material, source, target, .3f));

                }
            }

            distanceEndRoomUniLeft = 0f;
            distanceEndRoomUniRight = 0f;
            ambiant_interior_volume = 0f;
            musicVolume = 100f; 
            AkSoundEngine.SetRTPCValue("Ambiant_music_Sound", ambiant_interior_volume);
        }
    }



    IEnumerator fade(Material material, Vector4 source, Vector4 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            material.SetColor("_BaseColor", Vector4.Lerp(source, target, (Time.time - startTime) / overTime));
            yield return null;
        }
        material.SetColor("_BaseColor", target);
    }

}
