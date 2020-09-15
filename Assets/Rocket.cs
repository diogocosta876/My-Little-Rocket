using UnityEngine;

public class Rocket : MonoBehaviour
{
    Vector3 move;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(0, 1f, 0);
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("Thrusting");
            rigidBody.AddRelativeForce(0,1,0);
        }
        if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.A)))
        {
            print("You cant rotate both ways");

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,0,0.7f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0,0,-0.7f);

        }
    }
}
