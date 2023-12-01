using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    private GameObject Player;

    private bool hasLineOfSight = false;

    public enum ShootMode { None, ShootAll};
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        if (hasLineOfSight)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.transform.position - transform.position);
        if(ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Player");
            if(hasLineOfSight )
            {
                Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.red);
            }
        }
    }

    private void TryToShoot()
    {
        switch (ShootMode)
        {
            case ShootMode.None:
                break;
            case ShootMode.ShootAll:
                foreach (ShootingController gun in guns)
                {
                    gun.Fire();
                }
                break;
        }
    }
}
