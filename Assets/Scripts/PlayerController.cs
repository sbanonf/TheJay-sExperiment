using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float longIdleTime = 5f;
    public float speed = 2.5f;
    public float jumpforce = 2.5f;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public Transform groundCheck; //desde donde es el chequeo
    public LayerMask ground; //Q layer esta.
    public float groundcheckRadius; // Como es de grande el chequeo

    private float longidletimer;
    private bool derecha = true;
    private Vector2 _movimiento;
    public bool essuelo; //Estas en el suelo?
    private bool Atacando;
    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        _movimiento = new Vector2(horizontal, 0f);
        _rigidbody2D.AddForce(_movimiento);
        float horizontalVel = _movimiento.normalized.x * speed;
        _rigidbody2D.velocity = new Vector2(horizontalVel, _rigidbody2D.velocity.y);
        if (Input.GetButton("Jump")){
            _rigidbody2D.AddForce(Vector2.up * jumpforce,ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {
       
    }
    private void LateUpdate()
    {
        
    }

    private void Flip()
    {
        


    }

}