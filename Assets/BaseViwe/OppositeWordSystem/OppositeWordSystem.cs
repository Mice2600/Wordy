using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemBox;
using UnityEngine;

public static class OppositeWordSystem
{
    private static TList<OppositeWord> OppositeWords
    {
        get
        {
            if (_OppositeWords == null)
            {
                
                if (Application.isEditor) PlayerPrefs.SetString("OppositeWordsSaver", "");
                if (string.IsNullOrEmpty(PlayerPrefs.GetString("OppositeWordsSaver")))
                {
                    PlayerPrefs.SetString("OppositeWordsSaver", ProjectSettings.ProjectSettings.Mine.DefaultOppositeWords.text);
                }
                _OppositeWords = JsonHelper.FromJsonList<OppositeWord>(PlayerPrefs.GetString("OppositeWordsSaver"));
                if (_OppositeWords == null) _OppositeWords = new List<OppositeWord>();
            }
            return _OppositeWords;
        }
    }
    private static TList<OppositeWord> _OppositeWords;

    public static TList<string> OppositeOf(Content Content) => OppositeOf(Content.EnglishSource);
    public static TList<string> OppositeOf(string Source) {

        OppositeWord dd = OppositeWords.Find(d => d.Source == Source);
        if(dd == null) return new TList<string>();
        return dd.Opposites;

    }


    public static void AddOpposite(string Source, string Opposits) => AddOpposite(Source, new TList<string>(Opposits));
    public static void AddOpposite(string Source, TList<string> Opposits)
    {
        Source = Source.ToUpper();
        for (int i = 0; i < Opposits.Count; i++) Opposits[i] = Opposits[i].ToUpper();
        OppositeWord oppositeWord = OppositeWords.Find(d => d.Source == Source);
        if (oppositeWord == null) { OppositeWords.Add(new OppositeWord() {  Source = Source, Opposites = Opposits }); return; }
        Opposits.ForEach(d => { if (!oppositeWord.Opposites.Contains(d)) oppositeWord.Opposites.Add(d);});
    }
    public static void RemoveOpposite() => throw new System.Exception(" Not yet ");

    public static void ReAnilaizAdd()
    {
        for (int i = 0; i < OppositeWords.Count; i++)
        {
            for (int SS = 0; SS < OppositeWords[i].Opposites.Count; SS++)
            {
                AddOpposite(OppositeWords[i].Opposites[SS], OppositeWords[i].Source);
            }
        }
        //Save();
    }


    public static void Save() 
    {
        PlayerPrefs.SetString("OppositeWordsSaver", JsonHelper.ToJson<OppositeWord>(OppositeWords.ToList()));
        string All = "";/////
        if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default OppositeWords.txt"))
            All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default OppositeWords.txt");
        else Directory.CreateDirectory("Assets/Base/Resources");
        string SD = JsonHelper.ToJson(OppositeWords.ToList());
        SD = SD.Replace("{", "\n{");
        SD = SD.Replace("},", "\n},");
        SD = SD.Replace("\",\"", "\",\n\"");
        System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default OppositeWords.txt", SD);
        ProjectSettings.ProjectSettings.Mine.DefaultOppositeWords = UnityEngine.Resources.Load("Default OppositeWords", typeof(TextAsset)) as TextAsset;
    }




}
[System.Serializable]
public class OppositeWord 
{
    public string Source;
    public List<string> Opposites;
    public override int GetHashCode()=> base.GetHashCode();
    public override bool Equals(object obj)
    {
        if(obj == null) return false;
        if(obj is not OppositeWord) return false;
        return (obj as OppositeWord).Source == Source;
    }
}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        //[HorizontalGroup("DefalultBaseWord")]//
        public TextAsset DefaultOppositeWords;
    }
}