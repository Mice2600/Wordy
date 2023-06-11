using UnityEngine;
using SystemBox;
using Sirenix.OdinInspector;

[ExecuteAlways]
public class HorGropeer : MonoBehaviour
{

    private void Update()
    {
        TList<Transform> Childs = transform.Childs();
        for (int i = 0; i < Childs.Count; i++) 
        {
            Vector3 p = (Vector3.right * i * 100) - ((Vector3.right * (Childs.Count - 1) * 100) / 2);
            p.y = Childs[i].transform.localPosition.y;
            Childs[i].transform.localPosition = p;
        }
    }
}
