using Base;
using Base.Dialog;
using Base.Word;
using BaseViwe.DialogViwe;
using System.Collections;
using SystemBox.Engine;
using Traonsletor;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace WordCreator.DialogCreator
{
    public class DialogCreator : ContentObject
    {
        private void Awake()
        {
            Content = new Dialog();
        }
        public void OnWordValumeChanged(string Valume)
        {
            Dialog Old = (Content as Dialog?).Value;
            Content = new Dialog(Valume, Old.RussianSource, Old.Score, Old.Active);
        }
        public void OnScoreValumeChanged(float Valume)
        {
            Valume *= 100f;
            Dialog Old = (Content as Dialog?).Value;
            Content = new Dialog(Old.EnglishSource, Old.RussianSource, Valume, Old.Active);
        }

        public void OnActiveValumeChanged(bool Valume)
        {
            Dialog Old = (Content as Dialog?).Value;
            Content = new Dialog(Old.EnglishSource, Old.RussianSource, Old.Score, Valume);
        }

        private float TransleteTime = 0;

        private void Update()
        {
            TransleteTime += Time.deltaTime;
            if (TransleteTime > 1)
            {
                TransleteTime = 0;
                StartCoroutine(Translator.Process("en", "ru", Content.EnglishSource, onWordTransleted));
                void onWordTransleted(string tt)
                {
                    Dialog Old = (Content as Dialog?).Value;
                    Debug.Log(tt);
                    Content = new Dialog(Old.EnglishSource, tt, Old.Score, Old.Active);
                }
            }
        }

        public void TryAdd()
        {

            DialogBase.Dialogs.Add((Content as Dialog?).Value);
            DialogBase.Sort();
            SceneComands.OpenSceneSellecetDialogBase((Content as Dialog?).Value);
        }
    }
}
public static partial class SceneComands // WordBaseViwe 
{
    public static void OpenSceneSellecetDialogBase(Dialog dialog)
    {
        Engine.Get_Engine("Game").StartCoroutine(enumerator());
        IEnumerator enumerator()
        {

            SceneManager.LoadScene("DialogBaseViwe", LoadSceneMode.Single);
            yield return null;
            yield return null;
            yield return null;
            GameObject.FindObjectOfType<DialogBaseViwe>().Lode(DialogBase.Dialogs.IndexOf(dialog));
        }
    }
}
