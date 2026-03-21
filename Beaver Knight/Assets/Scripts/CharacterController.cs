using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float movementFactor = 7.5f;
    //rotation stuff
    public InputAction rotateAction;
    private float lastPressTime;
    private float doublePressTimer = 0.3f;

    //gravity sim
    private Vector2 gravity = new Vector2(0f, -3);
    private Quaternion gravityDirection;


    InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        gravityDirection = transform.rotation;
    }

    void OnEnable()
    {
        rotateAction.Enable();
        rotateAction.performed += OnRotate;
    }

    // Update is called once per frame
    void Update()
    {
        //gravity
        if (gravityDirection.z == 0)
        {
            rb.AddForce(gravity);
        }
        else
        {                 
            rb.AddForce(gravityDirection.eulerAngles * gravity);
        }

        Vector2 moveVal = moveAction.ReadValue<Vector2>();

        rb.AddForceX(moveVal.x * movementFactor * Time.deltaTime, ForceMode2D.Impulse);
    }

    //using this pretty much to only simulate gravity for the object
    void FixedUpdate()
    {
    }

    void OnRotate(InputAction.CallbackContext ctx)
    {
        if (Time.time - lastPressTime < doublePressTimer)
        {
            if (ctx.control is KeyControl keyControl)
            {
                if (keyControl.keyCode == Key.D)
                {
                    gravityDirection = quaternion.Euler(0, 0, 90);
                    transform.rotation = Quaternion.FromToRotation(transform.up, -gravityDirection.eulerAngles) * transform.rotation;
                }
                else if (keyControl.keyCode == Key.A)
                {
                    gravityDirection = quaternion.Euler(0, 0, -90);
                    transform.rotation = Quaternion.FromToRotation(transform.up, -gravityDirection.eulerAngles) * transform.rotation;

                }
            }
        }
        lastPressTime = Time.time;
    }
}
