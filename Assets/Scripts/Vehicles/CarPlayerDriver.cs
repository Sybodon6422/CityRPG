using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerDriver : MonoBehaviour
{
    private CharacterControls controls;
    private CameraController cam;
    private CarController car;

    private float throttle;
    private float brake;
    private float steerAngle;

    private void Awake()
    {
        controls = new CharacterControls();
    }

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
        car = GetComponent<CarController>();

        controls.Vehicle.Throttle.performed += ctx => throttle = ctx.ReadValue<float>();
        controls.Vehicle.Throttle.canceled += ctx => throttle = 0;

        controls.Vehicle.Brake.performed += ctx => brake = ctx.ReadValue<float>();
        controls.Vehicle.Brake.canceled += ctx => brake = 0;

        controls.Vehicle.Steering.performed += ctx => steerAngle = ctx.ReadValue<float>();
        controls.Vehicle.Steering.canceled += ctx => steerAngle = 0;

        controls.Vehicle.ShiftUp.performed += ctx => car.UpShift();
        controls.Vehicle.ShiftDown.performed += ctx => car.DownShift();

        controls.Default.enteract.performed += ctx => ExitCar();

        cam.trackedPlayer = transform;
    }

    private void FixedUpdate()
    {
        car.SetThrottlePosition(throttle - brake);
        car.SetSteeringWheelAngle(-steerAngle);
    }

    public void ExitCar()
    {

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
