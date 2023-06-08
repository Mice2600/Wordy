using System.Collections;
using System.Linq;
using SystemBox.Engine;
using SystemBox.Simpls;
using UnityEngine;

public class TCamera : MonoBehaviour
{
    public Camera GameCamera => _GameCamera ??= GetComponent<Camera>();
    private Camera _GameCamera;
    private BitBenderGames.MobileTouchCamera CameraTouchController => (_CameraTouchController ??= GameCamera.GetComponent<BitBenderGames.MobileTouchCamera>());
    private BitBenderGames.MobileTouchCamera _CameraTouchController;
    public void OnDragSceneObject() => CameraTouchController.OnDragSceneObject();
    public void Start()
    {
        FindObjectsOfType<Camera>().ToList().ForEach(c => c.gameObject.SetActive(false));
        gameObject.SetActive(true);
        //TLend.DobleClick += Fox;
    }
    bool IsDragging;
    private void Update()
    {
        Vector2 vector = GameCamera.ScreenToWorldPoint(TInput.mousePosition(0), Camera.MonoOrStereoscopicEye.Left);
       // CameraTouchController.enabled = InputTest.ControllingObject == null;

        if (!IsDragging && InputTest.ControllingObject != null)
        {
            CameraTouchController.OnDragSceneObject();
            IsDragging = true;
        }
        else if (InputTest.ControllingObject == null)
        {
            IsDragging = false;
        }
        
        
        if (InputTest.ControllingObject != null)
        {
            if (vector.x > 0.85f)
            {
                GameCamera.transform.position += Vector3.right * Time.deltaTime * 10;
                if (GameCamera.transform.position.x > 18f) GameCamera.transform.position =new Vector3(18, GameCamera.transform.position.y, GameCamera.transform.position.z);
            }
            else if (vector.x < -0.85f) 
            {
                GameCamera.transform.position += Vector3.left * Time.deltaTime * 10;
                if (GameCamera.transform.position.x < -18f) GameCamera.transform.position = new Vector3(-18, GameCamera.transform.position.y, GameCamera.transform.position.z);
            }
            if (vector.y > 0.68f)
            {
                GameCamera.transform.position += Vector3.up * Time.deltaTime * 10;
                if (GameCamera.transform.position.y > 18f) GameCamera.transform.position = new Vector3(GameCamera.transform.position.x, 18, GameCamera.transform.position.z);
            }
            else if (vector.y < -0.85f) 
            {
                GameCamera.transform.position += Vector3.down * Time.deltaTime * 10;
                if (GameCamera.transform.position.y < -18f) GameCamera.transform.position = new Vector3(GameCamera.transform.position.x, -18, GameCamera.transform.position.z);
            }

        }

    }
    private Coroutine ZoomTime;
    public void Fox(Vector2 Point) => Fox(Point, -1);
    public void Fox(Vector2 Point, float _NeedZoom)
    {
        ZoomTime = Engine.Get_Engine("Came").StartCoroutine(ZoomCoroutine());
        IEnumerator ZoomCoroutine()
        {
            if (_NeedZoom < CameraTouchController.CamZoomMin)
                _NeedZoom = (CameraTouchController.CamZoom < 7f) ? 9f : _NeedZoom = 4f;
            Vector3 _NeedZoomPos = new Vector3(Point.x, Point.y, CameraTouchController.transform.position.z);
            while (true)
            {

                if (InputTest.ControllingObject == null)
                {
                    CameraTouchController.CamZoom = Mathf.Lerp(CameraTouchController.CamZoom, _NeedZoom, Time.deltaTime * 15f);
                    GameCamera.transform.position = Vector3.Lerp(GameCamera.transform.position, _NeedZoomPos, Time.deltaTime * 15f);
                    //if(Vector2.Distance(GameCamera.transform.position, _NeedZoomPos) < .3f) _ZoomTime = false;
                    if (TMath.Distance(GameCamera.orthographicSize, _NeedZoom) < .2f && Vector3.Distance(GameCamera.transform.position, _NeedZoomPos) < .2f)
                    { ZoomTime = null; yield break; }
                }
                else { ZoomTime = null; yield break; }
            }
        }
    }
    public void Reset()
    {
        if (ZoomTime != null)
            Engine.Get_Engine("Came").StopCoroutine(ZoomTime);
        GameCamera.transform.position = new Vector3(0, 0, GameCamera.transform.position.z);
    }

    private void OnDestroy()
    {
        FindObjectsOfType<Camera>(true).ToList().ForEach(c => { if(c != null) c.gameObject.SetActive(true); });
    }

}
