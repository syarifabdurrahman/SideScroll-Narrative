using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private float length;
    private float startPos;
    [SerializeField]private GameObject cam;

    [SerializeField] private float parallaxEffectSpeed;

    private void Start()
    {
        cam = GameObject.Find("CM vcam");
        startPos = transform.position.x;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffectSpeed));
        float distance = (cam.transform.position.x * parallaxEffectSpeed);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp >= startPos + length)
        {
            startPos += length;
        }

        if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}