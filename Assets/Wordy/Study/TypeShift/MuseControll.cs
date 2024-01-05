using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemBox;
using SystemBox.Simpls;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
namespace Study.TypeShift
{
    public class MuseControll : MonoBehaviour
    {
        public static TList<MonoBehaviour> Stopers => _Stopers ?? new TList<MonoBehaviour>();
        private static TList<MonoBehaviour> _Stopers;




        public float UpLimit;
        public float DownLimit;

        [Button]
        public Vector3 GGsd() => transform.position;
        [Button]
        public Vector3 GG() => transform.TransformPoint(transform.GetChild(0).transform.localPosition);


        private void Start()
        {

            //int Fixer = (transform.childCount % 2 == 0) ? 100 : 0;
            //UpLimit = transform.TransformPoint(transform.GetChild(0).transform.localPosition).y - Fixer;
            UpLimit = transform.position.y + ((transform.childCount - 1) * 100);// transform.TransformPoint(transform.GetChild(transform.childCount -1).transform.localPosition).y;
                                                                                //DownLimit = transform.position.y - (transform.TransformPoint(transform.GetChild(0).transform.localPosition).y - transform.position.y);
            DownLimit = transform.position.y;// - (transform.TransformPoint(transform.GetChild(0).transform.localPosition).y - transform.position.y);
        }

        public float LastInputYPoint { get; private set; }
        public bool isMowing
        {
            get
            {
                if (Controlling) return true;
                if (transform.localPosition != new Vector3(transform.localPosition.x, GetFixsexY(), 0)) return true;
                return false;
            }
        }

        private float OfsetY;
        private bool Controlling;
        private int TouchIndex;
        public virtual void Update()
        {
            if (Stopers.IsEnpty())
            {
                if (!Controlling)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (TInput.GetMouseButtonDown(i))
                        {
                            Vector3 mouseWorldPoint = TInput.mousePosition(i);
                            if (Mathf.Abs(mouseWorldPoint.x - transform.position.x) < 50f)
                            {
                                TouchIndex = i;
                                Controlling = true;
                                OfsetY = mouseWorldPoint.y - transform.position.y;
                                break;
                            }
                        }
                    }
                }
                else if (TInput.GetMouseButtonUp(TouchIndex)) Controlling = false;
            }
            else { Controlling = false; TouchIndex = 0; }
            if (TInput.Is_Using_Touch && Input.touches.Length == 0) Controlling = false;




            if (Controlling)
            {
                Vector3 mouseWorldPoint = TInput.mousePosition(TouchIndex);
                float y = mouseWorldPoint.y - OfsetY;
                LastInputYPoint = y;
                y = (y > UpLimit) ? UpLimit : y;
                y = (y < DownLimit) ? DownLimit : y;
                transform.position = new Vector3(transform.position.x, y, 0);
            }
            else
            {
                LastInputYPoint = 0;

                float y = GetFixsexY();
                //y = (y > UpLimit) ? UpLimit : y;
                //y = (y < DownLimit) ? DownLimit : y;
                transform.localPosition =
                    Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, y, 0), Time.deltaTime * 200);
            }
        }
        private float GetFixsexY()
        {

            float dasd = transform.localPosition.y - (transform.localPosition.y % 100f);
            int BiCount = (int)dasd / 100;
            if (transform.localPosition.y % 100f > 50) BiCount++;
            return BiCount * 100;
        }

    }
}