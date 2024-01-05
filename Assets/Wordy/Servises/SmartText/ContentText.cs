using Base;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Servises.SmartText
{
    public abstract class ContentText : SmartText
    {
        private ContentObject MyContent => _MyContent ??= transform.GetComponentInParent<ContentObject>();
        private ContentObject _MyContent;
        public abstract string GetValue(Content Object);
        public override string MyText => GetValue(MyContent.Content);
    }
}