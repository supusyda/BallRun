using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    Vector3 velocity = Vector3.zero;
    [Range(0, 1)]
    public float smoothTime;
    public Vector3 camOffset;
    [Header("Axis Limitation")]
    public Vector2 xLimit;
    public Vector2 yLimit;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = this.target.transform.position + camOffset;
        targetPos = new Vector3(Mathf.Clamp(targetPos.x, this.xLimit.x, this.xLimit.y), Mathf.Clamp(targetPos.y, this.yLimit.x, this.yLimit.y), -8);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref this.velocity, this.smoothTime);
    }
}
