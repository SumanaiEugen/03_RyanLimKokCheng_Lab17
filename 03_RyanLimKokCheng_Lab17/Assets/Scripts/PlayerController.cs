using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float Speed = 5;
    float jumpForce = 10;

    int HealthCount = 100;
    int coinCount = 0;

    public Text HealthText;
    public Text Cointext;

    Rigidbody2D rb;
    private Animator animator;
    private AudioSource audiosource;

    public AudioClip[] audioClipArray;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        HealthText.GetComponent<Text>().text = "Health : " + HealthCount;
        Cointext.GetComponent<Text>().text = "Coin : " + coinCount;

        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HealthCount -= 10;
            HealthText.GetComponent<Text>().text = "Health : " + HealthCount;

            audiosource.PlayOneShot(audioClipArray[2]);
        }
        if (collision.gameObject.tag == "Coin")
        {
            coinCount += 1;
            Destroy(collision.gameObject);
            Cointext.GetComponent<Text>().text = "Coin : " + coinCount;

            audiosource.PlayOneShot(audioClipArray[0]);
        }
    }

    private void Movement()
    {
        float hVelocity = 0;
        float vVelocity = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hVelocity = -Speed;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));


        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            hVelocity = Speed;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));

            audiosource.PlayOneShot(audioClipArray[1]);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vVelocity = jumpForce;
            animator.SetTrigger("Jumptrigger");

            audiosource.PlayOneShot(audioClipArray[3]);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetFloat("xVelocity", 0);
        }

        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5, 5);
        rb.velocity = new Vector2(hVelocity, rb.velocity.y + vVelocity);
    }
}
