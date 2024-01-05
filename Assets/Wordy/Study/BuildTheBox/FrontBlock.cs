using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
namespace Study.BuildTheBox
{
    public class FrontBlock : MonoBehaviour
    {




        [SerializeField, Required]
        private GameObject LineUp, LineDown, LineLeft, LineRight;
        [SerializeField, Required]
        private GameObject ConerDownLeft, ConerDownRight, ConerUpLeft, ConerUpRight;

        [SerializeField, HideInEditorMode]
        private FrontBlock Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight;

        private void Start()
        {
            if (transform.parent == null) return;
            Dictionary<Vector3, FrontBlock> Boxes = transform.parent.GetComponentsInChildren<FrontBlock>().ToDictionary(D => D.transform.localPosition);
            StartCoroutine(AfterFrame());

            IEnumerator AfterFrame()
            {
                yield return new WaitForEndOfFrame();

                Up = FindBox(transform.localPosition + Vector3.up);
                Down = FindBox(transform.localPosition + Vector3.down);
                Left = FindBox(transform.localPosition + Vector3.left);
                Right = FindBox(transform.localPosition + Vector3.right);


                UpRight = FindBox(transform.localPosition + Vector3.right + Vector3.up);
                UpLeft = FindBox(transform.localPosition + Vector3.left + Vector3.up);
                DownRight = FindBox(transform.localPosition + Vector3.right + Vector3.down);
                DownLeft = FindBox(transform.localPosition + Vector3.left + Vector3.down);


                LineUp.SetActive(Up == null);
                LineDown.SetActive(Down == null);
                LineLeft.SetActive(Left == null);
                LineRight.SetActive(Right == null);

                ConerUpRight.SetActive(UpRight == null);
                ConerUpLeft.SetActive(UpLeft == null);

                ConerDownRight.SetActive(DownRight == null);
                ConerDownLeft.SetActive(DownLeft == null);



                FrontBlock FindBox(Vector3 NeedPos)
                {
                    if (Boxes.ContainsKey(NeedPos)) return Boxes[NeedPos];
                    return null;
                }


            }

        }
        /*
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position + (Vector3.up * transform.localScale.y), .05f);
            Gizmos.DrawSphere(transform.position + (Vector3.down * transform.localScale.y), .05f);
            Gizmos.DrawSphere(transform.position + (Vector3.right * transform.localScale.y), .05f);
            Gizmos.DrawSphere(transform.position + (Vector3.left * transform.localScale.y), .05f);
        }*/
    }
}