using Base;
using Base.Dialog;
using Traonsletor;
using UnityEngine;
namespace WordCreator.DialogCreator
{
    public class DialogCreator : ContentObject
    {
        private void Awake()
        {
            Content = new Dialog("", "",0,false);
        }
        public void OnWordValumeChanged(string Valume) => Content.EnglishSource= Valume;
        public void OnScoreValumeChanged(float Valume) => (Content as IPersanalData).ScoreConculeated = Valume *= 100f;

        public void OnActiveValumeChanged(bool Valume) => (Content as IPersanalData).Active = Valume;

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
        }
    }
}
