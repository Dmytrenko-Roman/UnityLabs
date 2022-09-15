using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    [SerializeField]
    private int _speed = 20;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask platformLayerMask;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    public bool IsGrounded() 
    {
        RaycastHit2D rh2d = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
        return rh2d.collider != null;
    }

    void Jump()
    {
        rb2d.velocity = Vector2.up * jumpForce;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(
            moveX * _speed, rb2d.velocity.y
        );


        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            Jump();
        }
    }
}
