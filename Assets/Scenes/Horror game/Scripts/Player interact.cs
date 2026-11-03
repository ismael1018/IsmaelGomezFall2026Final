
using UnityEngine;

public class Playerinteract : MonoBehaviour
{
    public float interactRange = 3f;
    public GameObject promptText;

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            SetPrompt(false);
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        IInteractable interactable = null;

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, ~0, QueryTriggerInteraction.Ignore))
            interactable = hit.collider.GetComponent<IInteractable>();

        SetPrompt(interactable != null);

        if (interactable != null && Input.GetKeyDown(KeyCode.E))
            interactable.Interact();
    }

    void SetPrompt(bool show)
    {
        if (promptText != null && promptText.activeSelf != show)
            promptText.SetActive(show);
    }
}
