using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IInteractable
{
    public bool isLocked = false;
    public bool neverOpens = false;
    public bool onlyOpensFromInside = false;

   
    public Transform insidePoint;
    public Transform outsidePoint;

    [Header("Opening")]
    public Transform hinge;
    public float openAngle = 90f;
    public float openTime = 0.8f;
    public AudioClip openSound;
    public AudioClip closeSound;
    public float closeClearance = 1.2f;   

    bool isOpen = false;
    bool moving = false;
    Quaternion closedRotation;
    Vector3 closedCenter;

    void Start()
    {
        Transform t = hinge != null ? hinge : transform;
        closedRotation = t.rotation;

        Collider col = GetComponent<Collider>();
        closedCenter = col != null ? col.bounds.center : transform.position;
    }

    public void Interact()
    {
        if (moving) return;

        if (isOpen)
        {
            TryClose();
            return;
        }

        if (neverOpens)
        {
            Debug.Log("It won't open.");
            return;
        }

        if (onlyOpensFromInside && !PlayerIsInside())
        {
            Debug.Log("This door won't open from this side.");
            return;
        }

        if (isLocked && !PlayerInventory.Instance.hasKey)
        {
            Debug.Log("The door is locked.");
            return;
        }

        isLocked = false;
        onlyOpensFromInside = false;

        if (openSound != null)
            AudioSource.PlayClipAtPoint(openSound, transform.position);

        StartCoroutine(Swing(true));
    }

    void TryClose()
    {
        Vector3 p = PlayerInventory.Instance.transform.position;
        Vector3 doorway = new Vector3(closedCenter.x, p.y, closedCenter.z);

        if (Vector3.Distance(p, doorway) < closeClearance)
        {
            Debug.Log("Something is in the way.");
            return;
        }

        if (closeSound != null)
            AudioSource.PlayClipAtPoint(closeSound, transform.position);

        StartCoroutine(Swing(false));
    }

    IEnumerator Swing(bool open)
    {
        moving = true;

        foreach (Collider c in GetComponents<Collider>())
            c.enabled = false;

        Transform t = hinge != null ? hinge : transform;
        Quaternion start = t.rotation;
        Quaternion end = open
            ? Quaternion.AngleAxis(openAngle, Vector3.up) * closedRotation
            : closedRotation;

        float time = 0f;
        while (time < openTime)
        {
            time += Time.deltaTime;
            t.rotation = Quaternion.Slerp(start, end, time / openTime);
            yield return null;
        }
        t.rotation = end;

        foreach (Collider c in GetComponents<Collider>())
            c.enabled = true;

        isOpen = open;
        moving = false;
    }

    bool PlayerIsInside()
    {
        if (insidePoint == null || outsidePoint == null) return true;

        Vector3 player = PlayerInventory.Instance.transform.position;
        return Vector3.Distance(player, insidePoint.position) < Vector3.Distance(player, outsidePoint.position);
    }
}