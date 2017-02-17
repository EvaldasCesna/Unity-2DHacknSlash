using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    Camera mycam;
    public Transform playerTransform;
    public int depth = -10;
    public int x = 0;
    public int y = 0;
    // Update is called once per frame
    private void Start()
    {
        mycam = GetComponent<Camera>();
    } 

    void FixedUpdate()
    {
        mycam.orthographicSize = (Screen.height / 25f) / 2f;
        if (playerTransform != null)
        {


            transform.position = Vector3.Lerp(transform.position, playerTransform.position, 0.1f) + new Vector3(x, y, depth);
           // transform.position = playerTransform.position + new Vector3(x, y, depth);
        }
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}