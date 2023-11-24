using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFpsLimiter : MonoBehaviour
{
    public Camera cam;
    public int targetFPS;
    
    private void Update(){
        LimitFPS();
    }
    
    private void LimitFPS(){
        StartCoroutine(Limit());
    }
    
    private IEnumerator Limit(){
        while(true){
            cam.Render();
            Debug.Log("asd");
            
            yield return new WaitForSeconds(1 / targetFPS);
        }
    }    
}
