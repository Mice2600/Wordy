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

public class WordChanger : ContentObject
{
    public static void StartChanging(Word NeedChanger = null) 
    {
        NeedChanger ??= new Word("", "", 0, false, "", "");
        Instantiate(ProjectSettings.ProjectSettings.Mine.WordChanger).GetComponent<WordChanger>().StartSet(NeedChanger); 
    }
    [Required]
    public TMP_InputField WordWriter;
    [Required]
    public TMP_InputField WordTronsleated;
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
        ScoreSlider.value = ((NeedChanger as IPersanalData).ScoreConculeated) / 100f;
    }
    public void TryAplay() 
    {
        this.Content.EnglishSource = WordWriter.text;
        this.Content.RussianSource  = WordTronsleated.text;
        (this.Content as IDiscreption).EnglishDiscretion = DiscreptionWriter.text;
        (this.Content as IDiscreption).RusianDiscretion = DiscreptionTranslated.text;
        (this.Content as IPersanalData).ScoreConculeated = (ScoreSlider.value) * 100f;
        if (!WordBase.Wordgs.Contains(Content as Word)) 
        {
            WordBase.Wordgs.Add(Content as Word);
            WordBase.Sort();
        }
        OnDestroyUrself();
        SceneComands.OpenSceneSellecetWordBase(Content as Word);
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
                DiscreptionTranslated.text = tt;
            }
        }
    }

    public void OnTranllateButton(string TronslateType) 
    {
        if (TronslateType != "ru" && TronslateType != "uz") TronslateType = "ru";
        StartCoroutine(Translator.Process("en", TronslateType, WordWriter.text, onWordTransleted));
        void onWordTransleted(string tt)
        {
            Debug.Log(tt);
            WordTronsleated.text = tt;
        }
    }
    public void OnDestroyUrself()
    {
        Destroy(gameObject);
    }
}
public static partial class SceneComands // WordBaseViwe 
{
    public static void OpenSceneSellecetWordBase(Word word)
    {
        Engine.Get_Engine("Game").StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            if (SceneManager.GetActiveScene().name != "WordBaseViwe") 
            {
                SceneManager.LoadScene("WordBaseViwe", LoadSceneMode.Single);
                yield return null;
                yield return null;
                yield return null;
            }
            GameObject.FindObjectOfType<WordBaseViwe>().Lode(WordBase.Wordgs.IndexOf(word));
        }
    }
}