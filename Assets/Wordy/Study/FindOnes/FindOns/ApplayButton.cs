using Base.Word;
using Servises;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
namespace Study.FindOns
{
    [RequireComponent(typeof(Button))]
    public class ApplayButton : MonoBehaviour
    {
        private FindOnsSystem FindOnsSystem;
        private TMP_Text text => _text ??= GetComponentInChildren<TMP_Text>();
        private TMP_Text _text;
        private ColorChanger ColorChanger => _ColorChanger ??= GetComponentInChildren<ColorChanger>();
        private ColorChanger _ColorChanger;
        public Color ColorChooseOne;
        public Color ColorApplay;
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnButton);
            FindOnsSystem = GetComponentInParent<FindOnsSystem>();
        }
        void Update()
        {
            if (FindonesContent.SellectedObject == null)
            {
                text.text = "Choose one";
                ColorChanger.SetColor(ColorChooseOne);
            }
            else
            {
                text.text = "Applay";
                ColorChanger.SetColor(ColorApplay);
            }
        }

        private bool Done;
        public void OnButton()
        {
            if (FindonesContent.SellectedObject == null) return;
            if (Done) return;
            if (FindonesContent.SellectedObject.transform.parent == FindOnsSystem.QueatinContentParrent) return;
            FindOnsSystem.OnApplayButton(FindonesContent.SellectedObject.Content as Word);
            Done = true;
            
        }
    }
}