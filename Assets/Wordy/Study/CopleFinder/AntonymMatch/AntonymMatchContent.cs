using Base.Antonym;
using Base.Synonym;
using Study.CopleFinder.SynonymMatch;
using SystemBox;
using UnityEngine;

namespace Study.CopleFinder.AntonymMatch
{
    public class AntonymMatchContent : CopleFinderContent
    {
        protected override string Text
        {
            get
            {
                if (!IsFirst)
                {
                    TextSEllectedtext = (Content as Antonym).attachments.RandomItem();
                    return TextUtulity.UnderLine(TextSEllectedtext);

                }
                TextSEllectedtext = Content.EnglishSource;
                return TextSEllectedtext;
            }
        }
        protected override bool CanUseVoiceToFirst => (!Application.isEditor);
        protected override bool CanUseVoiceToSecond => (!Application.isEditor);
        public override bool IsThereEqualnest()
        {
            string d = (SecondSellected as AntonymMatchContent).TextSEllectedtext;
            if (AntonymBase.Antonyms.Contains(d))
            {

                Antonym SSyn = AntonymBase.Antonyms[new Antonym(d, "", null)];
                bool IsThere = false;

                SSyn.attachments.ForEach(
                (ST) => {
                    if (ST.Contains(Content.EnglishSource, System.StringComparison.OrdinalIgnoreCase))
                        IsThere = true;
                });

                return IsThere;

            }
            return false;
        }
    }
}