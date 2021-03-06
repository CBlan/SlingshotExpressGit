﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public SoundManager soundManager;
    public LevelManager levelManager;
    public PauseManager pauseManager;
    public GoogleAnalyticsV4 googleAnalytics;

    public GameObject playerPrefab;
    public GameObject player;

    //public Camera cam;

    public static GameManager GM;

    public AudioClip backgroundMusic;

    private void Start()
    {

        googleAnalytics = GameObject.FindGameObjectsWithTag("GA")[0].GetComponent<GoogleAnalyticsV4>();
        googleAnalytics.StartSession();
        GM = this;
        soundManager.PlaySoundLoop(backgroundMusic, transform.position);
        player = GameObject.FindGameObjectWithTag("Player");
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    public void SceneChange(string scene)
    {

        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ResetPlayer()
    {
        //print("Retry");
        Destroy(GameObject.FindGameObjectWithTag("Player"));


        GameObject playerCopy = Instantiate(playerPrefab, levelManager.spawnPos.position, Quaternion.identity);
        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        playerCopy.GetComponent<SlingController>().slingMiddle = levelManager.slingMid;
        playerCopy.GetComponentInChildren<CameraController>().zoomPoint = levelManager.zoom;
        player = playerCopy;
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        scoreManager.DeactivateUI();
    }

    public void RetryGame()
    {
        googleAnalytics.LogEvent("Player Events", "Player Replayed", "Replay", 1);
    }

}
