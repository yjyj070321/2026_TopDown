using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Soundmanager : MonoBehaviour
{
    public static Soundmanager Instance;

    public AudioClip clipBGM;

    [Header("UI")]
    public Slider volumeSlider;

    AudioSource audioSourceBGM;
    AudioSource audioSource;

    void Awake()
    {
        Instance = this;

        audioSourceBGM =
            gameObject.AddComponent<AudioSource>();

        audioSource =
            gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        PlayerData data =
            GameDataManager
            .Instance
            .playerData;

        // 슬라이더 위치 복구
        if (
            volumeSlider != null
        )
        {
            volumeSlider.SetValueWithoutNotify(
                data.volume
            );
        }

        audioSource.volume =
            data.volume;

        if (data.BGM)
        {
            audioSourceBGM.volume =
                data.volume;
        }
        else
        {
            audioSourceBGM.volume =
                0;
        }

        PlayBGM();
    }

    public void PlayBGM()
    {
        audioSourceBGM.clip =
            clipBGM;

        audioSourceBGM.loop =
            true;

        audioSourceBGM.playOnAwake =
            false;

        audioSourceBGM.Play();
    }

    public void OnOffBGM(
        bool isOn
    )
    {
        GameDataManager
            .Instance
            .playerData
            .BGM =
            isOn;

        audioSourceBGM.volume =
            isOn
            ?
            GameDataManager
            .Instance
            .playerData
            .volume
            :
            0;

        GameDataManager
            .Instance
            .SaveData(
                GameDataManager
                .Instance
                .playerData
            );
    }

    public void ChangeBGMVolume(
        float volume
    )
    {
        GameDataManager
            .Instance
            .playerData
            .volume =
            volume;

        if (
            GameDataManager
            .Instance
            .playerData
            .BGM
        )
        {
            audioSourceBGM.volume =
                volume;
        }

        GameDataManager
            .Instance
            .SaveData(
                GameDataManager
                .Instance
                .playerData
            );
    }

    public void ChangeTextVolume(
        float volume
    )
    {
        audioSource.volume =
            volume;
    }
}