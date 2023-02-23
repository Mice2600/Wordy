using Base.Dialog;
using Base.Word;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

namespace ProjectSettings
{
    [Serializable]
    public struct StudyScoreValumes
    {
        public int AddScoreDialog;
        public int RemoveScoreDialog;
        public int AddScoreWord;
        public int RemoveScoreWord;
        public int AddScoreIrregular;
        public int RemoveScoreIrregular;
    }

}

namespace Study.aSystem
{
    public abstract class Quest : MonoBehaviour
    {
        public abstract GameObject QuestPrefab { get; }
        public abstract int AddScoreDialog { get; }
        public abstract int RemoveScoreDialog { get; }
        public abstract int AddScoreWord { get; }
        public abstract int RemoveScoreWord { get; }
        public abstract int AddScoreIrregular { get; }
        public abstract int RemoveScoreIrregular { get; }

        public System.Action OnGameWin;
        public System.Action OnGameLost;
        public System.Action OnFineshed;

        public System.Action<Word> OnWordWin;
        public System.Action<Word> OnWordLost;
        public System.Action<Dialog> OnDialogWin;
        public System.Action<Dialog> OnDialogLost;
        
        public System.Action<Irregular> OnIrregularWin;
        public System.Action<Irregular> OnIrregularLost;

        private protected virtual void Start()
        {
            OnWordWin += (W) =>
            {
                WordBase.Wordgs[W as Word].ScoreConculeated += Mathf.Abs(AddScoreWord);

            };
            OnWordLost += (W) =>
            {
                WordBase.Wordgs[W as Word].ScoreConculeated -= Mathf.Abs(RemoveScoreWord);
            };
            OnDialogWin += (D) =>
            {
                DialogBase.Dialogs[D].ScoreConculeated += Mathf.Abs(AddScoreDialog);
                WordBase.Wordgs.FindContentsFromString(D.EnglishSource, Nfound => OnWordWin.Invoke(Nfound));
            };
            OnDialogLost += (D) =>
            {
                DialogBase.Dialogs[D].ScoreConculeated -= Mathf.Abs(RemoveScoreDialog);
                WordBase.Wordgs.FindContentsFromString(D.EnglishSource, Nfound => OnWordLost.Invoke(Nfound));
            };
            OnIrregularWin += (W) =>
            {
                IrregularBase.Irregulars[W].ScoreConculeated += Mathf.Abs(AddScoreIrregular);
            };
            OnIrregularLost += (W) =>
            {
                IrregularBase.Irregulars[W].ScoreConculeated -= Mathf.Abs(RemoveScoreIrregular);
            };

            OnFineshed += () => Destroy(gameObject);
            //OnFineshed += () => Instantiate(QuestPrefab);
        }

        
    }
    public interface IQuestStarter 
    {
        public void CreatQuest();
    }
    public interface IQuestStarterWithWord
    {
        public void CreatQuest(Word word);
    }
    public interface IQuestStarterWithDialog
    {
        public void CreatQuest(Dialog dialog);
    }
    public interface IQuestStarterWithIrregular
    {
        public void CreatQuest(Irregular Irregular);
    }
    public interface IQuestStarterWithWordList
    {
        public int MinimalCount { get; }
        public void CreatQuest(List<Word> words);
    }
    public interface IQuestStarterWithDialogList
    {
        public int MinimalCount { get; }
        public void CreatQuest(List<Dialog> dialogs);
    }
    public interface IQuestStarterWithIrregularList
    {
        public int MinimalCount { get; }
        public void CreatQuest(List<Irregular> Irregulars);
    }
}