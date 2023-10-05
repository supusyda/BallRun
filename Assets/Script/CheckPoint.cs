using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D collider2D;
    private void Awake()
    {
        collider2D = transform.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Check point");
            Movement movement = other.GetComponent<Movement>();
            movement.UpdateRespawn(this.transform.position);
            collider2D.enabled = false;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.checkPoint);
            

        }

    }
   
}
