using Base;
using Base.Word;
using Newtonsoft.Json.Linq;
using System.Collections;
using SystemBox.Engine;
using Traonsletor;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace WordCreator.WordCretor
{
    public class WordCretorViwe : ContentObject
    {
        private void Awake()
        {
            Content = new Word("","",0,false,"","");
        }
        public void OnWordValumeChanged(string Valume) => Content.EnglishSource= Valume;
        public void OnDiscretioValumeChanged(string Valume)=> (Content as IDiscreption).EnglishDiscretion = Valume;
        public void OnScoreValumeChanged(float Valume) => (Content as IPersanalData).ScoreConculeated = Valume;
        public void OnActiveValumeChanged(bool Valume) => (Content as IPersanalData).Active = Valume;

        private float TransleteTime = 0;

        private void Update()
        {
            TransleteTime += Time.deltaTime;
            if (TransleteTime > 1)
            {
                TransleteTime = 0;
                StartCoroutine(Translator.Process("en", "ru", Content.EnglishSource, onWordTransleted));
                StartCoroutine(Translator.Process("en", "ru", (Content as IDiscreption).EnglishDiscretion, onDirectionTransleted));
                void onWordTransleted(string tt)
                {
                    Debug.Log(tt); 
                    Content.RussianSource = tt;
                }
                void onDirectionTransleted(string tt)
                {
                    Debug.Log(tt);
                    (Content as IDiscreption).RusianDiscretion = tt;
                }
            }
        }

        public void TryAdd()
        {
            WordBase.Wordgs.Add(Content as Word);
            WordBase.Sort();
        }


    }
}