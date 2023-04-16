using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SystemBox;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class OptimizedBehaver : MonoBehaviour
{

    public virtual int QueuecallCount => 1;
    private protected virtual void Start()
    {
        PlayerPrefs.SetString("LastCommand", "14");
        if (this is IQueueUpdate)(this as IQueueUpdate).AddMe();
    }
    private protected virtual void OnDestroy()
    {
        if (this is IQueueUpdate) (this as IQueueUpdate).RemoveMe();
    }

    protected private virtual bool UesPerUpdate { get => PerUpdateTime != 0; }
    protected private virtual int PerUpdateTime => 0;
    private int _OnUpdate;

    protected virtual void Update()
    {
        if (!UesPerUpdate) return;
        _OnUpdate++;
        if (_OnUpdate >= PerUpdateTime) 
        {
            _OnUpdate = 0;
            PerUpdate();
        }
    }
    protected virtual void PerUpdate() {}

    public void SelectionLog(object message) 
    {
#if UNITY_EDITOR
        if (UnityEditor.Selection.activeGameObject != gameObject) return;
        Debug.Log(message);
#endif
    }

}
