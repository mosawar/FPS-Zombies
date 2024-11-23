using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // private references to the PlayerInput system and the corresponding actions
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    // References to other player components (PlayerMotor and PlayerLook)
    private PlayerMotor motor;
    private PlayerLook look;

    // Called when the script instance is being loaded (before the first frame update)
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        // Bind the Jump action from the input system to the motor's Jump method
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Process the movement input (Vector2) and pass it to the motor for handling
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

    }

    // enable the OnFoot actions to start listening for player input
    private void OnEnable()
    {
        onFoot.Enable();
    }
    // Disable the OnFoot actions to stop listening for player input
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
