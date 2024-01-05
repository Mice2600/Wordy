using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using Sirenix.OdinInspector;

namespace Servises.SmartText
{
    public class IrregularParticipleText : ContentText
    {
        [ValueDropdown("ShowTypeValues")]
        public string ShowType = "Both of them";
        private List<string> ShowTypeValues => new List<string> { "Both of them", "Only First", "Only Scond" };
        public override string GetValue(Content Object)
        {
            if (Object == null) return "";
            IIrregular s = Object as IIrregular;
            if (ShowType == "Both of them") return s.PastParticiple;
            else if (s.PastParticiple.Split("/").Length < 2) return s.PastParticiple;
            else if (ShowType == "Only First") return s.PastParticiple.Split("/")[0];
            else if (ShowType == "Only Scond") return s.PastParticiple.Split("/")[1];
            return s.PastParticiple;
        }
    }
}