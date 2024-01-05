using Base;
using Servises.SmartText;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Study.BuildDialog
{
    public class BuildDialogContent : ContentObject
    {
        [System.NonSerialized]
        public RectTransform ShadowUp;
        [System.NonSerialized]
        public RectTransform ShadowDown;
        [System.NonSerialized]
        public RectTransform CorrentTarget;
        public BuildDialogVewe BuildDialogVewe;
        private void Start()
        {
            BuildDialogVewe = GetComponentInParent<BuildDialogVewe>();
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (CorrentTarget.position != transform.position) return;
                if (CorrentTarget == ShadowDown) BuildDialogVewe.TryAddMeUp(this);
                else BuildDialogVewe.TryAddMeDown(this);
            });
        }
        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, CorrentTarget.position, Time.deltaTime * 10000f);
        }
    }
}