using RoboRyanTron.Unite2017.Variables;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private FloatVariable HealthVariable;
    [SerializeField] private FloatVariable ManaVariable;
    [SerializeField] private FloatVariable StaminaVariable;

    private CharacterController characterController;

    [Header("Movmeent Settings")]
    [SerializeField] private float velocity = 5;
    [SerializeField] private float sprintModificator = 3;
    [SerializeField] private float staminaUse = 0.5f;
    [SerializeField] private LayerMask layerMask;

    [Header("Skill Settings")]
    [SerializeField] SkillSO JumpSkill;
    [SerializeField] SkillSO SprintSkill;

    private float yMovement = -9.81f;


    private Vector3 movementValue;
    private bool isJumping;
    private bool isSprinting;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one instance of this!", gameObject);
        
        Instance = this;
        characterController = GetComponent<CharacterController>();
    }

    private void Update() 
    {
        var movementValue_ = movementValue;
        
        if (SprintSkill.IsActive && isSprinting && StaminaVariable.Value > 0)
        {
            movementValue_ *= sprintModificator;
            StaminaVariable.Value -= staminaUse * Time.deltaTime;

            isSprinting = false;
        }
        else
        {
            StaminaVariable.Value += Time.fixedDeltaTime;
            StaminaVariable.Value = Mathf.Clamp01(StaminaVariable.Value);
        }

        movementValue_ *= velocity;
        movementValue_ *= Time.deltaTime;

        characterController.Move(new Vector3(movementValue_.x, yMovement * Time.deltaTime, movementValue_.y));
        if(characterController.velocity.sqrMagnitude > 0.1)
            transform.forward = new Vector3(movementValue_.x, 0f, movementValue_.y);

        if (JumpSkill.IsActive && isJumping && characterController.isGrounded) {
            yMovement = 10f;
            isJumping = false;
        }

        yMovement = Mathf.Max(-9.81f, yMovement - Time.deltaTime * 30f);
    }

    public void SetJump(bool state) {
        isJumping = state;
    }
    
    public void SetSprint(bool state) {
        isSprinting = state;
    }

    public void SetMovementVector(Vector3 movement) {
        this.movementValue = movement;
    }
}
