using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public static Movement Instance { get; private set; }
    [SerializeField] bool btnPress = false;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float moveSpeedMulti = 0;
    [Range(0, 10)][SerializeField] float accelerate;
    [SerializeField] bool isHitWall;
    [SerializeField] public bool ishitGround;

    [SerializeField] float wallDistantFormPlayer;
    BoxCollider2D wallCheckArea;
    BoxCollider2D boxCollider2D;
    RaycastHit2D[] wallHit = new RaycastHit2D[5];
    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    [SerializeField] Transform respawnLocate;
    [SerializeField] GameObject boomEfect;
    [SerializeField] GameObject cam;
    BoxCollider2D wallDectect;
    [SerializeField] ContactFilter2D contactFilter2D;

    Vector2 playerDirection => transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool isOnPlatfrom;
    public Rigidbody2D platfromRB;
    public Vector3 startScale;
    Rigidbody2D rigidbody2D;
    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        wallDectect = transform.Find("Wall Detect").GetComponent<BoxCollider2D>();
        this.respawnLocate = GameObject.Find("SpawnLocation").transform;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        // Respawn();
        startScale = this.transform.localScale;
        // this.UpdateRespawn(this.transform.position);
    }
    private void FixedUpdate()

    {
        MoveSpeedAccelerate();
        float targetSpeed = moveSpeed * moveSpeedMulti;
        if (isOnPlatfrom)
        {

            if (btnPress == true)
            {
                this.rigidbody2D.velocity = new Vector2((targetSpeed + this.platfromRB.velocity.x) * playerDirection.x, this.rigidbody2D.velocity.y);
            }
            else if (btnPress == false)
            {
                this.rigidbody2D.velocity = new Vector2(targetSpeed * playerDirection.x + this.platfromRB.velocity.x, this.rigidbody2D.velocity.y);
            }




        }
        else
        {

            this.rigidbody2D.velocity = new Vector2(targetSpeed * playerDirection.x, this.rigidbody2D.velocity.y);

        }

        // this.rigidbody2D.velocity = new Vector2(targetSpeed * transform.localScale.x * Time.fixedDeltaTime, this.rigidbody2D.velocity.y);
        // CheckForWall();
        CheckForGround();


    }

    public void Move(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPress = true;

        }
        else if (value.canceled)
        {
            btnPress = false;


        }
    }
    void CheckForWall()
    {
        if (boxCollider2D.Cast(playerDirection, contactFilter2D, wallHit, wallDistantFormPlayer) > 0)
        {
            Debug.Log(wallHit.GetValue(0));
            this.isHitWall = true;
            this.Flip();
        }
        else
        {
            this.isHitWall = false;
        }
    }
    void CheckForGround()
    {
        if (boxCollider2D.Cast(Vector2.down, contactFilter2D, groundHit, 0.1f) > 0)
        {
            this.ishitGround = true;

        }
        else
        {
            this.ishitGround = false;
        }
    }

    public void Flip()
    {
        // Debug.Log("flip");
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    public void MoveSpeedAccelerate()
    {
        if (this.btnPress == true && this.moveSpeedMulti <= 1)
        {
            this.moveSpeedMulti = this.moveSpeedMulti + Time.fixedDeltaTime * this.accelerate;
        }
        else if (this.btnPress == false && this.moveSpeedMulti >= 0)
        {
            this.moveSpeedMulti = this.moveSpeedMulti - Time.fixedDeltaTime * this.accelerate;
            if (this.moveSpeedMulti < 0)
            {
                this.moveSpeedMulti = 0;
            }
        }
    }
    public void UpdateRespawn(Vector3 newPos)
    {
        this.respawnLocate.position = newPos;
    }
    IEnumerator Respawn()
    {
        rigidbody2D.simulated = false;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.3f);
        this.transform.position = this.respawnLocate.position;
        this.transform.localScale = startScale;
        rigidbody2D.simulated = true;
        spriteRenderer.enabled = true;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Trap"))
        {
            Instantiate(this.boomEfect, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.death);
            cam.transform.DOShakePosition(1, 0.1f);
            StartCoroutine(nameof(this.Respawn));

        }
        if (other.CompareTag("Effect"))
        {
            this.rigidbody2D.mass = 0.10f;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Effect"))
        {
            this.rigidbody2D.mass = 1;

        }
    }
}
