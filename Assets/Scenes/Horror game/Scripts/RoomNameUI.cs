using UnityEngine;
using TMPro;

public class RoomNameUI : MonoBehaviour
{
    public static RoomNameUI Instance;
    TextMeshProUGUI text;
    string current = "";

    void Awake()
    {
        Instance = this;
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    public void Show(string roomName)
    {
        current = roomName;
        text.text = roomName;
    }

    public void Clear(string roomName)
    {
        if (current != roomName) return;   
        current = "";
        text.text = "";
    }
}