using Base.Antonym;
using Base.Dialog;
using Base.Synonym;
using Base.Word;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using Study.aSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SystemBox;
using UnityEngine;

public class QuestRandom : MonoBehaviour
{
    
    public static Dictionary<string, bool> Values 
    {
        get 
        {
            if (_Values == null)
            {
                _Values = new Dictionary<string, bool>();
                Dictionary<string, object> All = DictionaryFromType(ProjectSettings.ProjectSettings.Mine);
                List<string> Kays = new List<string>(All.Keys);
                TList<StudyContentData> gameObjects = new List<StudyContentData>();
                for (int i = 0; i < All.Count; i++)
                    if (All[Kays[i]] != null && All[Kays[i]] is StudyContentData && (All[Kays[i]] as StudyContentData) != null)
                        gameObjects.Add(All[Kays[i]] as StudyContentData);
                for (int i = 0; i < gameObjects.Count; i++)
                    _Values.Add(gameObjects[i].SceneName, true);
            }
            return _Values;

        }
        set => _Values = value;

    }
    public static Dictionary<string, bool> _Values;
    void Start()
    {
        Dictionary<string, object> All = DictionaryFromType(ProjectSettings.ProjectSettings.Mine);
        List<string> Kays = new List<string>(All.Keys);
        TList<StudyContentData> gameObjects = new List<StudyContentData>();
        for (int i = 0; i < All.Count; i++)
            if (All[Kays[i]] != null && All[Kays[i]] is StudyContentData && (All[Kays[i]] as StudyContentData) != null)
                gameObjects.Add(All[Kays[i]] as StudyContentData);

        Quest quest = FindObjectOfType<Quest>();
        if (quest == null)
        {
            OnFinsh();
            return;
        }
        quest.OnFineshed += OnFinsh;
        void OnFinsh()
        {
            TList<StudyContentData> HH = (new TList<StudyContentData>(gameObjects)).Mix();

            for (int i = 0; i < HH.Count; i++)
            {
                if (Values[HH[i].SceneName]) 
                {
                    LodeOne(HH[i]).GetComponent<Quest>().OnFineshed += OnFinsh;
                    return;
                }
            }
            LodeOne(gameObjects.RandomItem).GetComponent<Quest>().OnFineshed += OnFinsh;
        }
        GameObject LodeOne(StudyContentData QuestPrefab) 
        {
            if (QuestPrefab is IQuestStarter)
                return(QuestPrefab as IQuestStarter).CreatQuest();
            else if (QuestPrefab is IQuestStarterWithWord)
                return (QuestPrefab as IQuestStarterWithWord).CreatQuest(WordBase.Wordgs.GetContnetList(1)[0]);
            else if (QuestPrefab is IQuestStarterWithDialog)
                return (QuestPrefab as IQuestStarterWithDialog).CreatQuest(DialogBase.Dialogs.GetContnetList(1)[0]);
            else if (QuestPrefab is IQuestStarterWithIrregular)
                return (QuestPrefab as IQuestStarterWithIrregular).CreatQuest(IrregularBase.Irregulars.GetContnetList(1)[0]);
            else if (QuestPrefab is IQuestStarterWithWordList)
                return (QuestPrefab as IQuestStarterWithWordList).CreatQuest(WordBase.Wordgs.GetContnetList((QuestPrefab as IQuestStarterWithWordList).MinimalCount));
            else if (QuestPrefab is IQuestStarterWithDialogList)
                return (QuestPrefab as IQuestStarterWithDialogList).CreatQuest(DialogBase.Dialogs.GetContnetList((QuestPrefab as IQuestStarterWithDialogList).MinimalCount));
            else if (QuestPrefab is IQuestStarterWithIrregularList)
                return (QuestPrefab as IQuestStarterWithIrregularList).CreatQuest(IrregularBase.Irregulars.GetContnetList((QuestPrefab as IQuestStarterWithIrregularList).MinimalCount));
            else if (QuestPrefab is IQuestStarterWithSynonymList)
                return (QuestPrefab as IQuestStarterWithSynonymList).CreatQuest(SynonymBase.Synonyms.GetContnetList((QuestPrefab as IQuestStarterWithSynonymList).MinimalCount));
            else if (QuestPrefab is IQuestStarterWithAntonymList)
                return (QuestPrefab as IQuestStarterWithAntonymList).CreatQuest(AntonymBase.Antonyms.GetContnetList((QuestPrefab as IQuestStarterWithAntonymList).MinimalCount));
            else throw new System.Exception("Countinue the List");
        }
    }
    public static Dictionary<string, object> DictionaryFromType(object atype)
    {
        if (atype == null) return new Dictionary<string, object>();
        Dictionary<string, object> dict = new Dictionary<string, object>();
        foreach (var prop in atype.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            object value = prop.GetValue(atype);
            dict.Add(prop.Name, value);
        }
        return dict;
    }
    public GameObject FilterPrefab;
    public void OnOpenFillter() => Instantiate(FilterPrefab);
}
