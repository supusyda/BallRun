using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem runParticle;
    [SerializeField] ParticleSystem fallParticle;


    [Range(0, 10)]
    [SerializeField] float afterVelocity;
    // [SerializeField] BoxCollider2D groundCheck;

    [SerializeField] float dustFormationAfterTime;

    float counter;
    [SerializeField] Rigidbody2D playerRG2D;
    private void Awake()
    {   
        playerRG2D = transform.parent.GetComponent<Rigidbody2D>();
        runParticle = transform.Find("Run Particle").GetComponent<ParticleSystem>();
        fallParticle = transform.Find("Fall Particle").GetComponent<ParticleSystem>();

        // groundCheck = transform.GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        if (this.transform.parent.localScale.x > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);

        }

        counter += Time.deltaTime;
        if (Movement.Instance.ishitGround && Mathf.Abs(this.playerRG2D.velocity.x) > this.afterVelocity)
        {
            if (this.counter > this.dustFormationAfterTime)
            {
                // Debug.Log("wtfs");
                runParticle.Play();
                counter = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        fallParticle.Play();
    }

}
