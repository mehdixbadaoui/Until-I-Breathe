using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLenghtSound : MonoBehaviour
{
    //Wwwise playingID
    static uint[] playingIds = new uint[30];
    // Start is called before the first frame update
    

    public bool IsEventPlayingOnGameObject(string eventName, GameObject go)
    {
        uint testEventId = AkSoundEngine.GetIDFromString(eventName);

        uint count = (uint) playingIds.Length;
        AKRESULT result = AkSoundEngine.GetPlayingIDsFromGameObject(go, ref count, playingIds);

        for (int i = 0; i < count; i++)
        {
            uint playingId = playingIds[i];
            uint eventId = AkSoundEngine.GetEventIDFromPlayingID(playingId);

            if (eventId == testEventId)
                return true;
        }

        return false;
    }
}
