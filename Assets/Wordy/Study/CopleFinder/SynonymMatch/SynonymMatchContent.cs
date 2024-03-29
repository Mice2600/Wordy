using Base;
using Base.Synonym;
using System.Linq;
using System.Net.Mail;
using SystemBox;
using TMPro;
using UnityEngine;

namespace Study.CopleFinder.SynonymMatch
{
    public class SynonymMatchContent : CopleFinderContent
    {
        protected override string Text
        {
            get
            {
                if (!IsFirst) 
                {
                    if ((Content as Synonym).attachments.Count < 1)
                        Debug.Log((Content as Synonym).EnglishSource);
                    TextSEllectedtext = (Content as Synonym).attachments.RandomItem();
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
            string d = (SecondSellected as SynonymMatchContent).TextSEllectedtext;
            if (SynonymBase.Synonyms.Contains(d)) 
            {

                Synonym SSyn = SynonymBase.Synonyms[new Synonym(d, "", null)];
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