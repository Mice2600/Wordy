using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Study.EnglishTest
{
    public class OptionObject : MonoBehaviour
    {
        public System.Action OnPressed;
        public Answer answer;

        [SerializeField]
        private Image Planes;
        [SerializeField]
        private List<Color> PlanesColors;// Option Wrong Ok True
        [SerializeField]
        private List<GameObject> Marks;// Option Wrong Ok True

        [SerializeField]
        private TextMeshProUGUI Text;

        private static float DefoultHight;

        private bool IsSized = true;

        private bool IsOpend = false;

        void Start()
        {
            if (DefoultHight == 0)
                DefoultHight = (transform as RectTransform).sizeDelta.y;
            GetComponent<Button>().onClick.AddListener(() => { ShowAnswer(true); });
        }



        [Button]
        public void Close()
        {
            if (IsOpend) return;
            IsSized = false;
            GetComponent<LayoutElement>().ignoreLayout = true;
            GetComponent<Button>().enabled = false;
            (transform as RectTransform).offsetMax = new Vector2((transform as RectTransform).offsetMax.x, 0);
            (transform as RectTransform).offsetMin = new Vector2((transform as RectTransform).offsetMin.x, 0);
            Planes.color = PlanesColors[0];
            Planes.gameObject.SetActive(false);
            Marks.ForEach(a => a.SetActive(false));
            Text.gameObject.SetActive(false);
        }
        [Button]
        public void Open(bool Imedetly = false)
        {
            if (IsOpend) return;
            Text.text = answer.AnswerText;// Do Answer
            if (IsSized) return;
            Close();
            IsSized = true;
            StartCoroutine(GetOpen());
            IEnumerator GetOpen()
            {
                GetComponent<LayoutElement>().ignoreLayout = false;
                while (true)
                {
                    (transform as RectTransform).sizeDelta = Vector2.MoveTowards((transform as RectTransform).sizeDelta, new Vector2((transform as RectTransform).sizeDelta.x, DefoultHight), 5);
                    if ((transform as RectTransform).sizeDelta.y == DefoultHight) break;

                    if (!Imedetly)
                    {

                        yield return null;
                        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
                        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
                        LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
                        LayoutRebuilder.MarkLayoutForRebuild(transform.parent as RectTransform);
                    }

                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
                LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
                LayoutRebuilder.MarkLayoutForRebuild(transform.parent as RectTransform);
                Text.gameObject.SetActive(true);
                Planes.color = PlanesColors[0];
                Planes.gameObject.SetActive(true);
                Marks.ForEach(a => a.SetActive(false));
                Marks[0].SetActive(true);
                yield return new WaitForSeconds(1);
                GetComponent<Button>().enabled = true;

            }
        }



        [Button]
        public void ShowAnswer(bool ShowMark)
        {
            if (IsOpend) return;
            IsOpend = true;
            Text.text = answer.AnswerText + "\n(" + answer.AnswerReason + ")";
            Text.gameObject.SetActive(true);
            int AnswerTypeIndex = 3; //true
            if (answer.Score < 5) AnswerTypeIndex = 1;
            else if (answer.Score < 10) AnswerTypeIndex = 2;
            Planes.color = PlanesColors[AnswerTypeIndex];
            Planes.gameObject.SetActive(true);
            Marks.ForEach(a => a.SetActive(false));
            if (ShowMark)
            {
                Marks[AnswerTypeIndex].SetActive(true);
                OnPressed?.Invoke();
            }
            transform.parent.GetComponentsInChildren<OptionObject>().ToList().ForEach(a => a.ShowAnswer(false));
        }

    }
}