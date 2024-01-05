using UnityEngine;
using SystemBox;
namespace Study.TypeShift
{
    [ExecuteAlways]
    public class VerGropeer : MonoBehaviour
    {
        public void Update()
        {
            TList<Transform> Childs = transform.Childs();

            for (int i = 0; i < Childs.Count; i++) Childs[i].transform.localPosition = (Vector3.down * i * 100);
            return;
            int Fixer = (Childs.Count % 2 == 0) ? 0 : 1;
            for (int i = 0; i < Childs.Count; i++) Childs[i].transform.localPosition = (Vector3.down * i * 100) - ((Vector3.down * (Childs.Count - Fixer) * 100) / 2);
        }
    }
}