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
        [Button]
        public override void Lode(int From)
        {
            base.Lode(From);
            contentPattent.Childs().ForEach(child =>
            {
                RemoveButton[] UIButtons = child.GetComponentsInChildren<RemoveButton>();
                GameObject DD = child.gameObject;
                for (int i = 0; i < UIButtons.Length; i++) UIButtons[i].onClick.AddListener(() => TryRemove(DD));
            });

        }
        public void TryRemove(GameObject Current)
        {
            Word Content = (Current.GetComponent<ContentObject>().Content as Word);
            WordBase.Wordgs.Remove(Content);
            Refresh();
        }


    }
}