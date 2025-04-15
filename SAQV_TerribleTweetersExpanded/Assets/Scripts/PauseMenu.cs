using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;
    [SerializeField] string _musicVolume = "MusicVolume";
    [SerializeField] string _sfxVolume = "SFXVolume";
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;
    [SerializeField] float _multiplier = 30f;
    [SerializeField] Toggle _musicToggle;
    [SerializeField] Toggle _sfxToggle;
    
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    private bool _disableToggleEvent;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(HandleMusicValueChanged);
        _sfxSlider.onValueChanged.AddListener(HandleSFXValueChanged);
        _musicToggle.onValueChanged.AddListener(HandleMusicMuteChanged);
        _sfxToggle.onValueChanged.AddListener(HandleSFXMuteChanged);
    }

    private void HandleMusicMuteChanged(bool enableMusic)
    {
        if (enableMusic)
            _musicSlider.value = _musicSlider.maxValue;
        else
            _musicSlider.value = _musicSlider.minValue;
    }

    private void HandleSFXMuteChanged(bool enableSFX)
    {
        if (_disableToggleEvent)
            return;
        
        if (enableSFX)
            _sfxSlider.value = _sfxSlider.maxValue;
        else
            _sfxSlider.value = _sfxSlider.minValue;
    }

    private void OnPauseDisable()
    {
        PlayerPrefs.SetFloat(_musicVolume, _musicSlider.value);
        PlayerPrefs.SetFloat(_sfxVolume, _sfxSlider.value);
    }

    private void HandleMusicValueChanged(float arg0)
    {
        _mixer.SetFloat(_musicVolume, Mathf.Log10(arg0)* _multiplier);
        _disableToggleEvent = true;
        _musicToggle.isOn = _musicSlider.value > _musicSlider.minValue;
        _disableToggleEvent = false;
    }

    private void HandleSFXValueChanged(float arg0)
    {
        _mixer.SetFloat(_sfxVolume, Mathf.Log10(arg0) * _multiplier);
        _disableToggleEvent = true;
        _sfxToggle.isOn = _sfxSlider.value > _sfxSlider.minValue;
        _disableToggleEvent = false;
    }

    void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat(_musicVolume, _musicSlider.value);
        _sfxSlider.value = PlayerPrefs.GetFloat(_sfxVolume, _sfxSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused) 
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        OnPauseDisable();
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

//    public 
}
