using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFrame : MonoBehaviour
{
    public Transform enterFramePoint;

    public GameObject environementEnter;
    public GameObject[] environementExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneController.instance.ChangeScreenBox(enterFramePoint.transform.parent.GetComponent<ScreenBox>());
        environementEnter.SetActive(true);
        foreach(GameObject obj in environementExit)
        {
            obj.SetActive(false);
        }
    }
}
