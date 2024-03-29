using Base.Word;
using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Servises;
using Sirenix.OdinInspector;
using SystemBox;
using System.IO;
using System.Linq;
using Base.Dialog;

public class IrregularBase : DataList<Irregular>
{
    static IrregularBase()
    {
        if (PlayerPrefs.GetInt(ID + " defaul") == 0)
            PlayerPrefs.SetString(ID, ProjectSettings.ProjectSettings.Mine.DefalultIrregular.text);
        PlayerPrefs.SetInt(ID + " defaul", 1);
        Irregulars = new IrregularBase();
    }
    public Irregular this[Irregular index]
    {
        get
        {
            int fIndex = IndexOf(new Irregular(index.EnglishSource, "", "", "",  0, false, "", ""));
            if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
            return this[fIndex];
        }
        private set
        {
            int fIndex = IndexOf(new Irregular(index.EnglishSource, "", "", "", 0, false, "", ""));
            if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
            this[fIndex] = value;
        }
    }

    public static IrregularBase Irregulars { get; set; }

    private static string ID => "IrregularBase";

    public override string DataID => ID;
    public static new void Sort()
    {
        Irregulars.Save();
        Irregulars = new IrregularBase();
    }
    public override Irregular tryCreat(string Id) => new Irregular(Id, "", "", "", 0, false, "", "");
    public override Irregular tryCreat(Content Id)
    {
        if (Id is Irregular) return Id as Irregular;
        if (Id is IrregularDefoult) return new Irregular(Id.EnglishSource, Id.RussianSource, (Id as IIrregular).SimplePast, (Id as IIrregular).PastParticiple, 0, false, (Id as IDiscreption).EnglishDiscretion, (Id as IDiscreption).RusianDiscretion);
        else return tryCreat(Id.EnglishSource);
    }

#if UNITY_EDITOR
    /*
    public static string AddManural(string Base) 
    {
        
        Base = Base.Replace(".", "");
        Base = Base.Replace(",", "");
        Base = Base.Replace("?", "");
        Base = Base.Replace("!", "");
        Base = Base.Replace("(", "");
        Base = Base.Replace(")", "");
        Base = Base.Replace(" / ", "/");
        Base = Base.Replace(" REGULAR", "");
        for (int i = 0; i < 1000; i++)
        {
            if (Base.IndexOf(" [") != -1)
            {
                Base = Base.Remove(Base.IndexOf("["), Base.IndexOf("]") - Base.IndexOf("[") + 1);
            }
            else 
            {
                Debug.Log(Base); 

            }
        }
        DO(Base);
        void DO(string Base)
        {
            string[] GS = Base.Split(" ");
            if (GS.Length > 1)
            {
                for (int i = 0; i < GS.Length; i++)
                {
                    DO(GS[i]);
                }
                return;
            }


            string[] ds = Base.Split("\t");
            string BaseSS = ""; string Pas = ""; string NNE = "";


            //return;
            ds.ToList().ForEach(a =>
            {


                if (string.IsNullOrEmpty(BaseSS)) BaseSS = a;
                else if (string.IsNullOrEmpty(Pas)) Pas = a;
                else if (string.IsNullOrEmpty(NNE)) NNE = a;

                if (!string.IsNullOrEmpty(NNE))
                {
                    AddOne(BaseSS, Pas, NNE);
                    BaseSS = ""; Pas = ""; NNE = "";
                }
            });


            void AddOne(string Base, string Pas, string NNE)
            {
                if (WordBase.Wordgs.Contains(new Word(Base, "", 1, false, "", "")))
                {
                    Word Org = WordBase.Wordgs[new Word(Base, "", 1, false, "", "")];
                    Irregulars.Add(new Irregular(Org.EnglishSource, Org.RussianSource, Pas.ToUpper(), NNE.ToUpper(), 0, false, Org.EnglishDiscretion, Org.RusianDiscretion));
                    Debug.Log(" Got One " + Base);
                }
            }
        }



        return JsonHelper.ToJson(Irregulars.ToArray());

    }
    */
    public static void AddAllToDefaultBase()
    {
        ProjectSettings.ProjectSettings.Mine.AddIrregulars(Irregulars);
    }

    
#endif
}

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        //[BoxGroup("Defalult Base/Dialog")]
        public TextAsset DefalultIrregular;
#if UNITY_EDITOR
        public void AddIrregulars(TList<Irregular> Words)
        {
            string All = "";//
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Irregulars.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Irregulars.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<Irregular> AllList = JsonHelper.FromJson<Irregular>(All);
            TList<Irregular> New = new TList<Irregular>();
            AllList.ForEach(AddOne);
            Words.ForEach(AddOne);
            void AddOne(Irregular a)
            {
                if (string.IsNullOrEmpty(a.EnglishSource)) return;
                a.EnglishSource = a.EnglishSource.ToUpper().Replace("!", "").Replace("?", "").Replace(",", "").Replace(".", "");
                New.AddIfDirty(a);
            }
            Debug.Log(New.Count);
            string SD = JsonHelper.ToJson(New.ToArray());
            SD = SD.Replace("{", "\n{");
            SD = SD.Replace("},", "\n},");
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Irregulars.txt", SD);
            DefalultIrregular = UnityEngine.Resources.Load("Default Irregulars", typeof(TextAsset)) as TextAsset;
        }
#endif
    }
}