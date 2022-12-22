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
    }

}

namespace Study.aSystem
{
    public abstract class Quest : MonoBehaviour
    {
        public abstract int AddScoreDialog { get; }
        public abstract int RemoveScoreDialog { get; }
        public abstract int AddScoreWord { get; }
        public abstract int RemoveScoreWord { get; }

        public System.Action OnGameWin;
        public System.Action OnGameLost;
        public System.Action OnFineshed;
        public System.Action<Word> OnWordWin;
        public System.Action<Word> OnWordLost;
        public System.Action<Dialog> OnDialogWin;
        public System.Action<Dialog> OnDialogLost;
        private protected virtual void Start()
        {
            OnWordWin += (W) =>
            {
                WordBase.Wordgs[W] = new Word(W.EnglishSource, W.RussianSource, W.Score + Mathf.Abs(AddScoreWord), W.EnglishDiscretion, W.RusianDiscretion);

            };
            OnWordLost += (W) =>
            {
                WordBase.Wordgs[W] = new Word(W.EnglishSource, W.RussianSource, W.Score - Mathf.Abs(RemoveScoreWord), W.EnglishDiscretion, W.RusianDiscretion);
            };
            OnDialogWin += (D) =>
            {
                DialogBase.Dialogs[D] = new Dialog(D.EnglishSource, D.RussianSource, D.Score + Mathf.Abs(AddScoreDialog));
                WordBase.Wordgs.FindContentsFromString(D.EnglishSource, Nfound => OnWordWin.Invoke(Nfound));
            };
            OnDialogLost += (D) =>
            {
                DialogBase.Dialogs[D] = new Dialog(D.EnglishSource, D.RussianSource, D.Score - Mathf.Abs(RemoveScoreDialog));
                WordBase.Wordgs.FindContentsFromString(D.EnglishSource, Nfound => OnWordLost.Invoke(Nfound));
            };
            OnFineshed += () => Destroy(gameObject);
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
    public interface IQuestStarterWithWordList
    {
        public void CreatQuest(List<Word> words);
    }
    public interface IQuestStarterWithDialogList
    {
        public void CreatQuest(List<Dialog> dialogs);
    }
}