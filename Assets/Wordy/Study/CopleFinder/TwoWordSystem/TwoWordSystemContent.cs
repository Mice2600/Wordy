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
                if (IsFirst) {
                     TextSEllectedtext = Content.EnglishSource;
                    return TextSEllectedtext;
                }
                TextSEllectedtext = (Content as IMultiTranslation).Translations.RandomItem;
                return TextSEllectedtext;
            }
        }

        protected override bool CanUseVoiceToFirst => (!Application.isEditor);

        protected override bool CanUseVoiceToSecond => false;
    }
}