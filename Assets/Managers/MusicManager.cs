using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Button muteButton;

    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volumeSlider.value;

        muteButton.onClick.AddListener(ToggleSound);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void ToggleSound()
    {
        audioSource.mute = !audioSource.mute;
    }
}
