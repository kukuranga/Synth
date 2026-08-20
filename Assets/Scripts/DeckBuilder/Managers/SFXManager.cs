using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [Header("Setup")]
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private int initialPoolSize = 8;

    private Dictionary<string, AudioClip> clipLookup;
    private List<AudioSource> sourcePool;
    private float fxVolume = 1f;
    private bool isMuted = false;

    private void Awake()
    {
        // Singleton<T> has no Awake of its own and doesn't call
        // DontDestroyOnLoad, so we do it here — otherwise this whole
        // setup (including building the pool below) re-runs from scratch
        // every time a scene loads and something first touches .Instance.
        DontDestroyOnLoad(gameObject);

        clipLookup = audioClips
            .Where(c => c != null)
            .ToDictionary(c => c.name, c => c);

        sourcePool = new List<AudioSource>(initialPoolSize);
        for (int i = 0; i < initialPoolSize; i++)
        {
            sourcePool.Add(CreateSource());
        }
    }

    private AudioSource CreateSource()
    {
        GameObject go = new GameObject("SFX_Source");
        go.transform.SetParent(transform);
        AudioSource src = go.AddComponent<AudioSource>();
        src.playOnAwake = false;
        src.volume = fxVolume;
        src.mute = isMuted;
        return src;
    }

    // Reuses a free (non-playing) source, or grows the pool if all are busy.
    private AudioSource GetAvailableSource()
    {
        foreach (var src in sourcePool)
        {
            if (!src.isPlaying) return src;
        }

        AudioSource newSrc = CreateSource();
        sourcePool.Add(newSrc);
        return newSrc;
    }

    /// <summary>
    /// Plays a clip by name. Multiple sounds — including repeats of the
    /// same clip — can now play simultaneously. Returns the AudioSource
    /// playing it, in case the caller wants to stop/track that instance.
    /// </summary>
    public AudioSource PlaySound(string clipName, float volumeScale = 1f, float pitch = 1f)
    {
        if (!clipLookup.TryGetValue(clipName, out AudioClip clip))
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
            return null;
        }

        AudioSource src = GetAvailableSource();
        src.clip = clip;
        src.volume = fxVolume * Mathf.Clamp01(volumeScale);
        src.pitch = pitch;
        src.Play();
        return src;
    }

    public void SetFXVolume(float volume)
    {
        fxVolume = Mathf.Clamp01(volume);
        foreach (var src in sourcePool) src.volume = fxVolume;
    }

    public float GetFXVolume() => fxVolume;

    // Kept for backward compatibility with any existing call sites.
    public void SetVolume(float volume) => SetFXVolume(volume);

    public void Mute()
    {
        isMuted = true;
        foreach (var src in sourcePool) src.mute = true;
    }

    public void Unmute()
    {
        isMuted = false;
        foreach (var src in sourcePool) src.mute = false;
    }

    public bool IsMuted() => isMuted;

    // These now act on every pooled source. To stop/pause just ONE sound,
    // hold onto the AudioSource returned by PlaySound() and call
    // src.Stop() / src.Pause() on it directly.
    public void StopAll()
    {
        foreach (var src in sourcePool) src.Stop();
    }

    public void PauseAll()
    {
        foreach (var src in sourcePool) src.Pause();
    }

    public void UnpauseAll()
    {
        foreach (var src in sourcePool) src.UnPause();
    }
}