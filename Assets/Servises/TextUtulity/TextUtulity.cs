using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextUtulity 
{
    public static string Color(string Text, Color color) => $"<#{ColorUtility.ToHtmlStringRGBA(color)}>{Text}</color>";
    public static string UnderLine(string Text) => $"<u>{Text}</u>";
    public static string SenterLine(string Text) => $"<s>{Text}</s>";
    public static string Size(string Text, float Size) => $"<size={Size}>{Text}</size>";
}
