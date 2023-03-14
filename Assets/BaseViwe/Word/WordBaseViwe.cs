using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;
using SystemBox.Engine;
using UnityEngine.SceneManagement;
using Base.Word;
using Servises;
using Servises.BaseList;
using Base;
namespace BaseViwe.WordViwe
{
    public class WordBaseViwe : BaseListWithFillter
    {
        public override List<Content> AllContents => new List<Content>(WordBase.Wordgs);
        public void CreatNewContent() 
        {
            WordChanger.StartChanging();
        }

        private float CCSize;
        public override float GetSizeOfContent(Content content) 
        {
            if (CCSize == 0f) CCSize = content.ContentObject.GetComponent<RectTransform>().rect.height;
            return CCSize;
        }
    }
}