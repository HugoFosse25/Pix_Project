using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 12;

    private Vector3 movement;

    private SpriteRenderer playerSpriteRenderer;

    private void Start()
    {
        playerSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();

        if (playerSpriteRenderer.flipX) //If the player orientation is left ajist the movement of the bullet
        {
            movement = Vector3.left;
        }
        else
        {
            movement = Vector3.right;
        }

        StartCoroutine(bulletAutoDestroy());
    }
    private void Update()
    {
            transform.Translate(movement * bulletSpeed * Time.deltaTime);
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            StopCoroutine(bulletAutoDestroy()); //If collision stop autodestroy

            Destroy(gameObject);
        }
    }

    IEnumerator bulletAutoDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
