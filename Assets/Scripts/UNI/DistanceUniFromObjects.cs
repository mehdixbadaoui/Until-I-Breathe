using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceUniFromObjects : MonoBehaviour
{
    private GameObject uni;
    private CheckLenghtSound checkLenghtSound;
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
    public void RTPCGameObjectValue(Vector3 distUniFromObject, float maxDistance, GameObject gameObject, string nameOfEvent, string nameOfRTPC)
    {

        bool isSoundFinished = checkLenghtSound.IsEventPlayingOnGameObject(nameOfEvent, gameObject);
        if (!isSoundFinished)
            AkSoundEngine.PostEvent(nameOfEvent, gameObject);
        if (distUniFromObject.z <= maxDistance && distUniFromObject.z > 0)
        {
            volume = Mathf.Abs(100 - distUniFromObject.z * 100f / maxDistance);
            AkSoundEngine.SetRTPCValue(nameOfRTPC, volume);
        }
        else if (distUniFromObject.z >= -maxDistance && distUniFromObject.z < 0)
        {
            volume = 100 - Mathf.Abs(distUniFromObject.z * 100f / maxDistance);
            AkSoundEngine.SetRTPCValue(nameOfRTPC, volume);
        }
        else
        {
            volume = 0f;
            AkSoundEngine.SetRTPCValue(nameOfRTPC, 0f);
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

    public Vector3 CalculateDistanceUniFromObject(float positionOfGameObject)
    {
        Vector3 distanceGameObjectFromUni = new Vector3(0, 0, positionOfGameObject - uni.transform.position.z);
        return distanceGameObjectFromUni; 
    }
}
