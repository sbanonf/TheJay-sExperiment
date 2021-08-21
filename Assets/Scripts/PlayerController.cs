using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variables del personaje")]
    public float longIdleTime = 5f;
    public float speed = 2.5f;
    public float jumpforce = 2.5f;
    public float extra = 5f;

    [Header("Checkers")]
    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _boxcollider2d;
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
        _boxcollider2d = GetComponent<CapsuleCollider2D>();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0f && derecha == true)
        {
            _animator.SetBool("Caminando", true);
            Flip();
        }
        else if (horizontal > 0f && derecha == false)
        {
            Flip();
            _animator.SetBool("Caminando", true);
        }
        else {
            _animator.SetBool("Caminando", horizontal != 0 ? true : false);
        }
        
        _movimiento = new Vector2(horizontal, 0f);
        float horizontalVel = _movimiento.normalized.x * speed;
        _rigidbody2D.velocity = new Vector2(horizontalVel, _rigidbody2D.velocity.y);


        //seteo el movimiento
        _rigidbody2D.AddForce(_movimiento);
        //hago q se mueva.

        //seteo la velocidad

        bool grounded = Suelo();
        if (Input.GetButton("Jump") && grounded){
            _animator.SetBool("NotGrounded", true);
            Jump();
        }
    }
    private bool Suelo() {
       RaycastHit2D raycast = Physics2D.Raycast(_boxcollider2d.bounds.center, Vector2.down, _boxcollider2d.bounds.extents.y+extra,ground);
        Color raycolor;
        if (raycast.collider != null)
        {
            _animator.SetBool("NotGrounded", false);
            Debug.Log(raycast.collider != null);
            raycolor = Color.green;
        }
        else {
            _animator.SetBool("NotGrounded", true);
            raycolor = Color.red;
        }
        Debug.DrawRay(_boxcollider2d.bounds.center, Vector2.down * (_boxcollider2d.bounds.extents.y + extra), raycolor);
        return raycast.collider != null;
    }
    private void FixedUpdate()
    {
       
    }
    private void LateUpdate()
    {
        essuelo = Physics2D.OverlapCircle(groundCheck.position, groundcheckRadius, ground);
        
    }

    private void Flip()
    {
        derecha = !derecha;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawSphere(groundCheck.position, groundcheckRadius);
        
    }
    private void Jump() 
    {
        Gizmos.color = new Color(0, 1, 0);
        _rigidbody2D.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
    }
}