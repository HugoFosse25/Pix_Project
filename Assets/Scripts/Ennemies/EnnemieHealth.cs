using UnityEngine;
using System.Collections;

public class EnnemieHealth : MonoBehaviour
{
    public int nbOfMaxLife;

    private int nbOfCurrentLife;

    public float hitTime = 0.2f;

    private Color spColor;

    private void Start()
    {
        spColor = GetComponent<SpriteRenderer>().color;
        nbOfCurrentLife = nbOfMaxLife;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            StartCoroutine(TakeDamage(1));
        }
    }

    public IEnumerator TakeDamage(int nbOfDamage)
    {
        nbOfCurrentLife -= nbOfDamage;
        StartCoroutine(HittingTime());
        if (nbOfCurrentLife <= 0)
        {
            Die();
        }
        yield return null;
    }

    private IEnumerator HittingTime()
    {
        spColor.a = 0f;
        yield return new WaitForSeconds(hitTime);
        spColor.a = 1f;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
