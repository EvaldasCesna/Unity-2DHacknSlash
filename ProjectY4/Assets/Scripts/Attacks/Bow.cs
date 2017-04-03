using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bow : NetworkBehaviour
{
    public float speed;
    public GameObject arrow;
    public PlayerAttack pa;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
    //[Command]
    //public void CmdShoot()
    //{
    //    pa = GetComponentInParent<PlayerAttack>();
    //    float angle = Mathf.Atan2(pa.lastMovement().y, pa.lastMovement().x) * Mathf.Rad2Deg;
    //    var clone = (GameObject)Instantiate(arrow, transform.position, Quaternion.Euler(new Vector3(pa.lastMovement().y, pa.lastMovement().x,(angle-90))));
    //  //  clone.GetComponent<Arrow>().pa = pa;
    //    clone.GetComponent<Rigidbody2D>().velocity = pa.lastMovement().normalized * speed;
    //    NetworkServer.Spawn(clone);
    //}

}
