using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class Soundmanager : MonoBehaviour
{
    public static Soundmanager Instance;

    public AudioClip clipBGM;

    AudioSource audioSourceBGM;
    AudioSource audioSource;

    private void Start()
    {
        PlayerData data = GameDataManager.Instance.playerData;

        audioSourceBGM.volume = data.volume;

        if (data.BGM == false)
        {
            audioSourceBGM.volume = 0;
        }

        audioSource.volume = data.volume;

        PlayBGM();
    }

    void Awake()
    {
        Instance = this;

        audioSourceBGM = gameObject.AddComponent<AudioSource>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBGM()
    {
        audioSourceBGM.clip = clipBGM;
        audioSourceBGM.loop = true;
        audioSourceBGM.playOnAwake = false;

        audioSourceBGM.Play();
    }

    public void OnOffBGM(bool isOn)
    {
        GameDataManager.Instance.playerData.BGM = isOn;

        if (isOn)
        {
            audioSourceBGM.volume =
                GameDataManager.Instance.playerData.volume;
        }
        else
        {
            audioSourceBGM.volume = 0;
        }

        GameDataManager.Instance.SaveData(
            GameDataManager.Instance.playerData
        );
    }

    public void ChangeBGMVolume(float volume)
    {
        audioSourceBGM.volume = volume;

        GameDataManager.Instance.playerData.volume = volume;

        GameDataManager.Instance.SaveData(
            GameDataManager.Instance.playerData
        );
    }

    public void ChangeTextVolume(float volume)
    {
        audioSource.volume = volume;

        GameDataManager.Instance.playerData.volume = volume;

        GameDataManager.Instance.SaveData(
            GameDataManager.Instance.playerData
        );
    }
}