using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 7.5f;
    [SerializeField] private float gravityStrength = 9.81f;
    [SerializeField] private float doublePressWindow = 0.3f;

    [SerializeField] private Camera mainCamera;

    public InputAction rotateAction;
    private InputAction moveAction;

    //Per-key double-press tracking
    private float lastAPressTime = -1f;
    private float lastDPressTime = -1f;

    private Vector2 gravityDirection = Vector2.down;

    private SpriteRenderer sprite;

    public Vector2 GravityDirection { get => gravityDirection; private set => gravityDirection = value; }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        rotateAction.Enable();
        rotateAction.performed += OnRotate;
    }

    void OnDisable()
    {
        rotateAction.performed -= OnRotate;
        rotateAction.Disable();
    }

    void FixedUpdate()
    {
        //Apply custom gravity every physics step
        rb.AddForce(GravityDirection * gravityStrength, ForceMode2D.Force);

        //set velocity along the "right" axis relative to current orientation
        //movement is on the axis perpendicular to gravity direction
        Vector2 moveVal = moveAction.ReadValue<Vector2>();
        Vector2 moveAxis = new Vector2(transform.right.x, transform.right.y);
        //preserve current velocity
        float gravityVelocity = Vector2.Dot(rb.linearVelocity, GravityDirection.normalized);
        //movement on the perpendicular axis + gravity on the gravity axis
        rb.linearVelocity = moveAxis * moveVal.x * movementSpeed
                          + GravityDirection.normalized * gravityVelocity;

        //flip sprite based on move axis
        if (moveVal.x >= 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }

    void OnRotate(InputAction.CallbackContext ctx)
    {
        if (ctx.control is KeyControl keyControl)
        {
            if (keyControl.keyCode == Key.D)
            {
                if (Time.time - lastDPressTime < doublePressWindow)
                {
                    RotateGravity(90f);
                    lastDPressTime = -1f; //reset so a third press doesn't trigger again
                }
                else
                {
                    lastDPressTime = Time.time;
                }
            }
            else if (keyControl.keyCode == Key.A)
            {
                if (Time.time - lastAPressTime < doublePressWindow)
                {
                    RotateGravity(-90f);
                    lastAPressTime = -1f;
                }
                else
                {
                    lastAPressTime = Time.time;
                }
            }
        }
    }

    private void RotateGravity(float angleDeg)
    {
        //rotate the gravity vector around Z
        Quaternion rotation = Quaternion.Euler(0, 0, angleDeg);
        GravityDirection = rotation * GravityDirection;

        //rotate the player sprite
        transform.rotation *= rotation;
        mainCamera.transform.rotation *= rotation;
    }
}