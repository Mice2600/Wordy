using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public interface IQueueUpdate
{
    public void TurnUpdate();
    public void AddMe()
    {
        System.Type NT = (this as MonoBehaviour).GetType();
        if (!queueUpdates.ContainsKey(NT))
        {
            queueUpdates.Add(NT, new List<IQueueUpdate>() { this });
            CurrentIndexer.Add(NT, 0);
            SystemBox.Engine.Engine.Update_Engins((this as MonoBehaviour).GetType().Name, delegate () { CallNext(NT); });
            return;
        }
        if (queueUpdates[NT].Contains(this)) return;
        queueUpdates[NT].Add(this);
    }
    public void RemoveMe()
    {
        System.Type NT = (this as MonoBehaviour).GetType();
        if (!queueUpdates.ContainsKey(NT)) return;
        queueUpdates[NT].Remove(this);
    }

    public static Dictionary<System.Type, List<IQueueUpdate>> queueUpdates = new Dictionary<System.Type, List<IQueueUpdate>>();



    public static void CallNext(System.Type type)
    {
        List<IQueueUpdate> NList = queueUpdates[type];
        if (NList.Count == 0) { Reset(type); return; }
        int NeedCount = NList[0].QueuecallCount;
        if (NeedCount >= NList.Count)
        {
            for (int i = 0; i < NList.Count; i++)
            {
                if((NList[i] as MonoBehaviour).isActiveAndEnabled)NList[i].TurnUpdate();
            }
            Reset(type);
            return;
        }
        if (CurrentIndexer[type] >= NList.Count) { CurrentIndexer[type] = 0; }
        for (int i = 0; i < NeedCount; i++)
        {
            if ((NList[CurrentIndexer[type]] as MonoBehaviour).isActiveAndEnabled) NList[CurrentIndexer[type]].TurnUpdate();
            MoveNext(type);
        }
    }
    public int QueuecallCount { get; }

    private static Dictionary<System.Type, int> CurrentIndexer = new Dictionary<System.Type, int>();
    private static void MoveNext(System.Type type)
    {
        CurrentIndexer[type]++;
        if (CurrentIndexer[type] >= queueUpdates[type].Count)
        {
            CurrentIndexer[type] = 0;
        }
    }
    public static void Reset(System.Type type) => CurrentIndexer[type] = 0;
}
