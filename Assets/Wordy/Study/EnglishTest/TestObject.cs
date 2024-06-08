using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using SystemBox.Simpls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;
namespace Study.EnglishTest
{
    public class TestObject : MonoBehaviour
    {
        public System.Action BeforeDestroy;
        public TestContent testContent;
        [SerializeField] private GameObject Shadow;
        [SerializeField] private Image BackGraund;
        private List<OptionObject> Options;



        private TestObject UpOtherObject;

        private bool Answered;

        private bool SlideTime;
        private bool DestroingTime;
        private Vector3 StartMousePos;
        private Vector3 StartSlPos;
        [SerializeField]
        private Color DeepColor;

        [SerializeField] private TextMeshProUGUI QuestionTMP;
        [SerializeField] private TextMeshProUGUI RuleTMP;

        void Start()
        {

            Options = GetComponentsInChildren<OptionObject>().ToList();
            int Index = transform.GetSiblingIndex();


            int MyNeedOptionCount = testContent.Answers.Length;

            List<Answer> answers = testContent.Answers.ToList().Mix();
            testContent.Answers = answers.ToArray();

            for (int i = testContent.Answers.Length; i < Options.Count; i++)
            {
                GameObject DD = Options[i].gameObject;
                Destroy(DD);
                Options.RemoveAt(i);
                i--;
            }


            if (transform.parent.childCount != Index + 1)
                UpOtherObject = transform.parent.GetChild(Index + 1).GetComponent<TestObject>();

            Options.ForEach((d, i) =>
            {
                d.answer = answers[i];
                d.OnPressed += () => SlideTime = true;
                if (UpOtherObject != null) d.Close();
            });
            StartCoroutine(enumerator());
            IEnumerator enumerator()
            {
                RuleTMP.text = testContent.RuleInfo;
                QuestionTMP.text = testContent.Question;
                Ajast();
                yield return null;
                Ajast();
                yield return null;
                Ajast();
                yield return null;


                void Ajast()
                {
                    (QuestionTMP.transform as RectTransform).sizeDelta = new Vector2(

                    (QuestionTMP.transform as RectTransform).sizeDelta.x,
                    QuestionTMP.GetRenderedValues().y + 10
                    );

                    (RuleTMP.transform as RectTransform).sizeDelta = new Vector2(

                    (RuleTMP.transform as RectTransform).sizeDelta.x,
                    RuleTMP.GetRenderedValues().y + 10
                    );

                }
            }





        }

        void Update()
        {
            if (UpOtherObject != null)
            {
                float NeedY = (UpOtherObject.transform as RectTransform).offsetMin.y - 50;
                float MyY = (transform as RectTransform).offsetMin.y;

                if (MathF.Abs(NeedY - MyY) > 2f)
                {
                    if (NeedY > MyY) (transform as RectTransform).anchoredPosition += new Vector2(0, 1f);
                    else (transform as RectTransform).anchoredPosition -= new Vector2(0, 1f);
                }

                transform.localScale = Vector3.MoveTowards(transform.localScale, UpOtherObject.transform.localScale * 0.91f, Time.deltaTime);

                if (!UpOtherObject.SlideTime)
                {
                    int NeedOptionCount = UpOtherObject.testContent.Answers.Length;
                    int MyNeedOptionCount = testContent.Answers.Length;
                    for (int i = 0; i < Options.Count; i++)
                    {
                        if (i < NeedOptionCount && i < MyNeedOptionCount) Options[i].Open(true);
                        else Options[i].Close();
                    }
                }


                BackGraund.color = Color.Lerp(UpOtherObject.BackGraund.color, DeepColor, 0.2f);
                Shadow.gameObject.SetActive(false);



            }
            else
            {

                if (!SlideTime)
                {
                    transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime);
                    (transform as RectTransform).anchoredPosition = Vector2.MoveTowards((transform as RectTransform).anchoredPosition, Vector2.zero, 10f);
                    Shadow.gameObject.SetActive(true);
                    BackGraund.color = Color.white;
                }




                for (int i = 0; i < testContent.Answers.Length; i++)
                {
                    Options[i].Open();
                }

                if (SlideTime)
                {
                    if (DestroingTime) return;
                    if (TInput.GetMouseButtonDown(0, true))
                    {
                        StartMousePos = TInput.mousePosition(0);
                        StartSlPos = (transform as RectTransform).anchoredPosition;
                    }
                    else if (TInput.GetMouseButton(0, true))
                    {
                        Vector3 Ofset = TInput.mousePosition(0) - StartMousePos;
                        (transform as RectTransform).anchoredPosition = StartSlPos + Ofset;
                    }
                    else if (TInput.GetMouseButtonUp(0, true))
                    {
                        if (MathF.Abs((transform as RectTransform).anchoredPosition.x) > 400)
                        {
                            DestroingTime = true;
                            StartCoroutine(enumerator());
                            IEnumerator enumerator()
                            {
                                for (int i = 0; i < 40; i++)
                                {
                                    (transform as RectTransform).anchoredPosition =
                                        Vector2.MoveTowards((transform as RectTransform).anchoredPosition,
                                        (transform as RectTransform).anchoredPosition * 1.1f, 15);
                                    yield return null;
                                }
                                BeforeDestroy?.Invoke();
                                Destroy(gameObject);
                            }
                        }
                    }
                    else if (!DestroingTime)
                    {
                        (transform as RectTransform).anchoredPosition = Vector2.MoveTowards((transform as RectTransform).anchoredPosition, Vector2.zero, 10f);
                    }


                }


            }



        }

    }
}