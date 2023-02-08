using Base.Word;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Base;
using Traonsletor;

namespace ProjectSettings 
{
    public partial class ProjectSettings 
    {
        [Required]
        [BoxGroup("Changers")]
        public GameObject WordChanger;
    }
}

public class WordChanger : ContentObject
{
    public static void StartChanging(Word NeedChanger) 
    {
        Instantiate(ProjectSettings.ProjectSettings.Mine.WordChanger).GetComponent<WordChanger>().StartSet(NeedChanger); 
    }
    [Required]
    public TMP_InputField WordWriter;
    [Required]
    public TextMeshProUGUI WordTronsleated;
    [Required]
    public TMP_InputField DiscreptionWriter;
    [Required]
    public TextMeshProUGUI DiscreptionTranslated;
    [Required]
    public Slider ScoreSlider;
    private void StartSet(Word NeedChanger)
    {
        this.Content = NeedChanger;
        WordWriter.text = NeedChanger.EnglishSource;
        WordTronsleated.text = NeedChanger.RussianSource;
        DiscreptionWriter.text = NeedChanger.EnglishDiscretion;
        DiscreptionTranslated.text = NeedChanger.RusianDiscretion;
        ScoreSlider.value = (NeedChanger.Score) / 100f;
    }
    public void TryAplay() 
    {
        this.Content.EnglishSource = WordWriter.text;
        this.Content.RussianSource  = WordTronsleated.text;
        (this.Content as IDiscreption).EnglishDiscretion = DiscreptionWriter.text;
        (this.Content as IDiscreption).RusianDiscretion = DiscreptionTranslated.text;
        this.Content.Score = (ScoreSlider.value) * 100f;
        OnDestroyUrself();
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
            StartCoroutine(Translator.Process("en", "ru", WordWriter.text, onWordTransleted));
            StartCoroutine(Translator.Process("en", "ru", DiscreptionWriter.text, onDirectionTransleted));
            void onWordTransleted(string tt)
            {
                Debug.Log(tt);
                WordTronsleated.text = tt;
            }
            void onDirectionTransleted(string tt)
            {
                Debug.Log(tt);
                DiscreptionTranslated.text = tt;
            }
        }
    }

    public void OnDestroyUrself()
    {
        Destroy(gameObject);
    }
}
