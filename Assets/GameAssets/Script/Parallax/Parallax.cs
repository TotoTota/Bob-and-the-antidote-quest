using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subject;
    private float lenghtX;
    private float lenghtY;

    Vector2 startPosition;
    float startZ;

    float distanceFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    public void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
        lenghtX = GetComponent<SpriteRenderer>().bounds.size.x;
        lenghtY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public void Update()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxFactor));
        float tempY = (cam.transform.position.y * (1 - parallaxFactor));
        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, transform.position.y, startZ);

        if (tempX > startPosition.x + lenghtX) startPosition.x += lenghtX;
        else if (tempX < startPosition.x - lenghtX) startPosition.x -= lenghtX;

        if (tempY > startPosition.y + lenghtY) startPosition.y += lenghtY;
        else if (tempY < startPosition.y - lenghtY) startPosition.y -= lenghtY;
    }
}
