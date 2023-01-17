using Base;
using Base.Word;
using BaseViwe.WordViwe;
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
            Content = new Word();
        }
        public void OnWordValumeChanged(string Valume)
        {
            Valume = Valume.ToUpper();
            Word Old = (Content as Word?).Value;
            Content = new Word(Valume, Old.RussianSource, Old.Score, Old.Active, Old.EnglishDiscretion, Old.RusianDiscretion);
        }

        public void OnDiscretioValumeChanged(string Valume)
        {
            Word Old = (Content as Word?).Value;
            Content = new Word(Old.EnglishSource, Old.RussianSource, Old.Score, Old.Active, Valume, Old.RusianDiscretion);
        }
        public void OnScoreValumeChanged(float Valume)
        {
            Valume *= 100f;
            Word Old = (Content as Word?).Value;
            Content = new Word(Old.EnglishSource, Old.RussianSource, Valume, Old.Active,Old.EnglishDiscretion, Old.RusianDiscretion );
        }

        public void OnActiveValumeChanged(bool Valume) 
        {
            Word Old = (Content as Word?).Value;
            Content = new Word(Old.EnglishSource, Old.RussianSource, Old.Score, Valume, Old.EnglishDiscretion, Old.RusianDiscretion);
        }

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
                    Word Old = (Content as Word?).Value;
                    Debug.Log(tt);
                    Content = new Word(Old.EnglishSource, tt, Old.Score, Old.Active, Old.EnglishDiscretion, Old.RusianDiscretion);
                }
                void onDirectionTransleted(string tt)
                {
                    Word Old = (Content as Word?).Value;
                    Content = new Word(Old.EnglishSource, Old.RussianSource, Old.Score, Old.Active, Old.EnglishDiscretion, tt);
                }
            }
        }

        public void TryAdd()
        {

            WordBase.Wordgs.Add((Content as Word?).Value);
            WordBase.Sort();

            SceneComands.OpenSceneSellecetWordBase((Content as Word?).Value);
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