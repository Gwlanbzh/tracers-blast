using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private static float volume = 1f;
    public Slider volumeSlider;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.value = volume;
    }

    void Update() { }
}