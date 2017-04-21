using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    Camera mycam;
    public Transform playerTransform;
    public int depth = 0;
    public int x = 0;
    public int y = 0;

    public BoxCollider2D camBounds;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private float halfHeight;
    private float halfWidth;

    private static bool cameraExists;

    // Update is called once per frame
    private void Start()
    {
        mycam = GetComponent<Camera>();

        minBounds = camBounds.bounds.min;
        maxBounds = camBounds.bounds.max;

        if(!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

      

    } 

    void FixedUpdate()
    {
        mycam.orthographicSize = (Screen.height / 25f) / 2f;

        halfHeight = mycam.orthographicSize;
        halfWidth = (halfHeight * Screen.width) / Screen.height;
        if (playerTransform != null)
        {
            //If Camera too far away dont smoothen the movement
            if (playerTransform.position.x < transform.position.x + 10)
            {
                transform.position = Vector3.Lerp(transform.position, playerTransform.position, 0.1f) + new Vector3(x, y, depth);
            }
            else
            {
                transform.position = playerTransform.position + new Vector3(x, y, depth);
            }
           // transform.position = playerTransform.position + new Vector3(x, y, depth);
        }


        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }

    public void SetBounds(BoxCollider2D bounds)
    {
        camBounds = bounds;

        minBounds = camBounds.bounds.min;
        maxBounds = camBounds.bounds.max;
    }

}