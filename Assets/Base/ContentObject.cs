using UnityEngine;
namespace Base
{
    public class ContentObject : MonoBehaviour
    {
        public IContent Content
        {
            get => _Content;
            set
            {
                _Content = value;
                OnValueChanged?.Invoke(value);
            }
        }
        private IContent _Content;
        public System.Action<IContent> OnValueChanged;
    }
}