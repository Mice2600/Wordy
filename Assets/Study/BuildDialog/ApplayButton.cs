using Base.Dialog;
using Servises;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Study.BuildDialog
{
    public class ApplayButton : MonoBehaviour
    {
        public BuildDialogVewe BuildDialogVewe;
        private bool Done = false;
        private void Start()
        {
            BuildDialogVewe = GetComponentInParent<BuildDialogVewe>();
            GetComponent<Button>().onClick.AddListener(OnButton);
        }
        private void OnButton() 
        {
            if (Done) return;
            if (!BuildDialogVewe.isAnyGrope) return;
            Dialog Groped = BuildDialogVewe.Groped;
            Done = true;
            if (BuildDialogVewe.isTrueGrope)
            {

            }
            else 
            {
                
            }

        }
        [SerializeField]
        private Color RedyColor, NotRedyColor;
        private ColorChanger ColorChanger => _ColorChanger ??= GetComponentInChildren<ColorChanger>();
        private ColorChanger _ColorChanger;
        void Update()
        {
            if (BuildDialogVewe.isAnyGrope)
            {
                ColorChanger.SetColor(RedyColor);
            }
            else 
            {
                ColorChanger.SetColor(NotRedyColor);
            }
        }
    }
}
