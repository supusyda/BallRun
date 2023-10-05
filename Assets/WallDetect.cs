using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetect : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            player = transform.parent.GetComponent<Movement>();
            player.Flip();
        }


    }
}
