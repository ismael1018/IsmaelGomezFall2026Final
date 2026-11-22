using UnityEngine;

public class RoomZone : MonoBehaviour
{
    public string displayName;   

    string RoomName => string.IsNullOrEmpty(displayName) ? gameObject.name : displayName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && RoomNameUI.Instance != null)
            RoomNameUI.Instance.Show(RoomName);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && RoomNameUI.Instance != null)
            RoomNameUI.Instance.Clear(RoomName);
    }
}