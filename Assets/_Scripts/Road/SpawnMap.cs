using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    public static SpawnMap instance;
    public GameObject roadPreFab;
    public GameObject roadSpawnPos;
    public GameObject obstacleInit;
    public float distance = 0;
    public float distanceSpawn = 64;
    public GameObject roadCurrent;
    public GameObject obstaclePreFab;
    public GameObject obstacleCurrent;
    public List<GameObject> listObstacles = new List<GameObject>();
    public int index=0;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    private void Awake()
    {
        SpawnMapRestart();
    }
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
        this.distance = Vector3.Distance(PlayerCtrl.instance.transform.position, this.roadCurrent.transform.position);
        if (this.distance > distanceSpawn)
        {
            this.Spawn();
        }
    }
    protected virtual void Spawn()
    {
        Vector3 pos = this.roadSpawnPos.transform.position;
        pos.x = 0;
        pos.y = 0;
        this.PerformSpawnRoad(pos);
        index = Random.Range(0, listObstacles.Count);
        obstaclePreFab = listObstacles[index];
        PerformSpawnObstacle(pos);
    }
    protected virtual void PerformSpawnRoad(Vector3 position)
    {
        this.roadCurrent = Instantiate(this.roadPreFab, position, this.roadPreFab.transform.rotation);
        this.roadCurrent.transform.parent = transform;
        this.roadCurrent.SetActive(true);
    }
    protected virtual void PerformSpawnObstacle(Vector3 position)
    {
        this.obstacleCurrent = Instantiate(this.obstaclePreFab, position, this.obstaclePreFab.transform.rotation);
        this.obstacleCurrent.transform.parent = roadCurrent.transform;
        this.obstacleCurrent.SetActive(true);
    }
    public void SpawnMapRestart()
    {
        Destroy(roadCurrent.gameObject);
        obstaclePreFab = obstacleInit;
        this.PerformSpawnRoad(this.roadPreFab.transform.position);
        this.PerformSpawnObstacle(this.obstaclePreFab.transform.position);
    }
}
