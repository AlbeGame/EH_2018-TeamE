using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(Interactable))]
[CanEditMultipleObjects]
public class InteractableEditor : ButtonEditor
{
    public override void OnInspectorGUI()
    {
        Interactable targetInteractable = (Interactable)target;

        EditorGUILayout.BeginHorizontal();
        targetInteractable.SelectedMat = EditorGUILayout.ObjectField("Selected Material", targetInteractable.SelectedMat, typeof(Material), true) as Material;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        targetInteractable.UnselectedMat = EditorGUILayout.ObjectField("Unselected Material", targetInteractable.UnselectedMat, typeof(Material), true) as Material;
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
