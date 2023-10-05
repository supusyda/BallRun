using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Transform> movePoints;
    int currentPoint = 0;
    [SerializeField] private float speed;
    [SerializeField] Vector2 dir;

    Movement playerMove;
    Rigidbody2D rigidbody2D;
    public float waitTime = 3f;

    // Update is called once per frame
    private void Awake()
    {
        Transform wayPoint = transform.parent.Find("MovePoint");
        playerMove = GameObject.Find("Player").GetComponent<Movement>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        foreach (Transform point in wayPoint)
        {
            movePoints.Add(point);
        }
        CalDir();
    }
    private void FixedUpdate()
    {
        Move();

    }
    private void Update()
    {

        float distant = Vector3.Distance(transform.position, movePoints[currentPoint].transform.position);
        if (distant <= 0.05f)
        {
            NextPoint();
            // CalDir();
        }

    }
    void NextPoint()
    {
        this.Stop();

        currentPoint++;
        if (currentPoint >= movePoints.Count)
        {
            currentPoint = 0;
        }

        StartCoroutine(nameof(StopAfterComePoint));
    }
    IEnumerator StopAfterComePoint()
    {

        yield return new WaitForSeconds(this.waitTime);
        CalDir();



    }
    void MoveTorwardPoint(Transform point)
    {
        transform.position = Vector3.MoveTowards(transform.position, point.transform.position, Time.fixedDeltaTime * this.speed);
    }
    void CalDir()
    {
        this.dir = (movePoints[currentPoint].position - transform.position).normalized;
    }
    void Move()
    {
        // Debug.Log("Still Moving");
        rigidbody2D.velocity = this.dir * speed;
    }
    void Stop()
    {
        // Debug.Log("Stoping");
        this.dir = Vector2.zero;
        rigidbody2D.velocity = Vector3.zero;
    }
    private void OnCollisionEnter2D(Collision2D other)

    {
        other.transform.parent = this.transform;
        playerMove.isOnPlatfrom = true;
        playerMove.platfromRB = this.rigidbody2D;
        playerMove.GetComponent<Rigidbody2D>().gravityScale = 10;
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        other.transform.parent = null;
        playerMove.isOnPlatfrom = false;
        playerMove.GetComponent<Rigidbody2D>().gravityScale = 1;


    }
}

