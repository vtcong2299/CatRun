using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjObstacle : MonoBehaviour
{
    public Animator animator;
    public bool isDead;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isDead)
            {
                isDead = true;
                animator.SetTrigger("isHit");
            }
        }
    }
}