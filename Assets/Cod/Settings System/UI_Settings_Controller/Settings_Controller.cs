using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Settings_Controller : MonoBehaviour
{
    [Header("Settings UI")]
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] Button openButton;
    [SerializeField] Button closeButton;
    [SerializeField] Button musicONImage;    
    [SerializeField] Button musicOFFImage;

    [Space]
    [Header("Rewards Database")]
    [SerializeField] SettingsData settingsData;


    /*    [Space]
        [Header("Rewards Images")]
        [SerializeField] Sprite iconCoinsSprite;*/

    void Start()
    {
        //settingsData.CheckFileExist();
        if (!Directory.Exists(Application.dataPath + "/Resources"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources");

        }
        if (!File.Exists(Application.dataPath + "/Resources/XMLSettingsData.xml"))
        {
            File.Create(Application.dataPath + "/Resources/XMLSettingsData.xml");
        }
        else
        {
            settingsData.LoadInfo(true);
        }
        
        
        
        Initialize();
        
    }

    private void Initialize()
    {
        //Add click events
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(OnOpenButtonClick);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnCloseButtonClick);

        musicONImage.onClick.RemoveAllListeners();
        musicONImage.onClick.AddListener(MusicONButtonClick);

        musicOFFImage.onClick.RemoveAllListeners();
        musicOFFImage.onClick.AddListener(MusicOFFButtonClick);

        /*closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnCloseButtonClick);*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    void OnOpenButtonClick()
    {
        settingsCanvas.SetActive(true);
        settingsData.LoadInfo(false);
    }

    void OnCloseButtonClick()
    {
        settingsData.SaveInfo();
        Debug.Log(AudioController.volume);
        Debug.Log(settingsData.musicVolume);
        settingsCanvas.SetActive(false);
        
    }

    void MusicONButtonClick()
    {
        AudioController.OnMusic(musicONImage.image, musicOFFImage.image);
    }

    void MusicOFFButtonClick()
    {
        AudioController.OffMusic(musicONImage.image, musicOFFImage.image);
    }

    private void OnApplicationQuit()
    {
        settingsData.SaveInfo();
    }
}
