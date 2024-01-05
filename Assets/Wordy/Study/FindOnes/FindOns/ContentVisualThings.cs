using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Study.FindOns
{
    [RequireComponent(typeof(FindonesContent))]
    public class ContentVisualThings : MonoBehaviour
    {
        private FindonesContent CodeContent => _FindonesContent ??= GetComponent<FindonesContent>();
        private FindonesContent _FindonesContent;

        private static readonly Vector3 SellectetSize = new Vector3(1.1503f, 1.26533f, 1.1503f);
        private static readonly Vector3 NotSellectetSize = new Vector3(1f, 1f, 1f);
        private void Update()
        {
            transform.localScale = (FindonesContent.SellectedObject == CodeContent) ? SellectetSize : NotSellectetSize;
        }
    }
}