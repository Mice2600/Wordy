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
    public class WordBaseViwe : BaseListWithFillter<Word>
    {
        public override List<Word> AllContents => WordBase.Wordgs;
        private protected override int IndexOf(Content content) => base.Contents.IndexOf(content as Word);
        protected override TList<Word> SearchComand(TList<Word> AllContents, string SearchString) => Servises.Search.SearchAll<Word>(AllContents, SearchingString);
    }
}