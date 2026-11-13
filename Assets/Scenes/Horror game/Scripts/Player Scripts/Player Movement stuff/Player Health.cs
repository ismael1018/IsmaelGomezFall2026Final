using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int damage;
    public AudioClip hurtSound;
    public AudioSource hurtAudioSource;

    public Slider healthSlider;
    public GameObject deathScreen;

    private int maxHealth;
    private bool isDead = false;

    void Start()
    {
        maxHealth = health;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        if (deathScreen != null)
            deathScreen.SetActive(false);
    }
    void OnTriggerEnter(Collider col)
    {
        if (isDead) return;

        if (col.gameObject.tag == "Enemy")
        {
            health -= damage;
            hurtAudioSource.PlayOneShot(hurtSound);

            if (healthSlider != null)
                healthSlider.value = health;

            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("You died");

        if (deathScreen != null)
            deathScreen.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        Debug.Log("Restart clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHealth);

        if (healthSlider != null)
            healthSlider.value = health;
    }
}
