using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI lpText;
    public GameObject winTextObject;
    public GameObject LoseTextObject;

    private Rigidbody rb;
    private int count;
    private int lifepoints;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        rb = GetComponent<Rigidbody>();
        lifepoints = 3;

        SetCountText();
        winTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 21)
        {
            winTextObject.SetActive(true);
        }

        lpText.text = "Life Points: " + lifepoints.ToString();
        if (lifepoints == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    //pick up 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        //lose life points
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lifepoints = lifepoints - 1;

            SetCountText();
        }

        //teleport player
        if (count == 13)
        {
            transform.position = new Vector3(50.0f, 0.0f, 0.0f);
        }
    }
}
