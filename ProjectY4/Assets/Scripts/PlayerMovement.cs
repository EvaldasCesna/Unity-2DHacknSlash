using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : NetworkBehaviour
{

    public float speed;
    private Rigidbody2D rig;

    //Animator anim;



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
        //Move depending on direction player clicks
         float movHorizontal = Input.GetAxis("Horizontal");
         float movVertical = Input.GetAxis("Vertical");


        Vector2 movement = new Vector2(movHorizontal, movVertical);
        rig.velocity = movement * speed;

 

#else
        //Move where the direction the joystic is pointing
        Vector2 movement = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        rig.velocity = movement * speed;





#endif


    }

}
