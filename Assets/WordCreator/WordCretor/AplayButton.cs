using Base;
using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace WordCreator.WordCretor
{
    [RequireComponent(typeof(Button))]
    public class AplayButton : MonoBehaviour
    {

        private WordCretorViwe wordContent => _wordContent ??= FindObjectOfType<WordCretorViwe>();
        private WordCretorViwe _wordContent;
        private Image BackImage => _BackImage ??= GetComponent<Image>();
        private Image _BackImage;

        public TMP_Text ButtonText;
        // allredy Added
        // unknowen
        // redytoaplay
        // NeedMore Info

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClickButton);
            ButtonText.text = "UnKnowen";
        }
        [SerializeField, HorizontalGroup("AplayColor")]
        private Color AplayColor, AplayColorText;
        [SerializeField, HorizontalGroup("AlredyHave")]
        private Color AlredyHaveColor, AlredyHaveTextColor;
        [SerializeField, HorizontalGroup("UnKnowen")]
        private Color UnKnowenColor, UnKnowenTextColor;
        [SerializeField, HorizontalGroup("NeedMoreInfo")]
        private Color NeedMoreInfoColor, NeedMoreInfoTextColor;


        private float PerTime = 0f;
        private void Update()
        {
            PerTime += Time.deltaTime;
            if (PerTime > 1f)
            {
                PerTime = 0;
                SmartUpdate(wordContent.Content);
            }
        }

        public void SmartUpdate(Content N)
        {
            Find();
            Vector2 ds = ButtonText.GetRenderedValues(true);
            ds *= 1.15f;
            BackImage.rectTransform.offsetMax = ds / 2;
            BackImage.rectTransform.offsetMin = -ds / 2;
            transform.position = ButtonText.transform.position;
            void Find()
            {
                if (WordBase.Wordgs.Contains((N as Word))) { AlredyHavef(); return; }
                if (string.IsNullOrEmpty(N.EnglishSource)) { UnKnowenf(); return; }
                if (string.IsNullOrEmpty((N as IDiscreption).EnglishDiscretion)) { NeedMoreInfof(); return; }
                Aplayf();

                void Aplayf()
                {
                    ButtonText.text = "Aplay";
                    ButtonText.color = AplayColorText;
                    BackImage.color = AplayColor;
                }
                void AlredyHavef()
                {
                    ButtonText.text = "Alredy Have";
                    ButtonText.color = AlredyHaveTextColor;
                    BackImage.color = AlredyHaveColor;
                }
                void UnKnowenf()
                {
                    ButtonText.text = "UnKnowen";
                    ButtonText.color = UnKnowenTextColor;
                    BackImage.color = UnKnowenColor;
                }
                void NeedMoreInfof()
                {
                    ButtonText.text = "Need More Info";
                    ButtonText.color = NeedMoreInfoTextColor;
                    BackImage.color = NeedMoreInfoColor;
                }

            }
        }

        public void OnClickButton()
        {
            if (WordBase.Wordgs.Contains((wordContent.Content as Word))) return;
            if (string.IsNullOrEmpty(wordContent.Content.EnglishSource)) return;
            wordContent.TryAdd();
        }

    }
}