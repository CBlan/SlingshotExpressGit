using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundOnce(AudioClip audioClip)
    {
        StartCoroutine(PlaySoundCoroutine(audioClip));
    }

    IEnumerator PlaySoundCoroutine(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        Destroy(gameObject);
    }

    public void PlaySoundLoop(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
        Destroy(gameObject);
    }
}
