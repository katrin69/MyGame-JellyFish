using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;//массив с разрешением жкрана

    private void Start()
    {
        resolutions = Screen.resolutions; //перемнная равная разрешения экрана

        resolutionDropdown.ClearOptions(); //очищает опции

        List<string> options = new List<string>(); //список вариантов разрешений

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) //перебираем каждый элемент в массиве разрешений
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; //из них создаём строку отображающую разрешение
            options.Add(option); //добавляем в список

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) //если текущее разрешение равно нашему разрешению , то устанавливаем его
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options); //после цикла добавим наш список опций в раскрывающееся меню разрешений
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue(); //отобразить значение
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    //Звук
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Качество
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Полноэкран режим
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
