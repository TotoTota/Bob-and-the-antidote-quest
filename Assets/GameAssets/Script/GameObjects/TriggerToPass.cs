using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToPass : MonoBehaviour
{
    public Collider2D objectToPass;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectToPass.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectToPass.isTrigger = false;
    }
}
