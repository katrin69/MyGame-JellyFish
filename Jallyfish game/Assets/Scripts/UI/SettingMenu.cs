using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingMenu : MonoBehaviour
{
    //м еню настроек
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;//разрешение масив

    private void Start()
    {
        resolutions = Screen.resolutions; 

        resolutionDropdown.ClearOptions(); 

        List<string> options = new List<string>(); //опции

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)  //разрешение
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; 
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) 
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options); 
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue(); 
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    //Громкость
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Качество
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Полный экран
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
