using Base.Antonym;
using Base.Dialog;
using Base.Synonym;
using Base.Word;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemBox;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace ProjectSettings
{
    
}

namespace Study.aSystem
{
    public class Quest : MonoBehaviour
    {
        public StudyContentData QuestData 
        {
            get 
            {
                if (_QuestPrefab == null)
                {
                    Dictionary<string, object> All = DictionaryFromType(ProjectSettings.ProjectSettings.Mine);
                    List<string> Kays = new List<string>(All.Keys);
                    TList<StudyContentData> gameObjects = new List<StudyContentData>();
                    for (int i = 0; i < All.Count; i++)
                        if (All[Kays[i]] != null && All[Kays[i]] is StudyContentData && (All[Kays[i]] as StudyContentData) != null)
                                gameObjects.Add(All[Kays[i]] as StudyContentData);
                    _QuestPrefab = gameObjects.Find(x => x.Prefab.name == gameObject.name);
                    Dictionary<string, object> DictionaryFromType(object atype)
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
                }
                return _QuestPrefab;
            } 
        }
        private StudyContentData _QuestPrefab;
        public System.Action OnGameWin;
        public System.Action OnGameLost;
        public System.Action OnFineshed;

        public System.Action<Word> OnWordWin;
        public System.Action<Word> OnWordLost;
        public System.Action<Dialog> OnDialogWin;
        public System.Action<Dialog> OnDialogLost;
        
        public System.Action<Irregular> OnIrregularWin;
        public System.Action<Irregular> OnIrregularLost;

        [System.NonSerialized]
        public Word NeedWord;
        [System.NonSerialized]
        public TList<Word> NeedWordList;
        [System.NonSerialized]
        public Dialog NeedDialog;
        [System.NonSerialized]
        public TList<Dialog> NeedDialogList;
        [System.NonSerialized]
        public Irregular NeedIrregular;
        [System.NonSerialized]
        public TList<Irregular> NeedIrregularList;
        [System.NonSerialized]
        public TList<Synonym> NeedSynonymList;
        [System.NonSerialized]
        public TList<Antonym> NeedAntonymList;
        [System.NonSerialized]
        public TList<Content> NeedContentList;


        private protected virtual void Start()
        {
            if (QuestData is IWordScorer)
            {
                OnWordWin += (W) =>
                {
                    (WordBase.Wordgs[W as Word] as IPersanalData).ScoreConculeated += Mathf.Abs((QuestData as IWordScorer).AddScoreWord);

                };
                OnWordLost += (W) =>
                {
                    (WordBase.Wordgs[W as Word] as IPersanalData).ScoreConculeated -= Mathf.Abs((QuestData as IWordScorer).RemoveScoreWord);
                };
            }
            if (QuestData is IDialogScorer)
            {
                OnDialogWin += (D) =>
                {
                    (DialogBase.Dialogs[D] as IPersanalData).ScoreConculeated += Mathf.Abs((QuestData as IDialogScorer).AddScoreDialog);
                    if (QuestData is IWordScorer) WordBase.Wordgs.FindContentsFromString(D.EnglishSource, Nfound => OnWordWin?.Invoke(Nfound as Word));
                };
                OnDialogLost += (D) =>
                {
                    (DialogBase.Dialogs[D] as IPersanalData).ScoreConculeated -= Mathf.Abs((QuestData as IDialogScorer).RemoveScoreDialog);
                    if (QuestData is IWordScorer) WordBase.Wordgs.FindContentsFromString(D.EnglishSource, Nfound => OnWordLost?.Invoke(Nfound as Word));
                };
            }
            if (QuestData is IIrregularScorer)
            {
                OnIrregularWin += (W) =>
                {
                    (IrregularBase.Irregulars[W] as IPersanalData).ScoreConculeated += Mathf.Abs((QuestData as IIrregularScorer).AddScorIrregular);
                };
                OnIrregularLost += (W) =>
                {
                    (IrregularBase.Irregulars[W] as IPersanalData).ScoreConculeated -= Mathf.Abs((QuestData as IIrregularScorer).RemoveScoreIrregular);
                };
            }
            OnFineshed += () => Destroy(gameObject);
            //OnFineshed += () => Instantiate(QuestPrefab);
        }

    }


    [Serializable]
    public class StudyContentData
    {
        [Required]
        public GameObject Prefab;
        [Required]
        public string SceneName;
    }

    public interface IDialogScorer
    {
        public int AddScoreDialog { get; }
        public int RemoveScoreDialog { get; }
    }
    public interface IWordScorer
    {
        public int AddScoreWord { get; }
        public int RemoveScoreWord { get; }
    }
    public interface IIrregularScorer
    {
        public int AddScorIrregular { get; }
        public int RemoveScoreIrregular { get; }
    }
    public interface IQuestStarter 
    {
        public GameObject CreatQuest() 
        {
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            return D;
        }
    }
    public interface IQuestStarterWithWord
    {
        public GameObject CreatQuest(Word word) 
        {
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            D.GetComponentInChildren<Quest>().NeedWord = word;
            return D;
        }
    }
    public interface IQuestStarterWithDialog
    {
        public GameObject CreatQuest(Dialog dialog)
        {
            Debug.Log(dialog.EnglishSource);
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            D.GetComponentInChildren<Quest>().NeedDialog = dialog;
            return D;
        }
    }
    public interface IQuestStarterWithIrregular
    {
        public GameObject CreatQuest(Irregular Irregular) 
        {
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            D.GetComponentInChildren<Quest>().NeedIrregular = Irregular;
            return D;
        }
    }
    public interface IQuestStarterWithWordList
    {
        public int MinimalCount { get; }
        public GameObject CreatQuest(List<Word> words)
        {
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            D.GetComponentInChildren<Quest>().NeedWordList = words;

            List<Content> contents = new List<Content>();
            words.ForEach(a => contents.Add(a as Content));
            D.GetComponentInChildren<Quest>().NeedContentList = contents;

            return D;
        }
    }
    public interface IQuestStarterWithDialogList
    {
        public int MinimalCount { get; }
        public GameObject CreatQuest(List<Dialog> dialogs)
        {
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            D.GetComponentInChildren<Quest>().NeedDialogList = dialogs;

            List<Content> contents = new List<Content>();
            dialogs.ForEach(a => contents.Add(a as Content));
            D.GetComponentInChildren<Quest>().NeedContentList = contents;

            return D;
        }
    }
    public interface IQuestStarterWithIrregularList
    {
        public int MinimalCount { get; }
        public GameObject CreatQuest(List<Irregular> Irregulars)
        {
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            D.GetComponentInChildren<Quest>().NeedIrregularList = Irregulars;

            List<Content> contents = new List<Content>();
            Irregulars.ForEach(a => contents.Add(a as Content));
            D.GetComponentInChildren<Quest>().NeedContentList = contents;

            return D;
        }
    }
    public interface IQuestStarterWithSynonymList 
    {
        public int MinimalCount { get; }
        public GameObject CreatQuest(List<Synonym> Synonyms)
        {
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            D.GetComponentInChildren<Quest>().NeedSynonymList = Synonyms;


            List<Content> contents = new List<Content>();
            Synonyms.ForEach(a => contents.Add(a as Content));
            D.GetComponentInChildren<Quest>().NeedContentList = contents;


            return D;
        }
    }

    public interface IQuestStarterWithAntonymList
    {
        public int MinimalCount { get; }
        public GameObject CreatQuest(List<Antonym> Antonyms)
        {
            GameObject D = GameObject.Instantiate((this as StudyContentData).Prefab);
            D.name = (this as StudyContentData).Prefab.name;
            D.GetComponentInChildren<Quest>().NeedAntonymList = Antonyms;

            List<Content> contents = new List<Content>();
            Antonyms.ForEach(a => contents.Add(a as Content));
            D.GetComponentInChildren<Quest>().NeedContentList = contents;

            return D;
        }
    }

}