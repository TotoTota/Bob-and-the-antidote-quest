using UnityEngine;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;
    public static SceneController instance;

    public ScreenBox initialSB;

    ScreenBox curSB;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        instance = _instance;
    }

    private void Start()
    {
        ChangeScreenBox(initialSB);
    }

    public void ChangeScreenBox(ScreenBox newScreenBox)
    {
        if(curSB != null)
        {
            curSB.isScreenBox = false;
        }

        curSB = newScreenBox;
        curSB.isScreenBox = true;

        CameraController.instance.SetCamBounds(curSB.minCamPos + (Vector2)curSB.transform.position,
                                               curSB.maxCamPos + (Vector2)curSB.transform.position);
    }
}
