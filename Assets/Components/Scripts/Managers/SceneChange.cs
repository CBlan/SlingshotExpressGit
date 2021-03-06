﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    private GoogleAnalyticsV4 googleAnalytics;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeScene("ProBuild");
        }
    }

    public void ChangeScene(string name)
    {
        googleAnalytics = GameObject.FindGameObjectsWithTag("GA")[0].GetComponent<GoogleAnalyticsV4>();
        SceneManager.LoadScene(name);
        googleAnalytics.LogScreen(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}