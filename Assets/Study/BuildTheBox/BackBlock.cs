using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class BackBlock : MonoBehaviour
{
    static BackBlock(){ Boxes = new Dictionary<Vector2Int, BackBlock>(); }
    public static Dictionary<Vector2Int, BackBlock> Boxes;




    [SerializeField, Required]
    private GameObject LineUp, LineDown, LineLeft, LineRight;
    [SerializeField, Required]
    private GameObject  ConerDownLeft, ConerDownRight, ConerUpLeft, ConerUpRight;

    [SerializeField, HideInEditorMode]
    private BackBlock Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight;

    private void Start()
    {

        Boxes.Add(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()), this);

        StartCoroutine(AfterFrame());

        IEnumerator AfterFrame() 
        {
            yield return new WaitForEndOfFrame();

            Up = FindBox(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()) + Vector2Int.up);
            Down = FindBox(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()) + Vector2Int.down);
            Left = FindBox(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()) + Vector2Int.left);
            Right = FindBox(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()) + Vector2Int.right);


            UpRight = FindBox(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()) + Vector2Int.right + Vector2Int.up);
            UpLeft = FindBox(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()) + Vector2Int.left + Vector2Int.up);
            DownRight = FindBox(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()) + Vector2Int.right + Vector2Int.down);
            DownLeft = FindBox(new Vector2Int(transform.localPosition.x.ToInt(), transform.localPosition.y.ToInt()) + Vector2Int.left + Vector2Int.down);


            LineUp.SetActive(Up == null);
            LineDown.SetActive(Down == null);
            LineLeft.SetActive(Left == null);
            LineRight.SetActive(Right == null);

            ConerUpRight.SetActive(UpRight == null);
            ConerUpLeft.SetActive(UpLeft == null);

            ConerDownRight.SetActive(DownRight == null);
            ConerDownLeft.SetActive(DownLeft == null);



            BackBlock FindBox(Vector2Int NeedPos)
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
