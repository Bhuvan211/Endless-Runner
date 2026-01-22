using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audio; // Reference to the background music AudioSource
    [SerializeField] private AudioClip crash, startGame, endGame, bgm; // AudioClips for different game events
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(startGame);
        StartCoroutine(PlayBgm());
    }
    public void PlayCrash()
    {
        audio.Pause();
        audio.PlayOneShot(crash);
        StartCoroutine(PlayEndGame());
    }

    IEnumerator PlayBgm()
    {
        yield return new WaitForSeconds(startGame.length);
        audio.clip = bgm;
        audio.loop = true;
        audio.Play();
    }

    IEnumerator PlayEndGame()
    {
        yield return new WaitForSeconds(crash.length);
        audio.PlayOneShot(endGame);
    }
}
