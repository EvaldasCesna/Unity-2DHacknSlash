using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public int depth = -20;
    public int x = 0;
    public int y = 0;
    // Update is called once per frame
    void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + new Vector3(x, y, depth);
        }
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}