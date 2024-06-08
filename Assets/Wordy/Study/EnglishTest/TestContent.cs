
using System;
namespace Study.EnglishTest
{
    [Serializable]
    public class TestContent
    {
        public int Level = 1;
        public string RuleInfo = "Fill in the blank";
        public string Question = "If I studied harder, I ____ the exam.";
        public Answer[] Answers;

    }
    [Serializable]
    public struct Answer
    {
        public string AnswerText; // answer
        public string AnswerReason; // whay true or else
        public int Score;//0-10
    }
}