using Base;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;
namespace Servises
{
    public abstract class BaseListViwe<T> : MonoBehaviour, RePlaceController where T : IContent
    {
        private ScrollRect scrollRect => _scrollRect ??= GetComponentInChildren<ScrollRect>();
        private ScrollRect _scrollRect;
        [SerializeField]
        protected private GameObject contentPrefab;
        [SerializeField]
        protected private Transform contentPattent;
        [SerializeField]
        protected private Gradient Colors;
        public abstract List<T> Contents { get; }
        private protected abstract int IndexOf(IContent content);
        private protected virtual void Start()
        {
            Lode(0);
        }
        public virtual void Lode(int From)
        {
            contentPattent.ClearChilds();
            if (From < 0) From = 0;
            if (Contents.Count > 20)
            {
                if (From + 20 > Contents.Count)
                    for (int i = 0; i < 20; i++) CreatOne(Contents.Count - (20 - i));
                else for (int i = 0; i < 20; i++) CreatOne(From + i);

            }
            else for (int i = 0; i < Contents.Count; i++) CreatOne(i);
            void CreatOne(int Index)
            {
                GameObject NM = Instantiate(contentPrefab, contentPattent);
                NM.GetComponent<ContentObject>().Content = Contents[Index];
                NM.GetComponent<ColorChanger>().SetColor(GetColor(Index));


            }
            //contentPattent.GetComponent<RectTransform>().rect.Set(0, 0, 0, 0);

            scrollRect.velocity = Vector2.zero;
            scrollRect.verticalNormalizedPosition = 1;

        }

        public virtual void Refresh()
        {
            if (!contentPattent.hasChanged) return;
            IContent FistWord = contentPattent.GetChild(0).GetComponent<ContentObject>().Content;
            int FirstIndex = IndexOf(FistWord);
            Lode(FirstIndex);
        }
        public virtual bool TrayDown()
        {
            IContent lastWord = contentPattent.GetChild(contentPattent.childCount - 1).GetComponent<ContentObject>().Content;
            int LastIndex = IndexOf(lastWord);
            Debug.Log(lastWord.EnglishSource + "  " + LastIndex);
            if (LastIndex + 1 >= Contents.Count) return false;
            contentPattent.GetChild(0).GetComponent<ContentObject>().Content = Contents[LastIndex + 1];
            contentPattent.GetChild(0).GetComponent<ColorChanger>().SetColor(GetColor(LastIndex + 1));
            return true;
        }
        public virtual bool TrayUp()
        {
            IContent FistWord = contentPattent.GetChild(0).GetComponent<ContentObject>().Content;
            int FirstIndex = IndexOf(FistWord);
            if (FirstIndex < 1) return false;
            contentPattent.GetChild(contentPattent.childCount - 1).GetComponent<ContentObject>().Content = Contents[FirstIndex - 1];
            contentPattent.GetChild(contentPattent.childCount - 1).GetComponent<ColorChanger>().SetColor(GetColor(FirstIndex - 1));
            return true;
        }
        public Color GetColor(int index)
        {
            index = Mathf.Abs(index);
            int levv = (index / 10);
            index -= (levv * 10);
            return Colors.Evaluate((float)index / 10f);
        }
    }
}