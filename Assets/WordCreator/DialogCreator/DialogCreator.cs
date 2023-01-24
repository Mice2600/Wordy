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
            Content = new Dialog("", "",0,false);
        }
        public void OnWordValumeChanged(string Valume) => Content.EnglishSource= Valume;
        public void OnScoreValumeChanged(float Valume) => Content.Score = Valume *= 100f;

        public void OnActiveValumeChanged(bool Valume) => Content.Active = Valume;

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
                    Debug.Log(tt);
                    Content.RussianSource = tt;
                }
            }
        }

        public void TryAdd()
        {
            DialogBase.Dialogs.Add((Content as Dialog));
            DialogBase.Sort();
            SceneComands.OpenSceneSellecetDialogBase((Content as Dialog));
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
