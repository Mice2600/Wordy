using Base.Dialog;
using Base.Word;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public System.Action OnGameWin;
    public System.Action OnGameLost;
    public System.Action<Word> OnWordWin;
    public System.Action<Word> OnWordLost;
    public System.Action<Dialog> OnDialogWin;
    public System.Action<Dialog> OnDialogLost;
    private protected virtual void Start() 
    {
        OnWordWin += (W) =>
        {
            WordBase.Wordgs[WordBase.Wordgs.IndexOf(W)] = new Word(W.EnglishSource, W.RussianSource, W.Score + 2, W.EnglishDiscretion, W.RusianDiscretion);
        };
        OnWordLost += (W) =>
        {
            WordBase.Wordgs[WordBase.Wordgs.IndexOf(W)] = new Word(W.EnglishSource, W.RussianSource, W.Score - 7, W.EnglishDiscretion, W.RusianDiscretion);
        };
        OnDialogWin += (D) =>
        {
            DialogBase.Dialogs[DialogBase.Dialogs.IndexOf(D)] = new Dialog(D.EnglishSource, D.RussianSource, D.Score + 2);
        };
        OnDialogLost += (D) =>
        {
            DialogBase.Dialogs[DialogBase.Dialogs.IndexOf(D)] = new Dialog(D.EnglishSource, D.RussianSource, D.Score - 7);
        };
    }
    
}
