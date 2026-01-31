using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player component references")]
    [SerializeField] Rigidbody2D rp;

    [Header("Player setting")]
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    private float horizontal;

    private void FixedUpdate()
    {
        rp.linearVelocity = new Vector2(horizontal * speed, rp.linearVelocity.y);
    }

    #region PLAYER_CONTROLS

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void jump(InputAction.CallbackContext context)
    {
        rp.linearVelocity = new Vector2(rp.linearVelocity.x, jumpingPower);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    #endregion

}
