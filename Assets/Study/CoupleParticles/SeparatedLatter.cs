using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Study.CoupleParticles
{
    public class SeparatedLatter : MonoBehaviour
    {
        void Start()
        {
            CoupleParticles coupleParticles = FindObjectOfType<CoupleParticles>();
            TextMeshProUGUI textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
            GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                coupleParticles.CollectedString += textMeshProUGUI.text;
            });
            StartCoroutine(Update());
            IEnumerator Update()
            {
                while (true)
                {
                    yield return new WaitForSeconds(Random.Range(.5f, 1.5f));
                    coupleParticles.FindMyContent(textMeshProUGUI.text, out Color Color);
                    textMeshProUGUI.color = Color;
                }

            }
        }
    }
}