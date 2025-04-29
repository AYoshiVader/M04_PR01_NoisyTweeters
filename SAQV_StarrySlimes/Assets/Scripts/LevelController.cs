using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;

    Slime[] _slimes;

    void OnEnable()
    {
        _slimes = FindObjectsOfType<Slime>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SlimesAreAllDead())
            GoToNextLevel();
    }

    void GoToNextLevel()
    {
        Debug.Log("Got to level " + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

    bool SlimesAreAllDead()
    {
        foreach (var slime in _slimes)
        {
            if(slime.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
