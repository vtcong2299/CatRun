using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;
    public Rigidbody rb;
    public PlayerAnim playerAnim;
    public float laneOffset = 1.5f;
    public Vector3 rayOffset = new Vector3(0,0.7f,0);
    public int currentLane = 1;
    public float changeLaneSpeed = 5.0f;
    public float jumpHeight = 3.0f; 
    private Vector3 targetPosition;
    public bool isJumping = false;
    public bool isSliding = false;
    public bool isDead;
    private float jumpStartTime;
    public float jumpDuration = 0.6f; // Thời gian nhảy lên
    public BoxCollider colliderPlayer;
    public float sildingColliderSizeY = 0.6f;
    public float sildingColliderCenterY = 0.3f;
    public float oldColliderCenterY = 0.6f;
    public float oldColliderSizeY = 1.2f;
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
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnim>();
    }

    private void Update()
    {
        if (!GameManager.instance.isStart)
        {
            return;
        }
        PlayerMove();
    }
    public void PlayerMove()
    {        
        if (isDead) return;
        CheckInput();
        CheckStatus();
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * changeLaneSpeed);        
    }
    public void CheckStatus()
    {
        if (isJumping)
        {
            float elapsedTime = Time.time - jumpStartTime;
            float jumpProgress = elapsedTime / jumpDuration;

            if (jumpProgress < 1.0f)
            {
                float height = Mathf.Sin(Mathf.PI * jumpProgress) * jumpHeight;
                targetPosition.y = height;
            }
            else
            {
                isJumping = false;
                targetPosition.y = 0;
            }
        }
        if (isSliding)
        {
            targetPosition.y = 0;
            isSliding = false;
        }
    }
    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Sliding();
        }
    }

    public void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;

        if (targetLane < 0 || targetLane > 2)
            return;

        currentLane = targetLane;
        targetPosition.x = (currentLane - 1) * laneOffset;
    }

    public void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            playerAnim.SetAnimJump();
            jumpStartTime = Time.time;
        }
    }
    public void Sliding()
    {
        if (!isSliding)
        {
            isSliding = true;
            playerAnim.SetAnimSliding();
            ChangeColliderWhenSliding();
        }
    }
    public void ChangeColliderWhenSliding()
    {
        StartCoroutine(ChangeCollider());
    }
    IEnumerator ChangeCollider()
    {
        colliderPlayer.center = new Vector3(colliderPlayer.center.x, sildingColliderCenterY, colliderPlayer.center.z);
        colliderPlayer.size = new Vector3(colliderPlayer.size.x, sildingColliderSizeY, colliderPlayer.size.z);
        yield return new WaitForSeconds(1);
        colliderPlayer.center = new Vector3(colliderPlayer.center.x, oldColliderCenterY, colliderPlayer.center.z);
        colliderPlayer.size = new Vector3(colliderPlayer.size.x, oldColliderSizeY, colliderPlayer.size.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        CheckPlayerHit(other);
    }
    public void CheckPlayerHit(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if(isDead) return;
            playerAnim.SetAnimHit();
            isDead = true;
            GameManager.instance.PlayerHit();            
            targetPosition.y = 0;
        }
        GameManager.instance.CheckPlayerAteFishBone(other);
    }
    public void PlayerRestartGame()
    {
        isDead = false;
        playerAnim.SetRestart();
    }
}
