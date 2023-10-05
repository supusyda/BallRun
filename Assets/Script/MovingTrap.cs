using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : Trap
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    [SerializeField] List<Transform> movePoints;
    int currentPoint = 0;
    [Range(0, 5)]
    [SerializeField] private float speed;
    [Range(0, 5)]
    [SerializeField] private float waitTime = 3f;

    [SerializeField] Vector2 dir;
    [SerializeField] private int moveIndexDir = 1;

    // Movement playerMove;
    Rigidbody2D rigidbody2D;

    // Update is called once per frame
    private void Awake()
    {
        Transform wayPoint = transform.parent.Find("MovePoint");
        // playerMove = GameObject.Find("Player").GetComponent<Movement>();
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


        if (currentPoint >= movePoints.Count - 1)
        {
            this.moveIndexDir = -1;
        }
        else if (currentPoint == 0)
        {
            this.moveIndexDir = 1;

        }
        this.currentPoint = this.currentPoint + this.moveIndexDir;
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
   
}
