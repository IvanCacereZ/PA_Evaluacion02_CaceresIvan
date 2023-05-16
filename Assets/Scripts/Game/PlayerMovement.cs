using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    private Rigidbody2D myRB;
    [SerializeField] private float speed;
    [SerializeField] private Camera mainCamera;
    private float limitSuperior;
    private float limitInferior;
    public int player_lives = 4;
    private Vector2 MovementDirection;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    public void UpdateMovementData(Vector2 newMovementDirection)
    {
        MovementDirection = newMovementDirection;
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
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
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this);
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
}
