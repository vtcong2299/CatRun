using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet = new Vector3(0, 4, -3);
    public float speedCamera = 3;
    void Update()
    {
        if (!GameManager.instance.isStart)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position,new Vector3(0, 0, target.position.z) + offSet,Time.deltaTime*speedCamera);
        LeanTween.rotate(transform.gameObject, new Vector3(22, 0, 0), 1f);
    }
}
