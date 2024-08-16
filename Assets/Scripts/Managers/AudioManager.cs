using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Slider")]
    public GameObject bgmVolumeSliderObject;
    public GameObject sfxVolumeSliderObject;

    [Header("AudioClip")]
    public AudioClip clickSound;

    private Slider bgmVolumeSlider;
    private Slider sfxVolumeSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeSliders(); // 슬라이더 초기화 및 볼륨 설정

        bgmSource.Play(); // BGM 재생

        if (bgmVolumeSlider != null && sfxVolumeSlider != null)
        {
            UpdateBGMVolume(bgmVolumeSlider.value);
            UpdateSFXVolume(sfxVolumeSlider.value);
        }
    }

    public void InitializeSliders() // 슬라이더 초기화
    {
        if (bgmVolumeSliderObject != null)
        {
            bgmVolumeSlider = bgmVolumeSliderObject.GetComponent<Slider>();
            if (bgmVolumeSlider == null)
            {
                Debug.LogError("BGMVolumeSlider component not found on the GameObject.");
            }
        }
        else
        {
            bgmVolumeSlider = GameObject.Find("BGMVolumeSlider")?.GetComponent<Slider>();
            if (bgmVolumeSlider == null)
            {
                Debug.LogError("BGMVolumeSlider not found in the scene.");
            }
        }

        if (sfxVolumeSliderObject != null)
        {
            sfxVolumeSlider = sfxVolumeSliderObject.GetComponent<Slider>();
            if (sfxVolumeSlider == null)
            {
                Debug.LogError("SFXVolumeSlider component not found on the GameObject.");
            }
        }
        else
        {
            sfxVolumeSlider = GameObject.Find("SFXVolumeSlider")?.GetComponent<Slider>();
            if (sfxVolumeSlider == null)
            {
                Debug.LogError("SFXVolumeSlider not found in the scene.");
            }
        }

        if (bgmVolumeSlider != null && sfxVolumeSlider != null)
        {
            // 볼륨 슬라이더 초기화
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f); // 저장된 볼륨 불러오기, 없으면 기본값 0.5f로 설정
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            bgmVolumeSlider.value = bgmVolume;
            sfxVolumeSlider.value = sfxVolume;

            // 슬라이더 이벤트 등록
            bgmVolumeSlider.onValueChanged.AddListener(UpdateBGMVolume);
            sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);

            // 볼륨 설정
            UpdateBGMVolume(bgmVolume);
            UpdateSFXVolume(sfxVolume);
        }
    }

    public void UpdateBGMVolume(float volume) // BGM 볼륨 조절 메서드
    {
        if (bgmSource != null)
        {
            PlayerPrefs.SetFloat("BGMVolume", volume); // 설정된 볼륨 저장
            bgmSource.volume = volume; // BGM 볼륨 조절
        }
    }

    public void UpdateSFXVolume(float volume) // SFX 볼륨 조절 메서드
    {
        if (sfxSource != null)
        {
            PlayerPrefs.SetFloat("SFXVolume", volume); // 설정된 볼륨 저장
            sfxSource.volume = volume; // SFX 볼륨 조절
        }
    }

    public void PlayClickSound() // 클릭 시 효과음
    {
        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }
}
