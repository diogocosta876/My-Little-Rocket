using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Vector3 move;
    Rigidbody rigidBody;
    AudioSource audiosource;
    public float RotationSpeedMultiplier;
    public float ThrustingSpeedMultiplier;
    bool hasCollided;

    // Start is called before the first frame update
    void Start()
    {
        hasCollided = false;
        move = new Vector3(0, 1f, 0);
        rigidBody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "FinishedLevel":
                print("You Won");
                SceneManager.LoadScene(1);
                break;
            case "Fuel":
                print("Refueled");
                break;
            default:
                print("You are Dead");  // todo kill player
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void Rotate()
    {

        rigidBody.angularVelocity = Vector3.zero;      // nao esquecer desligar quando o collider bate
        Vector3 tmp = transform.localEulerAngles;      //
        tmp.x = 0; tmp.y = 0;                          // bloquear rotação eixos X e Y
        transform.localEulerAngles = tmp;              //

        float RotationSpeed = Time.deltaTime * RotationSpeedMultiplier;

        if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.A)))
        {
            print("You cant rotate both ways");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,0, RotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -RotationSpeed);
        }
    }

    private void Thrust()
    {
        float ThrustingSpeed = Time.deltaTime * 10 * ThrustingSpeedMultiplier;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(0, ThrustingSpeed, 0);
            if (audiosource.isPlaying == false) //So it doesn't repeat
            {
                audiosource.Play();
            }
        }
        else
        {
            audiosource.Stop();
        }
    }
}
