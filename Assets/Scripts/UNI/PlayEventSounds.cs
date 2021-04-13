using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEventSounds : MonoBehaviour
{
    private GameObject uni;
    private CheckLenghtSound checkLenghtSound;
    private float volumeZ;
    private float volumeY;
    private float volume; 

    // Start is called before the first frame update
    void Start()
    {
        uni = this.gameObject;
        checkLenghtSound = uni.GetComponent<CheckLenghtSound>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RTPCGameObjectValue(Vector3 distUniFromObject, float maxDistance, GameObject gameObject, string nameOfEvent, string nameOfRTPC = "", float coeffSpeed = 1)
    {

        
        if (((distUniFromObject.z <= maxDistance && distUniFromObject.z > 0 ) || (distUniFromObject.z >= -maxDistance && distUniFromObject.z < 0)) || (distUniFromObject.y <= maxDistance && distUniFromObject.y > 0) || (distUniFromObject.y >= -maxDistance && distUniFromObject.y < 0)/*|| (distUniFromObject.y <= maxDistance)*/)
        {
            volumeZ = (100 - Mathf.Abs(distUniFromObject.z * 100f / maxDistance) )* coeffSpeed;
            volumeY = (100 - Mathf.Abs(distUniFromObject.y * 100f / maxDistance)) * coeffSpeed;
            volume = Mathf.Min(volumeZ, volumeY); 
            AkSoundEngine.SetRTPCValue(nameOfRTPC, volume);
        }
        
        
        
        else
        {
            volume = 0f;
            AkSoundEngine.SetRTPCValue(nameOfRTPC, volume);
        }
        bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject(nameOfEvent, gameObject);
        if (!isSoundFinished)
            AkSoundEngine.PostEvent(nameOfEvent, gameObject);
    }
    public void LaunchSoundsonLevels(Vector3 distUniFromTrigger, float maxDistance, string nameOfRTPC = "", GameObject gameObject = null, string nameOfEvent = "")
    {
        if ((distUniFromTrigger.z <= maxDistance && distUniFromTrigger.z > 0))
        {
            volume = (100 - Mathf.Abs(distUniFromTrigger.z * 100f / maxDistance));
            AkSoundEngine.SetRTPCValue(nameOfRTPC, volume);
        }
        else if(distUniFromTrigger.z < 0)
        {
            volume = 100f;
            AkSoundEngine.SetRTPCValue(nameOfRTPC, volume);
        }
        else
        {
            volume = 0f;
            AkSoundEngine.SetRTPCValue(nameOfRTPC, volume);
        }
    }
    public void RTPCGameObjectValueEnterRoom(float distUniFromObjectLeft, float distUniFromObjectRight, float maxDistance, GameObject gameObject, string nameOfEvent, string nameOfRTPC, float coeffAttenuation )
    {
        bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject(nameOfEvent, gameObject);
        if (distUniFromObjectLeft <= maxDistance)
        {
            volume = distUniFromObjectLeft * 100f / maxDistance;

        }
        else if (distUniFromObjectRight <= maxDistance)
        {
            volume = distUniFromObjectRight * 100f / maxDistance;
        }
        else
        {
            volume = 100f;
        }
        float musicVolume = 100f - volume / coeffAttenuation;
        AkSoundEngine.SetRTPCValue(nameOfRTPC, volume);
        AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
        if (!isSoundFinished)
            AkSoundEngine.PostEvent(nameOfEvent, gameObject);
    }
    public void UniRespiration(string nameOfEvent, GameObject gameObject, float breathOfUni, string nameOfRTPC)
    {
        bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject(nameOfEvent, gameObject);
        AkSoundEngine.SetRTPCValue(nameOfRTPC, 110 - breathOfUni);
        if (!isSoundFinished)
            AkSoundEngine.PostEvent(nameOfEvent, gameObject);
    }
    public void UniSuffoc(string nameOfEvent, GameObject gameObject, float ValueOfRTPC, string nameOfRTPC)
    {
        bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject(nameOfEvent, gameObject);
        AkSoundEngine.SetRTPCValue(nameOfRTPC, ValueOfRTPC);
        if (!isSoundFinished)
            AkSoundEngine.PostEvent(nameOfEvent, gameObject);
    }

    public Vector3 CalculateDistanceUniFromObject(Vector3 positionOfGameObject)
    {
        Vector3 distanceGameObjectFromUni = positionOfGameObject - uni.transform.position;
        return distanceGameObjectFromUni; 
    }
    public void PlayEventWithoutRTPC(string nameOfEvent, GameObject gameObject)
    {
        bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject(nameOfEvent, gameObject);
        if (!isSoundFinished)
            AkSoundEngine.PostEvent(nameOfEvent, gameObject);
    }
}
