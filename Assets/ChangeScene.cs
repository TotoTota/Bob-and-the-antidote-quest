using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public Animator transitionAnimator;
    public int sceneID;
    public float transitionTime;
    
    // Settings 🛠️
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadLevel(sceneID));
    }

    IEnumerator LoadLevel(int sceneID)
    {
        transitionAnimator.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneID);
    }
}
