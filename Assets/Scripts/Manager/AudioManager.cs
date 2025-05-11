using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip collectSound;
    public AudioClip gameOverSound;
    public AudioClip openInventorySound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayCollect() => PlaySFX(collectSound);
    public void PlayGameOver() => PlaySFX(gameOverSound);
    public void PlayInventoryOpen() => PlaySFX(openInventorySound);
}
