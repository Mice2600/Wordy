using Base;
using Base.Word;
using BaseViwe.WordViwe;
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
        public void OnScoreValumeChanged(float Valume) => Content.Score= Valume;
        public void OnActiveValumeChanged(bool Valume) => Content.Active = Valume;

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
            SceneComands.OpenSceneSellecetWordBase(Content as Word);
        }


    }
}
public static partial class SceneComands // WordBaseViwe 
{
    public static void OpenSceneSellecetWordBase(Word word)
    {
        Engine.Get_Engine("Game").StartCoroutine(enumerator());
        IEnumerator enumerator()
        {

            SceneManager.LoadScene("WordBaseViwe", LoadSceneMode.Single);
            yield return null;
            yield return null;
            yield return null;
            GameObject.FindObjectOfType<WordBaseViwe>().Lode(WordBase.Wordgs.IndexOf(word));
        }
    }
}