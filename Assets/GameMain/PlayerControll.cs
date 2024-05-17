using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControll : MonoBehaviour
{
    GameObject CurrentFloor;
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float jumpPower = 15f;
    [SerializeField] int HP;
    [SerializeField] int Max_HP = 0;
    [SerializeField] GameObject Hp_Bar;
    [SerializeField] GameObject ReplayBottom;
    private float direction = 0.5f;
    private Rigidbody2D PlayerRb;
    private Animator PlayerAnima;
    private bool alive = true;
    private bool isJumping = false;
    void Start()
    {
        HP = Max_HP = 10;
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerAnima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
        if (alive)
        {
            PlayerMove();
            Jump();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Normal")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                CurrentFloor = other.gameObject;
                ModifyHP(1);
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (other.gameObject.tag == "Nails")
        {
            if (other.contacts[0].normal == new Vector2(0f, 1f))
            {
                CurrentFloor = other.gameObject;
                ModifyHP(-3);
            }
        }
        else if (other.gameObject.tag == "Ceiling")
        {
            CurrentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHP(-3);
        }
        PlayerAnima.SetBool("isJump", false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathLine")
        {
            GameOver();
        }
       
    }
    void ModifyHP(int num)
    {
        HP += num;
        if (HP > 10)
        {
            HP = 10;
        }
        else if (HP <= 0)
        {
            HP = 0;
            GameOver();
        }
    }
    void GameOver()
    {
        Time.timeScale = 0f;
        ReplayBottom.SetActive(true);
    }
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    void PlayerMove()
    {
        PlayerAnima.SetBool("isRun", false);
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(direction, 1, 1);
            PlayerAnima.SetBool("isRun", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(direction, 1, 1);
            PlayerAnima.SetBool("isRun", true);
        }
        Hp_Bar.transform.localScale = new Vector3(((float)HP / (float)Max_HP), Hp_Bar.transform.localScale.y, Hp_Bar.transform.localScale.z);
       //if (Input.GetKeyDown(KeyCode.S))
       // {
       //     this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
       //     PlayerRb.AddForce(new Vector2(0,2), ForceMode2D.Impulse);
       //     this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
       // }
    }
    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        && !PlayerAnima.GetBool("isJump"))
        {
            isJumping = true;
            PlayerAnima.SetBool("isJump", true);
        }
        if (!isJumping)
        {
            return;
        }

        PlayerRb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        PlayerRb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        isJumping = false;
    }
    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerAnima.SetTrigger("idle");
            alive = true;
        }
    }
}
