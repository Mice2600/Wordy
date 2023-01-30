using Base;
using Base.Dialog;
using Base.Word;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Servises
{
    [RequireComponent(typeof(Button))]
    public class DiscretionButton : MonoBehaviour
    {
        protected void Start()
        {
            ContentObject s = transform.GetComponentInParent<ContentObject>();
            if (s == null) return;
            if (s.Content is Word) GetComponent<Button>().onClick.AddListener(() => { DiscretionViwe.ShowWord((s.Content as Word)); });
            else if (s.Content is Dialog) GetComponent<Button>().onClick.AddListener(() => { DiscretionViwe.ShowDialog((s.Content as Dialog)); });
            else if (s.Content is Irregular) GetComponent<Button>().onClick.AddListener(() => { DiscretionViwe.ShowIrregular((s.Content as Irregular)); });
        }
    }
}