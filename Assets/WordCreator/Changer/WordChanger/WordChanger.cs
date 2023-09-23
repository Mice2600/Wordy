using Base.Word;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Base;
using Traonsletor;
using BaseViwe.WordViwe;
using SystemBox.Engine;
using UnityEngine.SceneManagement;

namespace ProjectSettings 
{
    public partial class ProjectSettings 
    {
        [Required]
        [BoxGroup("Changers")]
        public GameObject WordChanger;
    }
}
namespace WordCreator.WordCretor
{
    public class WordChanger : ContentObject
    {
        public static void StartChanging(Word NeedChanger = null)
        {
            NeedChanger ??= new Word("", "", 0, false, "", "");
            Instantiate(ProjectSettings.ProjectSettings.Mine.WordChanger).GetComponent<WordChanger>().StartSet(NeedChanger);
        }
        [Required]
        public TMP_InputField DiscreptionWriter;
        public System.Action OnApple;
        

        [Required]
        public Slider ScoreSlider;
        public Content OrginalContent;
        private void StartSet(Word NeedChanger)
        {
            OrginalContent = NeedChanger;
            this.Content = new Word(NeedChanger);
            DiscreptionWriter.text = NeedChanger.EnglishDiscretion;
            ScoreSlider.value = ((NeedChanger as IPersanalData).ScoreConculeated) / 100f;
            ScoreSlider.onValueChanged.AddListener(C => (this.Content as IPersanalData).ScoreConculeated = (C) * 100f);
            DiscreptionWriter.onValueChanged.AddListener((T) => (this.Content as IDiscreption).EnglishDiscretion = T);

        }
        public void TryAplay()
        {
            OrginalContent.EnglishSource = this.Content.EnglishSource;
            OrginalContent.RussianSource = this.Content.RussianSource;
            (OrginalContent as IDiscreption).EnglishDiscretion = (this.Content as IDiscreption).EnglishDiscretion;
            (OrginalContent as IDiscreption).RusianDiscretion = (this.Content as IDiscreption).RusianDiscretion;
            (OrginalContent as IPersanalData).ScoreConculeated = (this.Content as IPersanalData).ScoreConculeated;
            (OrginalContent as IPersanalData).Active = (this.Content as IPersanalData).Active;
            if (!WordBase.Wordgs.Contains(OrginalContent as Word))
            {
                WordBase.Wordgs.Add(OrginalContent as Word);
                WordBase.Sort();
            }
            OnDestroyUrself();
            SceneComands.OpenSceneSellecetWordBase(OrginalContent as Word);
            OnApple?.Invoke();
        }
        public void TryCancel()
        {
            OnDestroyUrself();
        }
        private float TransleteTime = 0;

        private void Update()
        {
            TransleteTime += Time.deltaTime;
            if (TransleteTime > 1)
            {
                TransleteTime = 0;
                StartCoroutine(Translator.Process("en", "ru", DiscreptionWriter.text, onDirectionTransleted));
                void onDirectionTransleted(string tt)
                {
                    Debug.Log(tt);
                    (this.Content as IDiscreption).RusianDiscretion = tt;
                }
            }
        }

        
        public void OnDestroyUrself()
        {
            Destroy(gameObject);
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
            yield return null;
            /*
            if (SceneManager.GetActiveScene().name != "WordBaseViwe") 
            {
                SceneManager.LoadScene("WordBaseViwe", LoadSceneMode.Single);
                yield return null;
                yield return null;
                yield return null;
            }*/
            WordBaseViwe d = GameObject.FindObjectOfType<WordBaseViwe>();
            if(d!= null)d.Lode(WordBase.Wordgs.IndexOf(word)); 
        }
    }
}