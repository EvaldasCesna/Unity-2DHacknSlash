using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : NetworkBehaviour
{

    public float speed;
    private Rigidbody2D rig;

    //Animator anim;

    bool isAttacking;

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
    }

    // Use this for initialization
    void Start()
    {


        rig = GetComponent<Rigidbody2D>();
     //   anim = GetComponent<Animator>();
  
}


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        

#if UNITY_STANDALONE || UNITY_WEBPLAYER
         float movHorizontal = Input.GetAxis("Horizontal");
         float movVertical = Input.GetAxis("Vertical");


        Vector2 movement = new Vector2(movHorizontal, movVertical);
        rig.velocity = movement * speed;

      //  isAttacking = Input.GetKeyDown(KeyCode.Space);

#else
        Vector2 movement = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        rig.velocity = movement * speed;

        //    isAttacking = CrossPlatformInputManager.GetButton("Attack");



#endif

        //     if (isAttacking)
        //     {
        //          anim.SetTrigger("Attack");
        //    }
    }

}
