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
    public class AddToBaseButton : MonoBehaviour
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
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Content.Content.BaseCommander.Add(Content.Content);
            });
        }

        private float _PerTime;
        private void Update()
        {
            if (Content.Content == null) return;
            _PerTime += Time.deltaTime;
            if (_PerTime > .4f)
            {
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
                 
                
                              
                _PerTime = 0;
                
            }

        }
    }
}