using UnityEngine;
using System.Collections;
using System;

public class WallBehaviour : MonoBehaviour
{
    public GameObject[] enemies;
    public Animator wall;
    public Animator camAnim;
    public static bool hasPlayed;
    public static bool playForStop = true;
    public GameObject player;

    private void Start()
    {

    }

    void Update()
    {
        if(enemies.Length == 0 && hasPlayed == false)
        {
            playForStop = false;
            wall.SetBool("close?", true);
            StartCoroutine(PlayCutscene());
            hasPlayed = true;
        }
    }

    public void RemoveItem<T>(ref T[] arr, int index)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            arr[i] = arr[i + 1];
        }

        Array.Resize(ref arr, arr.Length - 1);
    }

    IEnumerator PlayCutscene()
    {
        CameraController.instance.player = gameObject;
        yield return new WaitForSeconds(1f);
        CameraController.instance.player = player;
        playForStop = true;
    }
}
