using Base;
using Base.Dialog;
using Base.Word;
using BaseViwe.DialogViwe;
using BaseViwe.WordViwe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsComand : MonoBehaviour
{
    public void Import()
    {
        Servises.BaseList.BaseListViwe List = FindObjectOfType<Servises.BaseList.BaseListViwe>();
        if (List is WordBaseViwe)
        {
            WordBase.Wordgs.Import(GUIUtility.systemCopyBuffer);
        }
        if (List is DialogBaseViwe)
        {
            DialogBase.Dialogs.Import(GUIUtility.systemCopyBuffer);
        }



        ConsoleLog.Log("Imported");
    }
    public void Expert()
    {
        Servises.BaseList.BaseListViwe List = FindObjectOfType<Servises.BaseList.BaseListViwe>();
        if (List is WordBaseViwe)
        {
            WordBase.Wordgs.Export().CopyToClipboard();
        }
        if (List is DialogBaseViwe)
        {
            DialogBase.Dialogs.Export().CopyToClipboard();
        }
        ConsoleLog.Log("Data Copy");
    }
    public void ActivePassiveAll(bool Active) 
    {
        Servises.BaseList.BaseListViwe List = FindObjectOfType<Servises.BaseList.BaseListViwe>();
        try
        {   
            if (List is WordBaseViwe) WordBase.Wordgs.ForEach(a => a.Active = Active);
            if (List is DialogBaseViwe) DialogBase.Dialogs.ForEach(a => a.Active = Active);
            List.Refresh();
        }
        catch (System.Exception)
        {

        }
    }
    public void DelletAll()
    {

        Servises.BaseList.BaseListViwe List = FindObjectOfType<Servises.BaseList.BaseListViwe>();
        try
        {
            if (List is WordBaseViwe) WordBase.Wordgs.Clear();
            if (List is DialogBaseViwe) DialogBase.Dialogs.Clear();
            List.Refresh();
        }
        catch (System.Exception)
        {

        }

    }
    public void SpeechSpeedChanger(float speed) 
    {
        EasyTTSUtil.StopSpeech();


        string toSs = $"speech rate is {speed}";

        EasyTTSUtil.DefaultPitch = speed;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            EasyTTSUtil.SpeechAdd(toSs, 1, speed, 1);
        else Debug.Log(toSs + " Speeking");
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
