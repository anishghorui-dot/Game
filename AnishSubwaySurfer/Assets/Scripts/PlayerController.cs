using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;
    public float jumpForce = 15f;
    public float slideTime = 1.5f;
    
    [Header("Lane Settings")]
    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private Vector3 targetPosition;
    
    [Header("States")]
    private bool isGrounded = true;
    private bool isSliding = false;
    private float slideTimer = 0f;
    
    [Header("Components")]
    private Rigidbody rb;
    private Animator animator;
    private CapsuleCollider playerCollider;
    private float originalHeight;
    
    [Header("Input")]
    private float swipeStartX;
    private float swipeStartY;
    private bool isSwiping = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        
        if (playerCollider != null)
        {
            originalHeight = playerCollider.height;
        }
        
        targetPosition = transform.position;
    }
    
    void Update()
    {
        HandleInput();
        HandleSliding();
        MoveToTargetLane();
        
        // Always move forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        
        // Update GameManager score
        GameManager.instance.UpdateScore(forwardSpeed * Time.deltaTime);
    }
    
    void HandleInput()
    {
        // Keyboard Controls
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(1);
        }
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }
        
        // Touch/Swipe Controls
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                swipeStartX = touch.position.x;
                swipeStartY = touch.position.y;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                float swipeEndX = touch.position.x;
                float swipeEndY = touch.position.y;
                
                float swipeDeltaX = swipeEndX - swipeStartX;
                float swipeDeltaY = swipeEndY - swipeStartY;
                
                if (Mathf.Abs(swipeDeltaX) > Mathf.Abs(swipeDeltaY))
                {
                    // Horizontal swipe
                    if (swipeDeltaX > 50)
                    {
                        MoveLane(1); // Swipe right
                    }
                    else if (swipeDeltaX < -50)
                    {
                        MoveLane(-1); // Swipe left
                    }
                }
                else
                {
                    // Vertical swipe
                    if (swipeDeltaY > 50)
                    {
                        Jump(); // Swipe up
                    }
                    else if (swipeDeltaY < -50)
                    {
                        Slide(); // Swipe down
                    }
                }
                
                isSwiping = false;
            }
        }
    }
    
    void MoveLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
        targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        
        // Play dodge animation
        if (animator != null)
        {
            animator.SetTrigger(direction > 0 ? "DodgeRight" : "DodgeLeft");
        }
    }
    
    void MoveToTargetLane()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, (currentLane - 1) * laneDistance, laneChangeSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
    
    void Jump()
    {
        if (isGrounded && !isSliding)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }
    }
    
    void Slide()
    {
        if (isGrounded && !isSliding)
        {
            isSliding = true;
            slideTimer = slideTime;
            
            // Reduce collider height
            if (playerCollider != null)
            {
                playerCollider.height = originalHeight * 0.5f;
                playerCollider.center = new Vector3(0, originalHeight * 0.25f, 0);
            }
            
            if (animator != null)
            {
                animator.SetBool("IsSliding", true);
            }
        }
    }
    
    void HandleSliding()
    {
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            
            if (slideTimer <= 0)
            {
                StopSliding();
            }
        }
    }
    
    void StopSliding()
    {
        isSliding = false;
        
        // Restore collider height
        if (playerCollider != null)
        {
            playerCollider.height = originalHeight;
            playerCollider.center = new Vector3(0, originalHeight * 0.5f, 0);
        }
        
        if (animator != null)
        {
            animator.SetBool("IsSliding", false);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            
            if (animator != null)
            {
                animator.SetBool("IsGrounded", true);
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.instance.AddCoins(10);
            Destroy(other.gameObject);
            
            // Play coin collect sound/effect
        }
        else if (other.CompareTag("ANISH_Letter"))
        {
            LetterCollectible letter = other.GetComponent<LetterCollectible>();
            if (letter != null)
            {
                GameManager.instance.CollectLetter(letter.letterIndex);
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("PowerUp"))
        {
            PowerUp powerUp = other.GetComponent<PowerUp>();
            if (powerUp != null)
            {
                powerUp.Activate(gameObject);
                Destroy(other.gameObject);
            }
        }
    }
    
    void GameOver()
    {
        // Stop the game
        forwardSpeed = 0;
        enabled = false;
        
        GameManager.instance.GameOver();
    }
    
    public void IncreaseSpeed(float amount)
    {
        forwardSpeed += amount;
    }
}
