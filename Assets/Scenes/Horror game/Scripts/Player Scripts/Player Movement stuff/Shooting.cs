using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 100f;
    public AudioClip gunSound;
    public AudioClip emptySound;

    public int ammo = 10;
    public TextMeshProUGUI ammoText;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateAmmoUI();
    }
    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }
    void Shoot()
    {
        if (ammo <= 0)
        {
            if (emptySound != null)
                audioSource.PlayOneShot(emptySound);
            return;
        }

        ammo--;
        UpdateAmmoUI();

        if (gunSound != null)
            audioSource.PlayOneShot(gunSound);

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Enemyhealth enemy = hit.transform.GetComponent<Enemyhealth>();
            if (enemy != null)
                enemy.TakeDamage();
        }
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = "Ammo: " + ammo;
    }
}
    