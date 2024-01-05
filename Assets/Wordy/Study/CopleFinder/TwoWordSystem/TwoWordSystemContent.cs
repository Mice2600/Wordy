using Base;
using Base.Word;
using Study.CopleFinder;
using System.Collections;
using SystemBox;
using TMPro;
using UnityEngine;
namespace Study.TwoWordSystem
{
    public class TwoWordSystemContent : CopleFinderContent
    {
        protected override string Text {
            get 
            {
                if (IsFirst) return Content.EnglishSource;
                return (Content as IMultiTranslation).Translations.RandomItem;
            }
        }

        protected override bool CanUseVoiceToFirst => (!Application.isEditor);

        protected override bool CanUseVoiceToSecond => false;
    }
}