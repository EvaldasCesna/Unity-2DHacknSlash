using UnityEngine;
using UnityEngine.Networking;


public class PlayerMovement : NetworkBehaviour {

    public float speed;
    private Rigidbody2D rig;
 

	// Use this for initialization
	void Start ()
    {
        rig = GetComponent<Rigidbody2D>();
	}

  
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

         float movHorizontal = Input.GetAxis("Horizontal");
         float movVertical = Input.GetAxis("Vertical");


        Vector2 movement = new Vector2(movHorizontal, movVertical);
        rig.velocity = movement * speed;

    }

}
