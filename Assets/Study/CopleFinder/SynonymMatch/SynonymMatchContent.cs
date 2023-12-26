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

        protected string TextSEllectedtext;
        protected override string Text
        {
            get
            {
                if (!IsFirst) 
                {
                    TextSEllectedtext = (Content as Synonym).attachments.RandomItem();
                    return TextUtulity.UnderLine(TextSEllectedtext);

                }
                    
                

                else return Content.EnglishSource;

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