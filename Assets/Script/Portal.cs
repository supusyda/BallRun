using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform distinate;
    GameObject player;
    Animation playerAnimator;
    Rigidbody2D playerRigi;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animation>();
        playerRigi = player.GetComponent<Rigidbody2D>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                // playerAnimator.Play("Player_Inportal");

                StartCoroutine(nameof(InPortal));
                // other.transform.position = this.distinate.position;

            }
        }
    }
    IEnumerator InPortal()
    {

        // Animator playerAnimator = player.GetComponent<Animator>();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.portalIn);
        playerRigi.simulated = false;
        playerAnimator.Play("New Animation");
        yield return new WaitForSeconds(0.5f);
        this.playerRigi.velocity = Vector2.zero;
        player.transform.position = this.distinate.position;

        playerAnimator.Play("New Animation2");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.portalOut);

        yield return new WaitForSeconds(0.3f);
        playerRigi.simulated = true;




    }
}
