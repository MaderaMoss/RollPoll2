using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Basic movement and jump
    private float horizontal;
    private float speed = 3f;
    private float originalspeed = 3f;
    private float jumpPower = 20f;
    private bool isFacingRight = true;
    private bool isRunning;
    public Image koniec;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    private bool isGrounded;

    //system kurzu
    private bool canCollect = false;
    private GameObject kurz;
    private GameObject kurzsmall;
    private Animator kurzAnim;
    int kurzCount;
    public Healthbar healthbar;


    private void Start()
    {
        koniec.gameObject.SetActive(false);
        kurzCount = 0;
        canCollect = true;
        healthbar.SetMaxHealth(405);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("big_kurz") && !isRunning && canCollect)
        {
            kurz = other.gameObject;
            kurzAnim = other.GetComponent<Animator>();
            StartCoroutine(zbieranie());
        }

        if (other.CompareTag("small_kurz"))
        {
            kurzsmall = other.gameObject;
            kurzAnim = other.GetComponent<Animator>();
            StartCoroutine(zbieranieSmall());
        }
    }

    void Update()
    {
        healthbar.SetHealth(kurzCount);

        if (kurzCount == 405)
        {
            koniec.gameObject.SetActive(true);
            speed = 0f;
        }
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer))
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

       
        //Floaty
        float yVelocity = rb.linearVelocity.y;
        float roundedVelocityY = Mathf.Round(yVelocity * 1f) / 1f;
        anim.SetFloat("yVelocity", roundedVelocityY);
        float xVelocity = rb.linearVelocity.x;
        float roundedVelocityX = Mathf.Round(xVelocity * 1f) / 1f;
        anim.SetFloat("xVelocity", roundedVelocityX);
        //
        if (roundedVelocityX > 0.1f || roundedVelocityX < -0.1f)
        {
            isRunning = true;
            anim.SetBool("isRunning", true);
        }
        else
        {
            isRunning = false;
            anim.SetBool("isRunning", false);
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        if (Input.GetButtonDown("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (roundedVelocityX < 0.1f)
        {
            Flip();
        }

    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }


    private IEnumerator zbieranie()
    {
        anim.SetBool("jeto", true);
        canCollect = false;
        speed = 0f;
        kurzCount += 2;
        yield return new WaitForSeconds(1);
        kurzCount += 2;
        kurzAnim.SetBool("zbiera", true);
        yield return new WaitForSeconds(1);
        kurzCount += 2;
        yield return new WaitForSeconds(1);
        kurzCount += 2;
        yield return new WaitForSeconds(1);
        kurzAnim.SetBool("zbiera", false);
        speed = originalspeed;
        Destroy(kurz);
        kurzCount += 2;
        canCollect = true;
        anim.SetBool("jeto", false);
    }

    private IEnumerator zbieranieSmall()
    {
        kurzsmall.GetComponent<Renderer>().enabled = false;
        kurzsmall.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        kurzCount += 1;

    }
}
