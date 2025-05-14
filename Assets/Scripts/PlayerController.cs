using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // Prędkość poruszania się gracza
    public Rigidbody2D theRB; // Komponent Rigidbody2D gracza
    public float jumpForce; // Siła skoku
    public float runSpeed; // Prędkość biegu
    private float activeSpeed; // Aktywna prędkość gracza (zmienia się w zależności od biegu)

    private bool isGrounded; // Sprawdza, czy gracz jest na ziemi
    public Transform groundCheckPoint; // Punkt sprawdzania, czy gracz dotyka ziemi
    public float groundCheckRadius; // Promień sprawdzania w celu wykrycia ziemi
    public LayerMask whatIsGround; // Warstwa oznaczająca ziemię
    private bool canDoubleJump; // Czy gracz może wykonać podwójny skok

    public Animator anim; // Animator do obsługi animacji postaci

    public float knockbackLength; // Długość efektu odrzutu po otrzymaniu obrażeń
    public float knockbackSpeed; // Prędkość odrzutu
    private float knockbackCounter; // Licznik odrzutu

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f)
        {
            // Sprawdzanie, czy gracz jest na ziemi
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            if (knockbackCounter <= 0)
            {
                // Ustawienie aktywnej prędkości (w zależności od biegu)
                activeSpeed = moveSpeed;
                if (Input.GetKey(KeyCode.LeftShift)) 
                {
                    activeSpeed = runSpeed; // Bieg, gdy wciśnięty jest shift
                }

                // Ruch gracza (lewo/prawo)
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y);

                // Skok (zwykły lub podwójny)
                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded == true)
                    {
                        Jump(); // Zwykły skok
                        canDoubleJump = true; // Możliwość podwójnego skoku
                        anim.SetBool("isDoubleJumping", false);
                    }
                    else
                    {
                        if (canDoubleJump == true) // Podwójny skok, gdy gracz jest w powietrzu
                        {
                            Jump(); 
                            canDoubleJump = false;
                            anim.SetTrigger("doDoubleJump"); // Animacja podwójnego skoku
                        }
                    }
                }

                // Obrót postaci w zależności od kierunku ruchu
                if (theRB.velocity.x > 0)
                {
                    transform.localScale = Vector3.one; // Obrót w prawo
                }

                if (theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f); // Obrót w lewo
                }
            }
            else
            {
                // Obsługa odrzutu (po otrzymaniu obrażeń)
                knockbackCounter -= Time.deltaTime;
                theRB.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, theRB.velocity.y);
            }

            // Obsługa animacji postaci (ruchu i skoku)
            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            anim.SetBool("isGrounded", isGrounded);
            anim.SetFloat("ySpeed", theRB.velocity.y);
        }
    }

    // Funkcja skoku
    public void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        AudioManager.instance.PlaySFXPitched(14); // Dźwięk skoku
    }

    // Funkcja odrzutu (np. po otrzymaniu obrażeń)
    public void KnockBack()
    {
        theRB.velocity = new Vector2(0f, jumpForce * .5f);
        anim.SetTrigger("isKnockingBack"); // Animacja odrzutu
        knockbackCounter = knockbackLength; // Ustawienie licznika odrzutu
    }

    // Funkcja do odbicia się (np. po uderzeniu w twardą powierzchnię)
    public void BouncePlayer(float bounceForce)
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce); // Ustawienie siły odbicia
        canDoubleJump = true; // Gracz może wykonać podwójny skok po odbiciu
        anim.SetBool("isGrounded", true); // Ustawienie stanu "na ziemi"
    }
}
