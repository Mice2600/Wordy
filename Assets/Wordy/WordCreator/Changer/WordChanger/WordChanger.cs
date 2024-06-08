using Base.Word;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Base;
using Traonsletor;
using SystemBox.Engine;
using UnityEngine.SceneManagement;
using System.Linq;

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
        public static void StartChanging(Word NeedChanger = null, System.Action OnFinsh = null)
        {
            NeedChanger ??= new Word("", "", 0, false, "", "");
            WordChanger dd =Instantiate(ProjectSettings.ProjectSettings.Mine.WordChanger).GetComponent<WordChanger>();
            dd.OnApple += OnFinsh;
            dd.StartSet(NeedChanger);
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

            
            GetComponentsInChildren<IApplyers>().ToList().ForEach(x => x.TryApply(OrginalContent));

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
        public void OnAIDefenitionButton() 
        {
            GPTAI.GenerateDefenition(Content.EnglishSource, OnAiResponded);
            void OnAiResponded(List<string> Words)
            {
                DiscreptionWriter.text = "";
                Words.ForEach(WI => {
                    DiscreptionWriter.text += WI + "\n";
                });
            }
        }
        
        public void OnDestroyUrself()
        {
            Destroy(gameObject);
        }
    }
}
