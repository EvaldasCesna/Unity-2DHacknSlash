using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float arrowSpeed;
    //Here is where I would implement Different Skills
    public GameObject shootArrow(int damage, Vector3 rotation, Vector2 direction)
    {
        GameObject arrow = (GameObject)Instantiate(arrowPrefab, transform.position, Quaternion.Euler(rotation));

        arrow.GetComponent<Arrow>().damage = damage;
        arrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;

        return arrow;
    }

}
