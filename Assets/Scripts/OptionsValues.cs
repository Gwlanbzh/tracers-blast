using UnityEngine;
using UnityEngine.UI;

public class OptionsValues : MonoBehaviour
{
    private static float volume = 1.5f;
    public Slider volumeSlider;
    
    private static float fov = 90f;
    public Slider fovSlider;
    
    private static float mouse_sensitivity = 1f;
    public Slider mouseSensSlider;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        volumeSlider.value = volume / 3f;
        fovSlider.value = (fov - 40f) / 100f;
        mouseSensSlider.value = (mouse_sensitivity - .25f) / 2.75f;
    }

    void Update()
    {
        volume = 3f * volumeSlider.value;  // between 0 and 2

        fov = 40f + 100f * fovSlider.value;  // between 40 and 140

        mouse_sensitivity = .25f + 2.75f * mouseSensSlider.value;  // between .25 and 3
    }

    public float getVolume()
    {
        return volume;
    }

    public float getFOV()
    {
        return fov;
    }

    public float getMouseSensitivity()
    {
        return mouse_sensitivity;
    }
}
