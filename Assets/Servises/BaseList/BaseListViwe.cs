using Base;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;
namespace Servises.BaseList
{
    public abstract class BaseListViwe : MonoBehaviour, IRemoveButtonUser, RePlaceController
    {
        private ScrollRect scrollRect => _scrollRect ??= GetComponentInChildren<ScrollRect>();
        private ScrollRect _scrollRect;
        [SerializeField]
        protected private GameObject contentPrefab;
        [SerializeField]
        protected private Transform contentPattent;
        
        public abstract List<Content> Contents { get; }
        private protected virtual int IndexOf(Content content) => Contents.IndexOf(content);
        private protected virtual void Start()
        {
            Lode(0);
        }
        public virtual void Lode(int From)
        {
            contentPattent.ClearChilds();
            if (From < 0) From = 0;
            List<Content> Contents = this.Contents;
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
                Content NewContent = Contents[Index];
                NM.GetComponent<ContentObject>().Content = NewContent;


            }
            //contentPattent.GetComponent<RectTransform>().rect.Set(0, 0, 0, 0);

            scrollRect.velocity = Vector2.zero;
            scrollRect.verticalNormalizedPosition = 1;

        }

        public virtual void Refresh()
        {
            if (contentPattent.childCount == 0) 
            {
                Lode(0);
                return; 
            }
            Content FistWord = contentPattent.GetChild(0).GetComponent<ContentObject>().Content;
            int FirstIndex = IndexOf(FistWord);
            Lode(FirstIndex);
        }
        public virtual bool TrayDown()
        {
            Content lastWord = contentPattent.GetChild(contentPattent.childCount - 1).GetComponent<ContentObject>().Content;
            int LastIndex = IndexOf(lastWord);
            List<Content> Contents = this.Contents;
            if (LastIndex + 1 >= Contents.Count) return false;
            Content NewContent = Contents[LastIndex + 1];
            contentPattent.GetChild(0).GetComponent<ContentObject>().Content = NewContent;
            return true;
        }
        public virtual bool TrayUp()
        {
            Content FistWord = contentPattent.GetChild(0).GetComponent<ContentObject>().Content;
            int FirstIndex = IndexOf(FistWord);
            if (FirstIndex < 1) return false;

            Content NewContent = Contents[FirstIndex - 1];

            contentPattent.GetChild(contentPattent.childCount - 1).GetComponent<ContentObject>().Content = NewContent;
            return true;
        }


        public void OnRemoveButton(Content content)
        {
            content.BaseCommander.Remove(content);
            FindObjectOfType<DiscretionViwe>()?.DestroyUrself();
            Refresh();
        }

    }
}