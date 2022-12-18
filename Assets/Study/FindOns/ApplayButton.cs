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
        private static FindOnsSystem FindOnsSystem => _FindOnsSystem ??= FindObjectOfType<FindOnsSystem>();
        private static FindOnsSystem _FindOnsSystem;
        private TMP_Text text => _text ??= GetComponentInChildren<TMP_Text>();
        private TMP_Text _text;
        private ColorChanger ColorChanger => _ColorChanger ??= GetComponentInChildren<ColorChanger>();
        private ColorChanger _ColorChanger;
        public Color ColorChooseOne;
        public Color ColorApplay;
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnButton);
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

        private static bool Done;
        public void OnButton()
        {
            if (FindonesContent.SellectedObject == null) return;
            if (Done) return;
            if (FindOnsSystem.GameContent.Equals(FindonesContent.SellectedObject.Content))
            {
                FindonesContent.SellectedObject.GetComponent<ColorChanger>().SetColor(Color.green);
                if (FindonesContent.SellectedObject.transform.parent == FindOnsSystem.QueatinContentParrent) return;
                Done = true;
                StartCoroutine(WaitANdTrayAgane());
                IEnumerator WaitANdTrayAgane()
                {
                    yield return new WaitForSeconds(2);
                    DiscretionViwe.ShowWord((FindonesContent.SellectedObject.Content as Word?).Value);
                    yield return new WaitForSeconds(0.2f);
                    Done = false;
                    FindOnsSystem.Rondomize();
                }
            }
            else FindonesContent.SellectedObject.GetComponent<ColorChanger>().SetColor(Color.red);
        }
    }
}