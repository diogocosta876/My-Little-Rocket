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

    enum State { Alive, dying, Progressing}
    State state = State.Alive;

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
        if (state != State.dying)
        {
            Thrust();
            Rotate();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive){ return;}   //ignore collisions when dead

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "FinishedLevel":
                state = State.Progressing;
                print("You Won");
                Invoke("LoadNextLevel", 1f);
                break;
            case "Fuel":
                print("Refueled");
                break;
            default:
                state = State.dying;
                print("You are Dead");
                Invoke("LoadFirstLevel", 1f);
                audiosource.Stop();
                break;
        }
}

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Rotate()
    {

        rigidBody.angularVelocity = Vector3.zero;      // nao esquecer desligar quando o collider bate
        Vector3 tmp = transform.localEulerAngles;      //
        tmp.x = 0; tmp.y = 0;                          // bloquear rotação eixos X e Y
        transform.localEulerAngles = tmp;              //

        float RotationSpeed = Time.deltaTime * RotationSpeedMultiplier;

        if (state != State.dying) {
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
