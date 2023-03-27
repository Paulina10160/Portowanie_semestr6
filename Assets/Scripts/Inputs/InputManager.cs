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
        Inputs.Character.MoveJoystick.performed += moveValue =>
        {
            PlayerController.Instance.SetMovementVector(moveValue.ReadValue<Vector2>());
        };
        Inputs.Character.MoveJoystick.canceled += moveValue => {
            PlayerController.Instance.SetMovementVector(moveValue.ReadValue<Vector2>());
        };
        
        var cinemachineInputProvider = FindObjectOfType<CinemachineInputProvider>();
        cinemachineInputProvider.AutoEnableInputs = true;

        Inputs.Character.Move.performed += moveValue =>
        {
            PlayerController.Instance.SetMovementVector(moveValue.ReadValue<Vector3>());
        };
        
        Inputs.Character.Jump.performed += jumpValue =>
        {
            PlayerController.Instance.SetJump(jumpValue.ReadValueAsButton());
        };
        Inputs.Character.Sprint.performed += sprintValue =>
        {
            PlayerController.Instance.SetSprint(sprintValue.ReadValueAsButton());
        };
        
        Inputs.Enable();
    }

    private void OnDisable()
    {
        Inputs.Disable();
    }
}
