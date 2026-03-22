using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private float movementSpeed = 7.5f;
    [SerializeField] private float gravityStrength = 9.81f;
    [SerializeField] private float doublePressWindow = 0.3f;

    [SerializeField] private Camera mainCamera;

    public InputAction rotateAction;
    public InputAction moveAction;

    //movement value
    private float moveVal;

    //Per-key double-press tracking
    private float lastAPressTime = -1f;
    private float lastDPressTime = -1f;

    private Vector3 _lastPos;

    private Vector2 gravityDirection = Vector2.down;

    //Temp fix since singletons don't wanna work
    [SerializeField]
    private GameObject _soundManagerGameObject;
    private SoundManager _soundManager;

    public Vector2 GravityDirection { get => gravityDirection; private set => gravityDirection = value; }
    
    private void Awake()
    {
        _soundManager = _soundManagerGameObject.GetComponent<SoundManager>();
    }

    private SpriteRenderer sprite;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        rotateAction.Enable();
        rotateAction.performed += OnRotate;
        moveAction.Enable();
        moveAction.started += OnMove;
        moveAction.canceled += OnMove;
    }

    void OnDisable()
    {
        rotateAction.performed -= OnRotate;
        rotateAction.Disable();
        moveAction.Disable();
        moveAction.started -= OnMove;
        moveAction.canceled -= OnMove;
    }

    void FixedUpdate()
    {
        //Apply custom gravity every physics step
        rb.AddForce(GravityDirection * gravityStrength, ForceMode2D.Force);

        //set velocity along the "right" axis relative to current orientation
        //movement is on the axis perpendicular to gravity direction
        
        Vector2 moveAxis = new Vector2(transform.right.x, transform.right.y);
        //preserve current velocity
        float gravityVelocity = Vector2.Dot(rb.linearVelocity, GravityDirection.normalized);
        //movement on the perpendicular axis + gravity on the gravity axis
        rb.linearVelocity = moveAxis * moveVal * movementSpeed
                          + GravityDirection.normalized * gravityVelocity;

        //flip sprite based on move axis
        if (moveVal >= 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
         moveVal = ctx.ReadValue<float>();
    }

    /// <summary>
    /// Is called whenever the player moves
    /// </summary>
    /// <param name="ctx"></param>
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
        _soundManager.PlaySound(_soundManager.LevelSounds[4]);

        //rotate the gravity vector around Z
        Quaternion rotation = Quaternion.Euler(0, 0, angleDeg);
        GravityDirection = rotation * GravityDirection;

        //rotate the player sprite
        transform.rotation *= rotation;
        mainCamera.transform.rotation *= rotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //If the player touches spikes destory and create a new player.
        if (collision.gameObject.CompareTag("spike"))
        {
            Death();
        }
    }

    public void Death()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);
    }
}

