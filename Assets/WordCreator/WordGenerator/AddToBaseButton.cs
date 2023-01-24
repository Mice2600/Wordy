using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Base;
using Base.Word;

namespace WordCreator.WordGenerator
{
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
                Transform ToTest = transform;
                for (int i = 0; i < 20; i++)
                {
                    if (ToTest.TryGetComponent<ContentObject>(out ContentObject wordContent))
                    {
                        _Content = wordContent;
                        break;
                    }
                    ToTest = ToTest.parent;
                    if (ToTest == null) break;
                }
                return _Content;
            }
        }
        private ContentObject _Content;


        private float _PerTime;
        private void Update()
        {
            _PerTime += Time.deltaTime;
            if (_PerTime > .4f)
            {
                _PerTime = 0;
                if (WordBase.Wordgs.Contains((Content.Content as Word)))
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
}