using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dragSound;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("null");
        }
    }

    public void PlayDragSound()
    {
        PlaySound(dragSound);
    }

    public void PlayButtonSound()
    {
        PlaySound(buttonSound);
    }
    public void PlayGameOverSound()
    {
        PlaySound(gameOverSound);
    }

}