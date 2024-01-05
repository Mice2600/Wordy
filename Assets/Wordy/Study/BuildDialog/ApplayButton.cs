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
        [SerializeField]
        private GameObject OutBlocker;
        private void Start()
        {
            BuildDialogVewe = GetComponentInParent<BuildDialogVewe>();
            GetComponent<Button>().onClick.AddListener(OnButton);
            OutBlocker.SetActive(false);
        }

        private void OnButton()
        {
            if (OutBlocker.activeSelf) return;
            if (!BuildDialogVewe.isAnyGrope) return;
            OutBlocker.SetActive(true);
            OutBlocker.transform.SetAsLastSibling();

            BuildDialogVewe.TryApplay();

        }
        [SerializeField]
        private Color RedyColor, NotRedyColor, WrongAnswer, TrueAnswer;
        private ColorChanger ColorChanger => _ColorChanger ??= GetComponentInChildren<ColorChanger>();
        private ColorChanger _ColorChanger;
        void Update()
        {
            if (OutBlocker.activeSelf) return;
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
