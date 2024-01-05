using UnityEngine;
namespace Base
{
    public class ContentObject : MonoBehaviour
    {
        public Content Content
        {
            get => _Content;
            set
            {
                _Content = value;
                OnValueChanged?.Invoke(value);
            }
        }
        private Content _Content;

        public System.Action<Content> OnValueChanged;
    }
}