using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Study.Ring
{
    public class Letter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public static TList<Letter> Sellected;
        public static bool LastActivation;
        public void OnPointerDown(PointerEventData eventData)
        {
            Sellected = new TList<Letter>(this);
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {




            LastActivation = true;

            if (Sellected == null)return; 
            if(Sellected.IsEnpty())return;
            if(Sellected.Last == this)return;

            Sellected.Add(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Sellected == null) return;
            if (Sellected.IsEnpty()) return;
            LastActivation = false; 
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Sellected == null) return;
            if (Sellected.IsEnpty()) return;
            string String = "";
            Debug.Log(Sellected.Count);
            Sellected.ForEach(x => String += x.GetComponentInChildren<TextMeshProUGUI>().text);
            Sellected = new TList<Letter>();
            Debug.Log(String);
        }

        [Required]
        public RectTransform Liner;
        [Required]
        public GameObject Activer;

        void Start()
        {

        }
        private void LateUpdate()
        {
            Activer.SetActive(Sellected != null && !Sellected.IsEnpty() && Sellected.Last == this);
            if (!LastActivation && TInput.GetMouseButtonUp(0, true))
            {
                Sellected = new TList<Letter>();

            }
        }
    }
}