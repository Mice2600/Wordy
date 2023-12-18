using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class StaticInspectorOpener : MonoBehaviour
{
    [MenuItem("Assets/Create/Open Static Window", priority = 1)]
    private static void CreateScriptableObject()
    {

        Type NT = (Selection.activeObject as UnityEditor.MonoScript).GetClass();
        Sirenix.OdinInspector.Editor.StaticInspectorWindow.InspectType(NT);
    }
    [MenuItem("Assets/Create/Open Static Window", true)]
    private static bool CreateMyScriptableObjectTester()
    {
        if (Selection.activeObject == null) return false;
        if (Selection.activeObject.GetType() != typeof(UnityEditor.MonoScript)) return false;
        return true;
    }

}
