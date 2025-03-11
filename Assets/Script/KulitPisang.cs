using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulitPisang : MonoBehaviour
{

    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tr.position -= new Vector3(0f, 0.12f, 0f);

        if (tr.position.y < -7f) Destroy(transform.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "tong")
        {
            Destroy(this.gameObject);
        }
    }

}
