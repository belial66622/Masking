using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HorrorAmbienceController : MonoBehaviour
{
    [Header("Settings")]
    public Volume globalVolume;

    [Header("Controls")]
    [Range(0f, 1f)] public float vignetteIntensity = 0.55f;
    [Range(-5f, 2f)] public float exposure = -1.5f; // Lower is darker
    [Range(0f, 1f)] public float grainIntensity = 0.5f;

    // Cache
    private Vignette _vignette;
    private ColorAdjustments _colorAdjustments;
    private FilmGrain _filmGrain;

    void Start()
    {
        if (globalVolume == null)
            globalVolume = GetComponent<Volume>();

        if (globalVolume != null && globalVolume.profile != null)
        {
            globalVolume.profile.TryGet(out _vignette);
            globalVolume.profile.TryGet(out _colorAdjustments);
            globalVolume.profile.TryGet(out _filmGrain);
        }
    }

    void Update()
    {
        if (globalVolume == null) return;

        // Apply Vignette
        if (_vignette != null)
        {
            _vignette.active = vignetteIntensity > 0;
            _vignette.intensity.value = vignetteIntensity;
        }

        // Apply Exposure (Darkness)
        if (_colorAdjustments != null)
        {
            _colorAdjustments.active = true;
            _colorAdjustments.postExposure.value = exposure;
        }

        // Apply Grain
        if (_filmGrain != null)
        {
            _filmGrain.active = grainIntensity > 0;
            _filmGrain.intensity.value = grainIntensity;
        }
    }
}
