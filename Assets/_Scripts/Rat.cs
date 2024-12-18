using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public Vector3[] target = new Vector3[2];
    public float speedRat = 2;
    public int index = 0;
    public bool enemyFacingRight;
    private void Awake()
    {
        target[0]= new Vector3(-2f,0,transform.localPosition.z);
        target[1] = new Vector3(2f,0,transform.localPosition.z); 
    }
    private void Update()
    {
        if (!GameManager.instance.isStart)
        {
            return;
        }
        RatMove();
    }
    public void RatMove()
    {
        Vector3 targetPos = target[index];
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, speedRat * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance <= 0.02f)
        {
            index++;
        }
        if (index == target.Length)
        {
            index = 0;
        }
        CheckEnemyFacing(targetPos);
        if (index == 0 && enemyFacingRight)
        {
            EnemyFlip();
        }
        if (index == 1 && !enemyFacingRight)
        {
            EnemyFlip();
        }
    }
    public virtual void CheckEnemyFacing(Vector3 targetPos)
    {
        Vector3 enemyPos = targetPos - transform.position;
        float angle = Vector3.Angle(Vector3.right, enemyPos);
        if (angle == 0)
        {
            enemyFacingRight = true;
        }
        else
        {
            enemyFacingRight = false;
        }
    }
    public virtual void EnemyFlip()
    {
        Vector3 curDirection = transform.right;
        curDirection = -1 * curDirection;
        transform.right = curDirection;
        enemyFacingRight = !enemyFacingRight;
    }
}
