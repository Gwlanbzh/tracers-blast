using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GlowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI tmpText;

    public float glowOuterHover = 1.0f;
    public float glowOuterNormal = 0.0f;
    public float transitionDuration = 0.2f;

    private Material textMaterial;
    // private bool isHovered = false;
    private float transitionTimer = 0f;
    private bool transitioning = false;
    private bool goingToHover = false;

    void Start()
    {
        textMaterial = Instantiate(tmpText.fontMaterial);
        tmpText.fontMaterial = textMaterial;
        // Debug.Log("Initial GlowOuter: " + textMaterial.GetFloat(ShaderUtilities.ID_GlowOuter));
    }

    void Update()
    {
        if (transitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);

            float glowOuter = Mathf.Lerp(goingToHover ? glowOuterNormal : glowOuterHover, goingToHover ? glowOuterHover : glowOuterNormal, t);
            textMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowOuter);

            if (t >= 1f)
                transitioning = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // isHovered = true;
        StartTransition(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // isHovered = false;
        StartTransition(false);
    }

    private void StartTransition(bool toHover)
    {
        goingToHover = toHover;
        transitionTimer = 0f;
        transitioning = true;
    }
}
