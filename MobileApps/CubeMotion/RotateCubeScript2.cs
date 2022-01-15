using UnityEngine;

public class RotateCubeScript2 : MonoBehaviour
{

    float speed = 50f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //transform.RotateAround(this.transform.position, new Vector3(1, 0, 0), speed * Time.deltaTime);

        if (Input.GetKey("w"))
        {
            //transform.Rotate(new Vector3(35, 0, 0));
            transform.RotateAround(this.transform.position, new Vector3(1, 0, 0), (3 * speed) * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.RotateAround(this.transform.position, new Vector3(-1, 0, 0), (3 * speed) * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.RotateAround(this.transform.position, new Vector3(0, 1, 0), (3 * speed) * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.RotateAround(this.transform.position, new Vector3(0, -1, 0), (3 * speed) * Time.deltaTime);
        }
    }


    void Update()
    {

    }
}