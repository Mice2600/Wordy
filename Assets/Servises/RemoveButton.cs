using Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace Servises
{
    public class RemoveButton : Button
    {

        protected override void Start()
        {
            onClick.AddListener(
            () => {
                List<IRemoveButtonUser > Lis = new List<IRemoveButtonUser>(FindObjectsOfType<MonoBehaviour>().OfType<IRemoveButtonUser>());
                for (int i = 0; i < Lis.Count; i++)Lis[i].OnRemoveButton(gameObject.GetComponentInParent<ContentObject>().Content);
                });
        }

    }
}