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
    public class WordBaseViwe : BaseListWithFillter<Word>, IRemoveButtonUser
    {
        public override List<Word> AllContents => WordBase.Wordgs;
        private protected override int IndexOf(Content content) => base.Contents.IndexOf(content as Word);
        public void OnRemoveButton(Content content)
        {
            WordBase.Wordgs.Remove(content as Word);
            FindObjectOfType<DiscretionViwe>()?.DestroyUrself();
            Refresh();
        }
    }
}