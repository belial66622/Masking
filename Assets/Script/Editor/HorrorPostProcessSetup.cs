using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.IO;

public class HorrorPostProcessSetup : EditorWindow
{
    [MenuItem("Tools/Setup Horror Ambience")]
    public static void SetupHorror()
    {
        // 1. Setup Global Volume
        Volume globalVolume = FindObjectOfType<Volume>();
        if (globalVolume == null)
        {
            GameObject volumeGO = new GameObject("Global Volume");
            globalVolume = volumeGO.AddComponent<Volume>();
            globalVolume.isGlobal = true;
            Debug.Log("Created new Global Volume");
        }

        if (globalVolume.profile == null)
        {
            // Ensure settings folder exists
            string folderPath = "Assets/Settings";
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder("Assets", "Settings");
            }

            // Create new profile
            VolumeProfile profile = ScriptableObject.CreateInstance<VolumeProfile>();
            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Settings/HorrorProfile.asset");
            AssetDatabase.CreateAsset(profile, path);
            AssetDatabase.SaveAssets();
            
            globalVolume.profile = profile;
            Debug.Log($"Created and assigned new Volume Profile at {path}");
        }

        // 2. Configure Profile
        VolumeProfile p = globalVolume.profile;

        // Vignette
        Vignette vignette;
        if (!p.TryGet(out vignette))
        {
            vignette = p.Add<Vignette>(true);
        }
        vignette.active = true;
        vignette.color.overrideState = true;
        vignette.color.value = Color.black;
        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0.55f; // Strong vignette
        vignette.smoothness.overrideState = true;
        vignette.smoothness.value = 0.4f;

        // Color Adjustments (Darkness)
        ColorAdjustments colorAdj;
        if (!p.TryGet(out colorAdj))
        {
            colorAdj = p.Add<ColorAdjustments>(true);
        }
        colorAdj.active = true;
        colorAdj.postExposure.overrideState = true;
        colorAdj.postExposure.value = -1.5f; // Darker world
        colorAdj.contrast.overrideState = true;
        colorAdj.contrast.value = 15f; // Higher contrast for horror

        // Film Grain (Gritty feel)
        FilmGrain grain;
        if (!p.TryGet(out grain))
        {
            grain = p.Add<FilmGrain>(true);
        }
        grain.active = true;
        grain.intensity.overrideState = true;
        grain.intensity.value = 0.5f;
        grain.type.overrideState = true;
        grain.type.value = FilmGrainLookup.Medium2;

        EditorUtility.SetDirty(p);
        AssetDatabase.SaveAssets();

        // 4. Add Controller Script
        HorrorAmbienceController controller = globalVolume.gameObject.GetComponent<HorrorAmbienceController>();
        if (controller == null)
        {
            controller = globalVolume.gameObject.AddComponent<HorrorAmbienceController>();
        }
        controller.globalVolume = globalVolume; // Assign reference
        controller.vignetteIntensity = 0.55f;
        controller.exposure = -1.5f;
        controller.grainIntensity = 0.5f;
        Debug.Log("Attached HorrorAmbienceController script.");

        // 3. Setup Camera
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            UniversalAdditionalCameraData camData = mainCam.GetComponent<UniversalAdditionalCameraData>();
            if (camData == null)
            {
                camData = mainCam.gameObject.AddComponent<UniversalAdditionalCameraData>();
            }
            camData.renderPostProcessing = true;
            EditorUtility.SetDirty(mainCam);
            Debug.Log("Enabled Post Processing on Main Camera");
        }
        else
        {
            Debug.LogWarning("Main Camera not found! Please enable Post Processing on your camera manually.");
        }

        Debug.Log("<b>Horror Setup Complete!</b> Profile configured with Vignette, Darkness, and Film Grain.");
    }
}
