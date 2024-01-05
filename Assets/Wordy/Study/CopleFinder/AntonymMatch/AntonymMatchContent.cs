using Base.Antonym;
using SystemBox;
using UnityEngine;

namespace Study.CopleFinder.AntonymMatch
{
    public class AntonymMatchContent : CopleFinderContent
    {
        protected override string Text { get {
                if (IsFirst) return Content.EnglishSource;
                else return (Content as Antonym).attachments.RandomItem();
            } 
        }

        protected override bool CanUseVoiceToFirst => (!Application.isEditor);

        protected override bool CanUseVoiceToSecond => (!Application.isEditor);
    }
}