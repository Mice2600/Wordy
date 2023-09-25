using Base.Word;
using UnityEngine;

public class ExportImportData : MonoBehaviour
{
    public void Import() 
    {
        WordBase.Wordgs.Import(GUIUtility.systemCopyBuffer);
        ConsoleLog.Log("Imported");
    }
    public void Expert() 
    {

        WordBase.Wordgs.Export().CopyToClipboard();
        ConsoleLog.Log("Data Copy");
    }
}
public static class ClipboardExtension
{
    /// <summary>
    /// Puts the string into the Clipboard.
    /// </summary>
    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}

