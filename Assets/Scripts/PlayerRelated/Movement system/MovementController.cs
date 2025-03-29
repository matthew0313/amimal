using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    public Basic basic;
    public Agile agile;
    public Glide glide;
    public Spin spin;
    public OnTree onTree;

    public Movement current;
    [Header("Button Controlls")][SerializeField] HoldButton Left;
    [SerializeField] HoldButton Right;
    [Header("Statistics")] public bool canMove;
    public bool grounded = false;
    public bool canDisableJump = true;
    public bool antiAirMove = false;
    public bool pressingRight, pressingLeft;
    public bool canControl = true;
    public bool updating = true;
    public bool canJump = true;
    [Header("Common Stats")] public float riseGravity;
    public float fallGravity;
    [Header("Essential Components")] public Animator anim;
    public SpriteRenderer renderer;
    public BoxCollider2D collider;
    public Rigidbody2D rb;
    public HingeJoint2D joint;
    [Header("Map LayerMask")] public LayerMask whatToScan;
    [HideInInspector] public UnityEvent moveRight, moveLeft, fixedRight, fixedLeft;
    private void Awake()
    {
        basic = new Basic(this);
        agile = new Agile(this);
        glide = new Glide(this);
        spin = new Spin(this);
        onTree = new OnTree(this);
        current = basic;
    }
    private void Update()
    {
        if(updating) current.OnUpdate();
        if (!canControl) return;
        if (canMove)
        {
            if (!antiAirMove)
            {
                if (Input.GetKey(KeyCode.D) || Right.buttonPressed)
                {
                    pressingRight = true;
                    moveRight.Invoke();
                    current.MoveRight();
                }
                else pressingRight = false;
                if ((Input.GetKey(KeyCode.A) || Left.buttonPressed) && !pressingRight)
                {
                    pressingLeft = true;
                    moveLeft.Invoke();
                    current.MoveLeft();
                }
                else pressingLeft = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            pressingLeft = false;
            pressingRight = false;
        }
    }
    private void FixedUpdate()
    {
        current.OnFixedUpdate();
        if (!canControl) return;
        if (canMove && !antiAirMove)
        {
            if (Input.GetKey(KeyCode.D) || Right.buttonPressed)
            {
                pressingRight = true;
                fixedRight.Invoke();
                current.FixedMoveRight();
            }
            else pressingRight = false;
            if ((Input.GetKey(KeyCode.A) || Left.buttonPressed) && !pressingRight)
            {
                pressingLeft = true;
                fixedLeft.Invoke();
                current.FixedMoveLeft();
            }
            else pressingLeft = false;
        }
        else
        {
            pressingLeft = false;
            pressingRight = false;
        }
    }
    public void Jump()
    {
        if(canControl&&canMove&&canJump) current.Jump();
    }
    public void Switch(Movement move)
    {
        current.OnExit();
        current = move;
        current.OnSwitch();
    }
    public void Switch(Movement move, Animator changeAnim, SpriteRenderer changeRenderer)
    {
        current.OnExit();
        current = move;
        anim = changeAnim;
        renderer = changeRenderer;
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Moving", pressingRight||pressingLeft);
        current.OnSwitch();
    }
    public void CollisionReload()
    {
        collider.enabled = false;
        collider.enabled = true;
    }
}
