using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject audioPlayerPrefab;

    //The function instantiates a dummy AudioSource gameobject, gives it the clip and plays it. The dummy destroys itself when audio stops playing.
    //Call it using managers path .playSFX(myAudioClip, myPosition);
    //This is for objects that get deleted, but need to play a sound before they die.
    public void PlayAndDestroy(AudioClip clip, Vector3 position)
    {
        var newSound = Instantiate(audioPlayerPrefab, position, Quaternion.identity) as GameObject;
        newSound.gameObject.GetComponent<AudioSource>().clip = clip;
        newSound.gameObject.GetComponent<AudioPlayer>().PlaySoundOnce(clip);
    }

    public GameObject PlaySoundLoop(AudioClip clip, Vector3 position)
    {
        var newSound = Instantiate(audioPlayerPrefab, position, Quaternion.identity) as GameObject;
        newSound.gameObject.GetComponent<AudioSource>().clip = clip;
        newSound.gameObject.GetComponent<AudioPlayer>().PlaySoundLoop(clip);
        return newSound;
    }
}
