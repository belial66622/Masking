using UnityEngine;
using UnityEditor;

public class CharacterPoseHelper : EditorWindow
{
    [MenuItem("Tools/Character Pose Helper/Disable Animator (Safe)")]
    public static void DisableAnimator()
    {
        GameObject selected = Selection.activeGameObject;
        if (selected == null)
        {
            Debug.LogError("No GameObject selected! Please select the character root.");
            return;
        }

        Animator animator = selected.GetComponent<Animator>();
        if (animator != null)
        {
            Undo.RecordObject(animator, "Disable Animator");
            animator.enabled = false;
            Debug.Log($"Animator disabled for {selected.name}. The character is now frozen in its current pose.");
        }
        else
        {
            Debug.LogWarning($"No Animator found on {selected.name}.");
        }
    }

    [MenuItem("Tools/Character Pose Helper/Enable Animator")]
    public static void EnableAnimator()
    {
        GameObject selected = Selection.activeGameObject;
        if (selected == null)
        {
            Debug.LogError("No GameObject selected! Please select the character root.");
            return;
        }

        Animator animator = selected.GetComponent<Animator>();
        if (animator != null)
        {
            Undo.RecordObject(animator, "Enable Animator");
            animator.enabled = true;
            Debug.Log($"Animator enabled for {selected.name}.");
        }
        else
        {
            Debug.LogWarning($"No Animator found on {selected.name}.");
        }
    }
}
