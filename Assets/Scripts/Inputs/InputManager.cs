using Cinemachine;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputActions Inputs;

    private void Awake()
    {
        Inputs = new InputActions();
    }

    private void OnEnable()
    {
        var playerController = FindObjectOfType<PlayerController>();
        if (playerController == null) return;
        
        Inputs.Character.MoveJoystick.performed += moveValue =>
        {
            playerController.SetMovementVector(moveValue.ReadValue<Vector2>());
        };
        Inputs.Character.MoveJoystick.canceled += moveValue => {
            playerController.SetMovementVector(moveValue.ReadValue<Vector2>());
        };
        
        var cinemachineInputProvider = FindObjectOfType<CinemachineInputProvider>();
        cinemachineInputProvider.AutoEnableInputs = true;

        Inputs.Character.Move.performed += moveValue =>
        {
            playerController.SetMovementVector(moveValue.ReadValue<Vector3>());
        };
        

        Inputs.Character.Jump.performed += jumpValue =>
        {
            playerController.SetJump(jumpValue.ReadValueAsButton());
        };
        Inputs.Character.Sprint.performed += sprintValue =>
        {
            playerController.SetSprint(sprintValue.ReadValueAsButton());
        };
        
        Inputs.Enable();
    }

    private void OnDisable()
    {
        Inputs.Disable();
    }
}
