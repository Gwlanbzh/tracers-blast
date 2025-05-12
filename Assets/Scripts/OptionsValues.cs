using UnityEngine;
using UnityEngine.UI;

public class OptionsValues : MonoBehaviour
{
    private static float volume = 1.5f;
    public Slider volumeSlider;
    
    private static float fov = 90f;
    public Slider fovSlider;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Debug.Log(gameObject.name);
        
        volumeSlider.value = volume / 3f;
        fovSlider.value = (fov - 40f) / 100f;
    }

    void Update()
    {
        //volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volume = 3f * volumeSlider.value;  // between 0 and 2

        //fov = PlayerPrefs.GetFloat("Fov", 90f);
        fov = 40f + 100f * fovSlider.value;  // between 40 and 140
    }

    public float getVolume()
    {
        return volume;
    }

    public float getFOV()
    {
        return fov;
    }
}
