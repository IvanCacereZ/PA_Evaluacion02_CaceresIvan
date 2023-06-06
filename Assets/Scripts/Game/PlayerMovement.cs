using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    [SerializeField] private float speed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Text metrosTXT;
    [SerializeField] private Text vidasTXT;
    [SerializeField] private Text scoreTXT;
    public int player_lives = 3;
    private Vector2 MovementDirection;
    [SerializeField] AudioSource myAudio;
    [SerializeField] AudioClip audioEat;
    [SerializeField] AudioClip audioEspecial;
    [SerializeField] AudioClip audioHurt;
    [SerializeField] BoxCollider2D colliderBox;
    int metros = 0;
    int score = 0;
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        InvokeRepeating("AddOneMetro", 1f, 1f);
    }
    public void UpdateMovementData(Vector2 newMovementDirection)
    {
        MovementDirection = newMovementDirection;
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
        metrosTXT.text = "metros: " + metros;
        vidasTXT.text = "vida: " + player_lives;
        scoreTXT.text = "Score: " + score;
    }
    void MoveThePlayer()
    {
        Vector3 movement = CameraDirection(MovementDirection) * speed * Time.deltaTime;
        myRB.MovePosition(transform.position + movement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Candy")
        {
            myAudio.PlayOneShot(audioEat);
            score = score + other.gameObject.GetComponent<CandyController>().Score;
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this.gameObject);
        }
        else if (other.tag == "Especial")
        {
            myAudio.PlayOneShot(audioEspecial);
            score = score * other.gameObject.GetComponent<CandyController>().Score;
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this.gameObject);
        }
        else if (other.tag == "Enemy")
        {
            transform.position = new Vector2(-5f, 0f);
            StartCoroutine(ActivarColliderTemporalmente(1.0f));
            myAudio.PlayOneShot(audioHurt);
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this.gameObject);
        }
        else
        {
            return;
        }
    }
    Vector2 CameraDirection(Vector2 movementDirection)
    {
        var cameraForward = mainCamera.transform.up;
        var cameraRight = mainCamera.transform.right;

        cameraForward.x = 0f;
        cameraRight.x = 0f;
        cameraForward.z = 0f;
        cameraRight.z = 0f;

        return cameraForward * movementDirection.y + cameraRight * movementDirection.x;
    }
    private void AddOneMetro()
    {
        metros++;
    }
    IEnumerator ActivarColliderTemporalmente(float time)
    {
        colliderBox.enabled = false;
        yield return new WaitForSeconds(time);
        colliderBox.enabled = true;
    }
    public void GetTextLives(string text)
    {
        vidasTXT.text = text;
    }
    public float GetScore()
    {
        return score;
    }
}