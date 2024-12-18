using UnityEngine;

public class RoadDesSpawn : MonoBehaviour
{
    protected float distance = 0;
    public float distanceDestroy = 126;
    private void Update()
    {
        if (!GameManager.instance.isStart)
        {
            return;
        }
        this.UpDateRoad();
    }
    protected virtual void UpDateRoad()
    {
        transform.position -=Vector3.forward * Time.deltaTime* GameManager.instance.speedRoad;
        this.distance = Vector3.Distance(PlayerCtrl.instance.transform.position, transform.position);
        if (this.distance > distanceDestroy)
        {
            this.DesSpawn();
        }
    }
    protected virtual void DesSpawn()
    {
        Destroy(gameObject);
    }
}
