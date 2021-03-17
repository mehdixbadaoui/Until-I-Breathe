using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEvent : MonoBehaviour
{
    private Movement movement;
    private string typeOfGround; 

    private void Start()
    {
        movement = GetComponentInParent<Movement>();
    }
    private void Update()
    {
        typeOfGround = movement.typeOfGround;
    }
    public void callEvent(string s)
    {
        AkSoundEngine.PostEvent(s, gameObject);
    }
    public void callEventRun(string s)
    {
       if (movement.typeOfGround == "Ground_Beton")
            s = "FS_Beton_event"; 
        else if(movement.typeOfGround == "Ground_Metal")
            s = "FS_Metal_event";
        else if (movement.typeOfGround == "Ground_Ordure")
            s = "FS_Ordure_event";
        else if (movement.typeOfGround == "Ground_Bois")
            s = "FS_Bois_event";
        else if (movement.typeOfGround == "Ground_Liquide")
            s = "FS_Liquide_event";
        AkSoundEngine.PostEvent(s, gameObject);
        Debug.Log("PrintEvent: " + s + "Called at : " + Time.time); 
    }
    public void callEventClothes(string s)
    {
        AkSoundEngine.PostEvent(s, gameObject);
        Debug.Log("PrintEvent: " + s + "Called at : " + Time.time);
    }
    public void callEventCrouch(string s)
    {
        
        if (movement.typeOfGround == "Ground_Beton")
            s = "FS_crouch_Beton_event";
        else if (movement.typeOfGround == "Ground_Metal")
            s = "FS_crouch_Metal_event";
        else if (movement.typeOfGround == "Ground_Ordure")
            s = "FS_crouch_Ordure_event";
        else if (movement.typeOfGround == "Ground_Bois")
            s = "FS_crouch_Bois_event";
        else if (movement.typeOfGround == "Ground_Liquide")
            s = "FS_crouch_Liquide_event";
        AkSoundEngine.PostEvent(s, gameObject);
        Debug.Log("PrintEvent: " + s + "Called at : " + Time.time);
    }
}
