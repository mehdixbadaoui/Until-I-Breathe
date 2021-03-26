 using UnityEditor;
 using UnityEngine;
 
public static class GroupCommand
{
    [MenuItem("GameObject/Group Selected %g")]
    private static void GroupSelected()
    {
        if (!Selection.activeTransform) return;
        var go = new GameObject(Selection.activeTransform.name + " Group");
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);
        foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
        Selection.activeGameObject = go;
    }

    [MenuItem("GameObject/Ungroup Selected #%g")]
    private static void UngroupSelected()
    {
        if (!Selection.activeTransform) 
            return;

        int childCount = Selection.transforms[0].childCount;

        // Only first element
        for (int j = 0; j < childCount; j++)
        {
            Transform child = Selection.transforms[0].GetChild(0);

            child.SetParent(null);
            child.SetSiblingIndex(Selection.transforms[0].GetSiblingIndex());
        }

        GameObject.DestroyImmediate(Selection.transforms[0].gameObject);
    }
}



