using Base;
using Base.Dialog;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace WordCreator.DialogCreator
{
    [RequireComponent(typeof(Button))]
    public class AplayButton : MonoBehaviour
    {
        private DialogCreator wordContent => _wordContent ??= FindObjectOfType<DialogCreator>();
        private DialogCreator _wordContent;
        private Image BackImage => _BackImage ??= GetComponent<Image>();
        private Image _BackImage;
        [Required]
        public TMP_Text ButtonText;
        // allredy Added
        // unknowen
        // redytoaplay
        // NeedMore Info

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClickButton);
            wordContent.OnValueChanged += SmartUpdate;
            StartCoroutine(enumerator());
            IEnumerator enumerator()
            {
                ButtonText.text = "UnKnowen";
                yield return new WaitForEndOfFrame();
                SmartUpdate(wordContent.Content);
            }

        }
        [SerializeField, HorizontalGroup("AplayColor")]
        private Color AplayColor, AplayColorText;
        [SerializeField, HorizontalGroup("AlredyHave")]
        private Color AlredyHaveColor, AlredyHaveTextColor;
        [SerializeField, HorizontalGroup("UnKnowen")]
        private Color UnKnowenColor, UnKnowenTextColor;
        public void SmartUpdate(IContent N)
        {
            Find();
            Vector2 ds = ButtonText.GetRenderedValues(true);
            ds *= 1.15f;
            BackImage.rectTransform.offsetMax = ds / 2;
            BackImage.rectTransform.offsetMin = -ds / 2;
            transform.position = ButtonText.transform.position;
            void Find()
            {
                if (DialogBase.Dialogs.Contains((N as Dialog?).Value)) { AlredyHavef(); return; }
                if (string.IsNullOrEmpty(N.EnglishSource)) { UnKnowenf(); return; }
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
                
            }
        }

        public void OnClickButton()
        {
            if (DialogBase.Dialogs.Contains((wordContent.Content as Dialog?).Value)) return;
            if (string.IsNullOrEmpty(wordContent.Content.EnglishSource)) return;
            wordContent.TryAdd();
        }
    }
}