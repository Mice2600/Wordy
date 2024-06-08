using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;
namespace Study.EnglishTest
{
    public class EnglishTest : MonoBehaviour
    {
        private int ChosenLevel = 1;
        public TextAsset DefalultContents;


        public Dictionary<int, TList<TestContent>> Contents;
        [SerializeField, Required] private Transform TestObjectParent;
        public GameObject TestObjectPrefab;

        [SerializeField]
        private List<Image> LevelButtons = new List<Image>();


        public void Start()
        {

            Contents = new Dictionary<int, TList<TestContent>>();
            JsonHelper.FromJsonList<TestContent>(DefalultContents.text).ForEach(A => {
                if (!Contents.ContainsKey(A.Level)) Contents.Add(A.Level, new TList<TestContent>());
                Contents[A.Level].Add(A);
            });


            TestObjectParent.ClearChilds();
            for (int i = 0; i < 4; i++)OnContetDestroed();
        }
        public void OnContetDestroed() 
        {

            if (Contents[ChosenLevel].IsEnpty()) return;
            GameObject NObject = Instantiate(TestObjectPrefab, TestObjectParent);
            NObject.transform.SetAsFirstSibling();
            TestObject testObject = NObject.GetComponent<TestObject>();
            testObject.testContent = Contents[ChosenLevel].RemoveRandomItem();
            testObject.BeforeDestroy += OnContetDestroed;
            NObject.GetComponent<CanvasGroup>().alpha = 0f;
            StartCoroutine(enumerator());



            IEnumerator enumerator() 
            {
                yield return new WaitForSeconds(1);
                NObject.GetComponent<CanvasGroup>().alpha = 1f;
            }
            /*
             

            create 10 English test. 
            rules:
                Questions must must be Elementary, Intermediate, Advanced and Proficient levls.
                There should be from 2 to 10 answers.
                Evry answer has an appropriate score.
                Score can tell how close is answer.
                The closest answer receives 10 points, the worst - 0 points.
                There should be 2 wrong answer.
                Give it In Json.
                Give final results not a sample
            Questions typs:
                Multiple Choice,
                True/False,
                Fill in the Blank,
                Choose the Correct Sentence,
                Error Identification,
                Choose the correct definition,
                Gap-filling,
                Sentence Transformation,
                Paraphrasing,
                Short Answer,
                Chosen synonym,
                chosen antonym


                

            as an example you can analyze this:
            {
                {
                    "Level":1,
                    "RuleInfo":"Fill in the blank",
                    "Question":"If I studied harder, I ____ the exam.",
                    "Answers":
                    [
                        {
                            "AnswerText":"passed",
                            "AnswerReason":"incorrect tense",
                            "Score":5
                        },
                        {
                            "AnswerText":"would have passed",
                            "AnswerReason":"correct",
                            "Score":10
                        },
                        {
                            "AnswerText":"am passing",
                            "AnswerReason":"incorrect tense",
                            "Score":0
                        },
                        {
                            "AnswerText":"had pass",
                            "AnswerReason":"grammatical error",
                            "Score":2
                        },
                        {
                            "AnswerText":"would pass",
                            "AnswerReason":"incorrect modal verb",
                            "Score":7
                        },
                        {
                            "AnswerText":"pass better",
                            "AnswerReason":"doesn't express the hypothetical scenario",
                            "Score":3
                        }
                    ]
                },
                {
                    "Level":2,
                    "RuleInfo":"Choose the sentence that uses the phrasal verb 'come across' correctly.",
                    "Question":"If I studied harder, I ____ the exam.",
                    "Answers":
                    [
                        {
                            "AnswerText":"I came across an interesting article online.",
                            "AnswerReason":"correct",
                            "Score":5
                        },
                        {
                            "AnswerText":"We need to come across a solution quickly.",
                            "AnswerReason":"doesn't imply finding something unexpectedly",
                            "Score":7
                        },
                        {
                            "AnswerText":"They came across each other on the street.",
                            "AnswerReason":"implies meeting someone unexpectedly, but not necessarily something",
                            "Score":5
                        },
                        {
                            "AnswerText":"The thief came across the house easily.",
                            "AnswerReason":"doesn't imply finding something unexpectedly",
                            "Score":2
                        },
                        {
                            "AnswerText":"Let's come across with a plan for the project.",
                            "AnswerReason":"incorrect usage of phrasal verb",
                            "Score":0
                        },
                        {
                            "AnswerText":"I hope we **come across** some good weather during the trip.",
                            "AnswerReason":"doesn't imply finding something",
                            "Score":3
                        }
                    ]
                }
            }


            consider json as Convertible to following C# class :

            public class TestContent 
            {
                public int Level = 1;
                public string RuleInfo = "Fill in the blank";
                public string Question = "If I studied harder, I ____ the exam.";
                public Answer[] Answers;
            }
            public struct Answer
            {
                public string AnswerText; 
                public string AnswerReason; 
                public int Score;//0-10
            }
            public enum EnglishLevel
            {
                Elementary = 1, Intermediate = 2, Advanced = 3, Proficient = 4
            }

             */
        }

        [SerializeField]
        private Color UnsellecteColor, SellectedCollor;
        [Button]
        public void OnButton() 
        {
            

                string SD = DefalultContents.text.Replace("Levle", "Level:");
                System.IO.File.WriteAllText(Application.dataPath + "\\Wordy\\Study\\EnglishTest\\TestContents.txt", SD);
            

        }
        public void OnButton(int Level)
        {
            LevelButtons.ForEach(c => c.color = UnsellecteColor);
            LevelButtons[Level - 1].color = SellectedCollor;
            ChosenLevel = Level;
            Start();
        }
    }
}