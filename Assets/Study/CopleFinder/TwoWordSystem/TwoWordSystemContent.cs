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
                if (SideType) return Content.EnglishSource;
                return (Content as IMultiTranslation).Translations.RandomItem;
            }
        }

        protected override bool CanUseVoiceToBothSids => false;
    }
}