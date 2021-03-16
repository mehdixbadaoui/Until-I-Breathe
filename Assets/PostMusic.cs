using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMusic : MonoBehaviour
{
    public AK.Wwise.Event MusicEvent;
    // Start is called before the first frame update
    void Start()
    {
        MusicEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBar, CallBackFunction); 
    }

    void CallBackFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {

    }
}
