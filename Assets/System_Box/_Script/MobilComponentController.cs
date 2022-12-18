using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SystemBox
{
    public class MobilComponentController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touches.Length == 2)
            {
                _Scaling();
                return;
            }
        }

        [Space]
        public int m_minScale;
        public int m_maxScale;
        private float initialFingersDistance;

        private Vector3 initialScale;
        void _Scaling()
        {
            if (Input.touches.Length == 2)
            {
                Touch t1 = Input.touches[0];
                Touch t2 = Input.touches[1];

                if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
                {
                    initialFingersDistance = Vector2.Distance(t1.position, t2.position);
                    initialScale = transform.localScale;
                }
                else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
                {

                    float currentFingersDistance = Vector2.Distance(t1.position, t2.position);
                    var scaleFactor = currentFingersDistance / initialFingersDistance;

                    Vector3 m_scale = initialScale * scaleFactor;

                    m_scale.x = Mathf.Clamp(m_scale.x, m_minScale, m_maxScale);
                    m_scale.y = Mathf.Clamp(m_scale.y, m_minScale, m_maxScale);
                    m_scale.z = Mathf.Clamp(m_scale.z, m_minScale, m_maxScale);
                    transform.localScale = m_scale;
                    // m_detailstext.text = m_scale.ToString();
                }
            }

        }

    }
}