using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Base;
using Base.Word;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Servises
{
    [RequireComponent(typeof(Button))]
    public class AddToBaseButton : OptimizedBehaver, IQueueUpdate
    {
        [SerializeField]
        private GameObject AddIcon;
        [SerializeField]
        private GameObject CompletedIcon;
        private ContentObject Content
        {
            get
            {
                if (_Content != null) return _Content;
                _Content = transform.GetComponentInParent<ContentObject>();
                return _Content;
            }
        }
        private ContentObject _Content;

        private protected override void Start()
        {
            base.Start();
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Content.Content.BaseCommander.Add(Content.Content);
            });
        }
        public void TurnUpdate()
        {
            if (Content.Content == null) return;
            if (Content.Content.BaseCommander.Contains(Content.Content))
            {
                AddIcon.SetActive(false);
                CompletedIcon.SetActive(true);
            }
            else
            {
                AddIcon.SetActive(true);
                CompletedIcon.SetActive(false);
            }
        }
    }
}