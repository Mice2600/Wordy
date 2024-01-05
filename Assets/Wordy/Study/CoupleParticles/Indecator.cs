using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Study.CoupleParticles
{
    public class Indecator : MonoBehaviour
    {

        CoupleParticles coupleParticles;
        public TextMeshProUGUI TextMeshProUGUI;
        public GameObject RemoveButton;
        private void Start()
        {
            coupleParticles = GetComponentInParent<CoupleParticles>();
            TextMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
            RemoveButton.GetComponent<Button>().onClick.AddListener(OnRemoveButton);
        }
        void Update()
        {
            TextMeshProUGUI.text = coupleParticles.CollectedString;
            RemoveButton.SetActive(!string.IsNullOrEmpty(coupleParticles.CollectedString));
        }
        public void OnRemoveButton()
        {
            coupleParticles.CollectedString = "";
        }
    }
}