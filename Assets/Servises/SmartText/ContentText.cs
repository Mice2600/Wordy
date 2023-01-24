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
        public ContentObject MyContent;
        private void Start()
        {
            MyContent = transform.GetComponentInParent<ContentObject>();
        }
        public abstract void OnValueChanged(Content Object);
        public override string MyText => MyTextContent;
        private protected string MyTextContent
        {
            get => _MyText;
            set
            {
                _MyText = value;
                base.Update();
            }
        }
        private string _MyText;

        float perTime = 10f;

        protected override void Update() 
        {
            perTime += Time.deltaTime;
            if (perTime > .4f) 
            {
                perTime = 0f;
                OnValueChanged(MyContent.Content);
            }
        }
    }
}