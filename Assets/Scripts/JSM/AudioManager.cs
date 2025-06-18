using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource walkSource;

    [Header("Audio Clips")]
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;
    public AudioClip[] walkClips;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        SetBGMVolume(0.1f);
        SetSFXVolume(0.1f);
        SetWalkVolume(0.3f);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(int index, bool loop = true)
    {
        if (index < 0 || index >= bgmClips.Length) return;
        bgmSource.clip = bgmClips[index];
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfxClips.Length) return;
        sfxSource.PlayOneShot(sfxClips[index]);
    }

    public void PlayWalk(int index)
    {
        if (index < 0 || index >= walkClips.Length) return;
        walkSource.PlayOneShot(walkClips[index]);
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp01(volume); // 0~1로 제한
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }

    public void SetWalkVolume(float volume)
    {
        walkSource.volume = Mathf.Clamp01(volume);
    }

    public float GetBGMVolume() => bgmSource.volume;
    public float GetSFXVolume() => sfxSource.volume;
    public float GetWalkVolume() => walkSource.volume;
}
