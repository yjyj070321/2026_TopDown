using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Soundmanager : MonoBehaviour
{
    public static Soundmanager Instance;

    public AudioClip clipBGM;

    [Header("UI")]
    public Slider volumeSlider;

    public Toggle bgmToggle;

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

        // PlayerPrefs에서 BGM 상태 복구
        data.BGM =
            PlayerPrefs.GetInt(
                "BGM",
                1
            ) == 1;

        // 슬라이더 위치 복구
        if (
            volumeSlider != null
        )
        {
            volumeSlider
                .SetValueWithoutNotify(
                    data.volume
                );
        }

        // 토글 상태 복구
        if (
            bgmToggle != null
        )
        {
            bgmToggle
                .SetIsOnWithoutNotify(
                    data.BGM
                );
        }

        audioSource.volume =
            data.volume;

        audioSourceBGM.volume =
            data.BGM
            ?
            data.volume
            :
            0;

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
        // PlayerPrefs 저장
        PlayerPrefs.SetInt(
            "BGM",
            isOn
            ? 1
            : 0
        );

        PlayerPrefs.Save();

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