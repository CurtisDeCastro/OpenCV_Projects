using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        transform.RotateAround(this.transform.position, new Vector3(1, 0, 0), speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {


    }

    // public void Rotate()
    // {
    //     print("ROTATING!!!");
    //     this.transform.parent.parent.RotateAround(this.transform.parent.parent.position, new Vector3(1, 0, 0), speed * Time.deltaTime);
    // }
}
