using Base;
using BaseViwe.DialogViwe;
using BaseViwe.WordViwe;
using Servises;
using Servises.BaseList;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class IdleRepeat : MonoBehaviour, IBaseToolItem
{
    public void OnNewViweOpend(GameObject baseList)
    {
        baseList.TryGetComponent(out WordBaseViwe w);
        baseList.TryGetComponent(out DialogBaseViwe d);
        baseList.TryGetComponent(out IrregularBaseViwe l);
        gameObject.SetActive(w != null || d != null|| l != null);
        if (w != null) ListWithFillter = w;
        if (d != null) ListWithFillter = d;
        if (l != null) ListWithFillter = l;
    }
    BaseListViwe ListWithFillter;

    Coroutine coroutine;
    public void OnButton()
    {
        if (ListWithFillter == null) return;

        coroutine = StartCoroutine(DDDD());

        IEnumerator DDDD() 
        {
            while (true) 
            {

                Content content = new TList<Content>(ListWithFillter.Contents).Mix().Find(a => (a as IPersanalData).Active);
                if (content == null) content = ListWithFillter.Contents.RandomItem();
                DiscretionObject D = DiscretionObject.Show(content, () => { if (coroutine != null) StopCoroutine(coroutine); });
                yield return new WaitForSeconds(1);
                D.GetComponentInChildren<SoundButton>().onClick.Invoke();
                yield return new WaitForSeconds(4);
                D.OnClose = null;
                D.gameObject.ToDestroy();
            }
        }

    }
}
