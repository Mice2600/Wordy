using Servises.SmartText;
using Study.CoupleParticles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Base.Word
{
    public class TranslateWordList : ContentText
    {

        public override string GetValue(Content Object)
        {
            if (Object == null) return "";
            string Gg = "";
            (Object as IMultiTranslation).Translations.ForEach(g => Gg += g + "\n");
            return Gg;
        }
    }
}