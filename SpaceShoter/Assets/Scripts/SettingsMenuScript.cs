using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuScript : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixerMain;

    [Space]

    [Header("Graphic")]
    public TMP_Dropdown resolutionDropdown;

    [Space]

    [Header("Gameplay")]
    public TMP_InputField hazardCount;
    public TMP_InputField speedValue;
    public TMP_InputField accelValue;

    Resolution[] resolutions;

    private GameController gameController;

    void Start()
    {
        hazardCount.text = GameController.Instance.hazardCount.ToString();
        speedValue.text = MovementController.Instance.modifierSpeed.ToString();
        accelValue.text = MovementController.Instance.modifierAccel.ToString();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
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

    public void SetVolumeMain(float volumeMain)
    {
        audioMixerMain.SetFloat("volumeMain", volumeMain);

    }

    public void SetVolumeSFX(float volumeSFX)
    {
        audioMixerMain.SetFloat("volumeSFX", volumeSFX);

    }

    public void SetVolumeMusic(float volumeMusic)
    {
        audioMixerMain.SetFloat("volumeMusic", volumeMusic);

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality (int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }

    public void FullScreenTogle(bool SetFullscreen)
    {
        Screen.fullScreen = SetFullscreen;
    }

    public void ChangeHazardPerWave()
    {
        GameController.Instance.hazardCount = int.Parse(hazardCount.text);
    }

    public void ChangeSpeedModifier()
    {
        MovementController.Instance.modifierSpeed = int.Parse(speedValue.text);
    }

    public void ChangeAccelModifier()
    {
        MovementController.Instance.modifierAccel = int.Parse(accelValue.text);
    }
}
