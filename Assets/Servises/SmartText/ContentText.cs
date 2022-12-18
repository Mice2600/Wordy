using Base;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
namespace Servises.SmartText
{
    public abstract class ContentText : SmartText
    {
        private void Start()
        {
            Transform ToTest = transform;
            for (int i = 0; i < 20; i++)
            {
                if (ToTest.TryGetComponent<ContentObject>(out ContentObject wordContent))
                {
                    wordContent.OnValueChanged += OnValueChanged;
                    OnValueChanged(wordContent.Content);
                    break;
                }
                ToTest = ToTest.parent;
                if (ToTest == null) break;
            }
        }
        public abstract void OnValueChanged(IContent Object);
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
        protected override void Update() { }
    }
}