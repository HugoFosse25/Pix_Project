using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private int nbOfMaxLife = 3;
    private int nbOfCurrentLife = 3;

    private float invincibilityTime = 3f;

    private bool isInvincible = false;

    private SpriteRenderer graphics;
    private Rigidbody2D rb;

    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    private void Start()
    {
        graphics = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            HealPlayer(1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int nbOfLife)
    {
        if (!isInvincible)
        {
            nbOfCurrentLife -= nbOfLife;

            if (nbOfCurrentLife <= 0)    //If player have 0 life
            {
                Die();
            }

            rb.velocity = Vector3.zero; // to upgrade

            isInvincible = true;

            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());

            HealthSystem.instance.RefreshUI(nbOfCurrentLife);
        }
        
    }

    public void SetMaxLife()
    {
        nbOfCurrentLife = nbOfMaxLife;
    }

    public void HealPlayer(int nbOfLife)
    {
        nbOfCurrentLife += nbOfLife;

        if (nbOfCurrentLife > nbOfMaxLife)
        {
            nbOfCurrentLife = nbOfMaxLife;
        }

        HealthSystem.instance.RefreshUI(nbOfCurrentLife);
    }

    private void Die()
    {
        Destroy(gameObject);
        Debug.Log("He's dead");
    }

    private IEnumerator InvicibilityFlash()  //Coroutine 
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HandleInvicibilityDelay()    //Coroutine
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }
}
