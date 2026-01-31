using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player component references")]
    [SerializeField] Rigidbody2D rp;

    [Header("Player setting")]
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;
    [SerializeField] Animator anim;

    [Header("Sprite Settings")]
    [SerializeField] SpriteRenderer sprite; // ลาก Sprite Renderer มาใส่ในช่องนี้

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    bool facingRight = true;

    private float horizontal;

    private void FixedUpdate()
    {
        rp.linearVelocity = new Vector2(horizontal * speed, rp.linearVelocity.y);

        // --- ใช้ SetBool เพื่อเช็คว่ากำลังเดินอยู่หรือไม่ ---
        if (anim != null)
        {
            // ถ้า horizontal ไม่เป็น 0 แสดงว่ามีการกดปุ่มเดิน
            bool walking = horizontal != 0;
            anim.SetBool("isWalking", walking);
        }
        // -------------------------------------------

        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            Flip();
        }
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

    void Flip()
    {
        facingRight = !facingRight;

        if (sprite != null)
        {
            // ถ้า facingRight เป็น true (หันขวา) -> flipX ต้องเป็น false
            // ถ้า facingRight เป็น false (หันซ้าย) -> flipX ต้องเป็น true
            sprite.flipX = !facingRight;
        }
    }


}
