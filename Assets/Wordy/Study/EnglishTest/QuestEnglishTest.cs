using Sirenix.OdinInspector;
using Study.aSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Serializable]
        public class QuestEnglishTest : StudyContentData
        {
        }
        [FoldoutGroup("questEnglishTest")]
        public QuestEnglishTest questEnglishTest;
    }
}
namespace Study.EnglishTest
{
    public class QuestEnglishTest : Quest
    {
    }
}