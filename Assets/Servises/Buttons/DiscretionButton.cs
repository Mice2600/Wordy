using Base;
using Base.Dialog;
using Base.Word;
using UnityEngine;
using UnityEngine.UI;

namespace Servises
{
    [RequireComponent(typeof(Button))]
    public class DiscretionButton : MonoBehaviour
    {
        protected void Start()
        {
            ContentObject s = transform.GetComponentInParent<ContentObject>();
            if (s == null) return;
            GetComponent<Button>().onClick.AddListener(() => {
                DiscretionObject.Show(s.Content);
            });
        }
    }
}