using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static InputTest ControllingObject;
    public void OnPointerDown(PointerEventData eventData)
    {
        isControlling = true;
        ControllingObject = this;
        Vector3 ppp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ofsete = transform.position - new Vector3(ppp.x, ppp.y, transform.position.z);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isControlling = false;
        ControllingObject = null;
        NearPos = Builder.FindEnptyPos(gameObject);
    }

    
    void Start()
    {
        NearPos = transform.position;
    }

    private bool isControlling;
    private Vector3 Ofsete;
    private Vector3 NearPos;

    bool IsMoving = true;

    private void Update()
    {
        Vector3 ppp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isControlling) 
        {
            transform.position = Ofsete + new Vector3(ppp.x, ppp.y, transform.position.z);
            IsMoving = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, NearPos, Time.deltaTime * 3);
            if (transform.position != NearPos) IsMoving = false;
            else 
            {
                if (IsMoving == false) 
                {
                    JoinSystem.TrayJoin(transform);
                }
                IsMoving = true;
            }
        }
    }
}
