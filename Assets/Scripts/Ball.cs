using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField] private Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds ;
    [SerializeField] float randonFactor = 0.2f;

    //state
    private Vector2 paddleToBallVector;
    private bool hasStarted = false;

    //Cached 
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    private void Start() {
        paddleToBallVector = transform.position - paddle1.transform.position;
        this.myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void ResetBall() {
        this.hasStarted = false;
        LockBallToPadle();

    }
    // Update is called once per frame
    private void Update() {
        if (this.hasStarted == false) {
            LockBallToPadle();
            LaunchBallOnMouseClick();
        }
    }

    private void LaunchBallOnMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            this.hasStarted = true;
            myRigidBody2D.velocity = new Vector2(this.xPush, this.yPush);
        }
    }

    private void LockBallToPadle() {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randonFactor), Random.Range(0f, randonFactor));

        if (hasStarted == true) {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
        
    }
}