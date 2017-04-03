using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : NetworkBehaviour
{

    public float speed;
    private Rigidbody2D rig;
    private Animator anim;
    private Vector2 movement;
    public Vector2 lastMovement;
    private float movHorizontal;
    private float movVertical;

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(1, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(2, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(3, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(4, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(5, true);
    }

    public override void PreStartClient()
    {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(1, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(2, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(3, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(4, true);
        GetComponent<NetworkAnimator>().SetParameterAutoSend(5, true);
    }

    // Use this for initialization
    void Start()
    {
        lastMovement = new Vector2(1, 0);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

     updateMovement();

     moving(movHorizontal , movVertical);
    }

    private void updateMovement()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
         //Move depending on direction player clicks
         movHorizontal = Input.GetAxisRaw("Horizontal");
         movVertical = Input.GetAxisRaw("Vertical");
#else
         //Move where the direction the joystic is pointing
         movHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
         movVertical = CrossPlatformInputManager.GetAxis("Vertical");

#endif

    }

    public void moving(float x, float y)
    {

        bool isWalking = (Mathf.Abs(x) + Mathf.Abs(y)) > 0;

        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            anim.SetFloat("x", x);
            anim.SetFloat("y", y);

            movement = new Vector2(x, y).normalized;
            rig.velocity = movement * speed;

            lastMovement = new Vector2(x, y);

        }

        anim.SetFloat("lastx", lastMovement.x);
        anim.SetFloat("lasty", lastMovement.y);

    }

}
