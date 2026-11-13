using UnityEngine;
using System.Collections;

public class Enemyhealth : MonoBehaviour
{
    public int health;
    public AudioClip deathSound;

    Renderer rend;
    bool isDead = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void TakeDamage()
    {
        if (isDead) return;

        health--;

        if (health <= 0)
        {
            isDead = true;
            Gamemanager.instance.EnemyKilled();

            if (deathSound != null)
                AudioSource.PlayClipAtPoint(deathSound, transform.position);

            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = Color.white;
    }
}