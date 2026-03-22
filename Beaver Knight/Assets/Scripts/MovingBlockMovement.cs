using UnityEngine;

public class MovingBlockMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float gravityStrength = 9.81f;

    [SerializeField] CharacterController characterController;

    void FixedUpdate()
    {
        //Apply custom gravity every physics step
        rb.AddForce(characterController.GravityDirection * gravityStrength, ForceMode2D.Force);

        //preserve current velocity
        float gravityVelocity = Vector2.Dot(rb.linearVelocity, characterController.GravityDirection.normalized);
    }
}
