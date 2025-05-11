using UnityEngine;
using UnityEngine.UI;

public class OptionsValues : MonoBehaviour
{
    private static float volume = 1f;
    public Slider volumeSlider;


    private static float fov = 90f;
    public Slider fovSlider;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.value = volume;

        fov = PlayerPrefs.GetFloat("Fov", 90f);
        fovSlider.value = fov;
    }

    void Update() { }
}