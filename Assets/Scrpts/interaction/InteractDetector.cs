using UnityEngine;
using UnityEngine.InputSystem;

public class InteractDetector : MonoBehaviour
{

    public IInteraction interactionInRange = null;
    public GameObject interactionIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactionInRange?.Interact();
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteraction interaction) && interaction.CanInteract())
        {
            interactionInRange = interaction;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteraction interaction) && interaction == interactionInRange)
        {
            interactionInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
