using UnityEngine;

public class IdleRepeat : MonoBehaviour//, IBaseToolItem
{
    /*
    public void OnNewViweOpend(GameObject baseList)
    {
        baseList.TryGetComponent(out DialogBaseViwe d);
        baseList.TryGetComponent(out IrregularBaseViwe l);
        gameObject.SetActive(d != null|| l != null);
        if (d != null) ListWithFillter = d;
        if (l != null) ListWithFillter = l;
    }
    

    Coroutine coroutine;
    public void OnButton()
    {
        if (ListWithFillter == null) return;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        Content content = null;
        coroutine = StartCoroutine(DDDD());
        IEnumerator DDDD() 
        {
            while (true) 
            {
                if(content == null) content = new TList<Content>(ListWithFillter.Contents).Mix().Find(a => (a as IPersanalData).Active);
                if (content == null) content = ListWithFillter.Contents.RandomItem();
                DiscretionObject D = DiscretionObject.Show(content, () => { if (coroutine != null) StopCoroutine(coroutine);
                    Screen.sleepTimeout = SleepTimeout.SystemSetting;
                });
                Dialog Dialog = null;
                if (content is Word) 
                {
                    List<Dialog> dlist = Servises.Search.SearchAll<Dialog>(DialogBase.Dialogs, content.EnglishSource);
                    if(dlist != null && dlist.Count > 0) Dialog = dlist.RandomItem();
                    else Dialog = null;
                }
                yield return new WaitForSeconds(1);
                
                D.GetComponentInChildren<SoundButton>().onClick.Invoke();
                yield return new WaitForSeconds(6);
                D.OnClose = null;
                D.gameObject.ToDestroy();
                content = Dialog;
            }
        }

    }*/
}
