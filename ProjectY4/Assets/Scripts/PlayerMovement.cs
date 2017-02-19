using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : NetworkBehaviour
{

    public float speed;
    private Rigidbody2D rig;
    private Animator anim;
    private Vector2 movement;
    //Animator anim;

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
  
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        //Move depending on direction player clicks
        float movHorizontal = Input.GetAxisRaw("Horizontal");
        float movVertical = Input.GetAxisRaw("Vertical");
#else
              //Move where the direction the joystic is pointing
        float movHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float movVertical = CrossPlatformInputManager.GetAxis("Vertical");

#endif
        bool isWalking = (Mathf.Abs(movHorizontal) + Mathf.Abs(movVertical)) > 0;

        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            anim.SetFloat("x", movHorizontal);
            anim.SetFloat("y", movVertical);
          
             movement = new Vector2(movHorizontal, movVertical).normalized;
            rig.velocity = movement * speed;

        }
        anim.SetFloat("lastx", movement.x);
        anim.SetFloat("lasty", movement.y);

    }

}
