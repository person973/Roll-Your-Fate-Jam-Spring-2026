using UnityEngine;

public class MovingBlockMoveSideToSide : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float gravityStrength = 9.81f;

    [SerializeField] CharacterController characterController;

    void FixedUpdate()
    {
        //Apply custom gravity every physics step
        rb.AddForce(characterController.gravityDirection * gravityStrength, ForceMode2D.Force);

        //preserve current velocity
        float gravityVelocity = Vector2.Dot(rb.linearVelocity, characterController.gravityDirection.normalized);
    }
}
