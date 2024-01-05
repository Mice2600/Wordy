using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
namespace Base.Word
{
    public interface IMultiTranslation
    {
        public int TranslationCount => (this as Content).RussianSource.Split(',').Length;
        public TList<string> Translations
        {
            get
            {
                string Translation = (this as Content).RussianSource;
                TList<string> TranslationsList = new TList<string>();
                Translation.Split(',').ToList().ForEach(t => TranslationsList.Add(t));
                return TranslationsList;
            }
        }
        public void AddTranslation(string Translation)
        {
            (this as Content).RussianSource += "," + Translation;
        }
        public bool RemoveTranslationAt(int index)
        {
            string Translation = (this as Content).RussianSource;
            List<string> TranslationList = Translation.Split(',').ToList();
            if (index  >= TranslationList.Count) return false;

            TranslationList.RemoveAt(index); ;
            Translation = "";
            for (int i = 0; i < TranslationList.Count; i++)
            {
                Translation += TranslationList[i];
                if (i + 1 != TranslationList.Count) Translation += ",";
            }
            (this as Content).RussianSource = Translation;
            return true;
        }
        public bool RemoveTranslation(string RemovingTranslation)
        {
            string Translation = (this as Content).RussianSource;
            List<string> TranslationList = Translation.Split(',').ToList();
            if (!TranslationList.Contains(RemovingTranslation)) return false;

            TranslationList.RemoveAt(TranslationList.IndexOf(RemovingTranslation)); ;
            Translation = "";
            for (int i = 0; i < TranslationList.Count; i++)
            {
                Translation += TranslationList[i];
                if (i + 1 != TranslationList.Count) Translation += ",";
            }
            (this as Content).RussianSource = Translation;
            return true;
        }
        public bool ChangeTranslationAt(int index, string NewTranslation)
        {
            string Translation = (this as Content).RussianSource;
            List<string> TranslationList = Translation.Split(',').ToList();
            if (index >= TranslationList.Count) return false;
            TranslationList[index] = NewTranslation;
            Translation = "";
            for (int i = 0; i < TranslationList.Count; i++)
            {   
                Translation += TranslationList[i];
                if (i + 1 != TranslationList.Count) Translation += ",";
            }
            (this as Content).RussianSource = Translation;
            return true;
        }
        public bool RaplaceTranslation(string OldTranslation, string NewTranslation)
        {
            string Translation = (this as Content).RussianSource;
            List<string> TranslationList = Translation.Split(',').ToList();
            if (!TranslationList.Contains(OldTranslation)) return false;

            TranslationList[TranslationList.IndexOf(OldTranslation)] = NewTranslation;
            Translation = "";
            for (int i = 0; i < TranslationList.Count; i++)
            {   
                Translation += TranslationList[i];
                if (i + 1 != TranslationList.Count) Translation += ",";
            }
            (this as Content).RussianSource = Translation;


            return true;
        }
    }
}