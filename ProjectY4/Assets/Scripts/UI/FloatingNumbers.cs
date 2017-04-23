using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{
    public float movSpeed;
    public int damageNum;
    public Text displayNumber;

    // Update is called once per frame
    void Update()
    {
        displayNumber.CrossFadeAlpha(0.0f, 1.0f, false);
        displayNumber.text = damageNum.ToString();
        transform.position = new Vector3(transform.position.x, transform.position.y + (movSpeed * Time.deltaTime), transform.position.z);

    }
}
