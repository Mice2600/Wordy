using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Base;
using Traonsletor;
using Base.Dialog;

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Required]
        [BoxGroup("Changers")]
        public GameObject DialogChanger;
    }
}

public class DialogChanger : ContentObject
{
    public static void StartChanging(Dialog NeedChanger = null)
    {
        NeedChanger ??= new Dialog("", "", 0, false);
        Instantiate(ProjectSettings.ProjectSettings.Mine.DialogChanger).GetComponent<DialogChanger>().StartSet(NeedChanger);
    }

    [Required]
    public TMP_InputField WordWriter;
    [Required]
    public TextMeshProUGUI WordTronsleated;
    [Required]
    public Slider ScoreSlider;
    private void StartSet(Dialog NeedChanger)
    {
        this.Content = NeedChanger;
        WordWriter.text = NeedChanger.EnglishSource;
        WordTronsleated.text = NeedChanger.RussianSource;
        ScoreSlider.value = ((NeedChanger as IPersanalData).ScoreConculeated) / 100f;
    }
    public void TryAplay()
    {
        this.Content.EnglishSource = WordWriter.text;
        this.Content.RussianSource = WordTronsleated.text;
        (this.Content as IPersanalData).ScoreConculeated = (ScoreSlider.value) * 100f;

        if (!DialogBase.Dialogs.Contains(Content as Dialog))
        {
            DialogBase.Dialogs.Add(Content as Dialog);
            DialogBase.Sort();
        }

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
            void onWordTransleted(string tt)
            {
                Debug.Log(tt);
                WordTronsleated.text = tt;
            }
        }
    }

    public void OnDestroyUrself()
    {
        Destroy(gameObject);
    }
}

