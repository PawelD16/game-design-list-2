using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;

    private AudioSource audioSource;
    private Animator animator;

    public const int MAX_HEALTH_POINTS = 5;
    public const float SPEED = 5f;
    public const float JUMP_FORCE = 5f;
    public const float JUMP_THRESHHOLD = 0.001f;

    public const string PLAYER_TAG = "Player";
    public const string RESTING_ANIMATION_NAME = "RestingAnim";
    public const string MOVEMENT_ANIMATION_NAME = "MovementAnimation";
    public const string JUMP_ANIMATION_NAME = "JumpAnimation";

    private int _healthPoints = MAX_HEALTH_POINTS;
    private Vector2 currentMovementInput;
    private PlayerInput input;

    private int HealthPoints 
    { 
        get 
        {
            return _healthPoints;
        }

        set
        {
            _healthPoints = value;

            PlayerUIScript.UpdateHealth(_healthPoints);

            if (_healthPoints <= 0)
                StateManager.GameState = GameState.LOSE;
        }
    } 

    private static Player Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        input = new();
    }

    private void OnEnable()
    {
        input.Player.Move.performed += ctx => currentMovementInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => currentMovementInput = Vector2.zero;
        input.Player.Move.Enable();
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= ctx => currentMovementInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled -= ctx => currentMovementInput = Vector2.zero;
        input.Player.Move.Disable();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        gameObject.tag = PLAYER_TAG;
    }

    private void Update()
    {
        DecidePlayerMovement();
        DecideCurrentAnimation();
    }

    private void DecidePlayerMovement()
    {
        AddHorizontalMovement(currentMovementInput.x);
        AddVerticalMovement(currentMovementInput.y);
    }

    private void DecideCurrentAnimation()
    {
        if (playerRigidBody.velocity.x == 0 && playerRigidBody.velocity.y == 0)
            animator.Play(RESTING_ANIMATION_NAME);
        else if (playerRigidBody.velocity.x != 0 && playerRigidBody.velocity.y == 0)
            animator.Play(MOVEMENT_ANIMATION_NAME);
        else
            animator.Play(JUMP_ANIMATION_NAME);
    }

    public void AddHorizontalMovement(float moveX)
    {
        playerRigidBody.velocity = new Vector2(moveX * SPEED, playerRigidBody.velocity.y);
    }

    public void AddVerticalMovement(float moveY)
    {
        if (moveY > 0 && Mathf.Abs(playerRigidBody.velocity.y) < JUMP_THRESHHOLD)
            playerRigidBody.AddForce(new Vector2(0, JUMP_FORCE), ForceMode2D.Impulse);
        else if (moveY < 0)
            playerRigidBody.velocity = Vector2.zero;
    }

    public void MovementAnimation()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void IdleAnimation()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public static int GetPlayerHealth()
    {
        return Instance.HealthPoints;
    }

    public static void KillPlayer()
    {
        Instance.HealthPoints = 0;
        StateManager.GameState = GameState.LOSE;
    }

    public static void IncreasePlayerHealth()
    {
        ChangePlayerHealth(1);
    }

    public static void DecreasePlayerHealth()
    {
        ChangePlayerHealth(-1);
    }

    public static Transform GetTransform()
    {
        return Instance.transform;
    }

    private static void ChangePlayerHealth(int hpChange)
    {
        var instance = Instance;

        if (instance.HealthPoints + hpChange > MAX_HEALTH_POINTS || instance.HealthPoints + hpChange < 0)
            return;

        instance.HealthPoints += hpChange;
    }
}
