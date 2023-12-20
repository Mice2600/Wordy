using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace WordCreator.WordCretor {

    public class SynonymField : MonoBehaviour
    {
        [Required]
        public GameObject Toggle;
        public System.Action<TMP_InputField> OnChangeAction;
        private TMP_InputField tMP_InputField;
        public string Text => tMP_InputField.text;
        public void Set(string DefoultString)
        {
            tMP_InputField = GetComponentInChildren<TMP_InputField>();
            tMP_InputField.text = DefoultString;
            tMP_InputField.onValueChanged.AddListener((T) => OnChange(T));

            GetComponentInChildren<Button>().onClick.AddListener(() => {
                Destroy(gameObject);
            });

        }
        public void OnChange(string Text)
        {
            OnChangeAction?.Invoke(tMP_InputField);
            Toggle.SetActive(Base.Synonym.SynonymBase.Synonyms.Contains(Text));
        }
    }
}