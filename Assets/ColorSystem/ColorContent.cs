using Base;
using Base.Dialog;
using Base.Word;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectSettings 
{
    public partial class ProjectSettings 
    {
        public Gradient ContentLoopColors;
    }
}
public class ColorContent : OptimizedBehaver, IQueueUpdate
{
    [SerializeField]
    private List<MaskableGraphic> Arts;
    private protected ContentObject ContentObject;
    private Gradient ContentLoopColors;
    private protected override void Start()
    {
        base.Start();
        ContentObject = GetComponent<ContentObject>();
        ContentLoopColors = ProjectSettings.ProjectSettings.Mine.ContentLoopColors;
    }

    public void TurnUpdate()
    {
        if (ContentObject == null) return;
        if (ContentObject.Content == null) return;
        int Index = ContentObject.Content.BaseCommander.IndexOf(ContentObject.Content);
        Color NC = GetColor(Index, ContentObject.Content.Active);
        for (int i = 0; i < Arts.Count; i++) 
        {
            if (Arts[i] == null) continue;
            Arts[i].color = NC;
        }
    }

    private protected virtual Color GetColor(int index, bool IsActive)
    {
        index = Mathf.Abs(index);
        int levv = (index / 30);
        index -= (levv * 30);
        Color color = ContentLoopColors.Evaluate((float)index / 30f);
        if (IsActive) return color;
        return Color.Lerp(color, Color.black, .3f);
    }

}
