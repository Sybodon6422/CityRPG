using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour,IEnteractable
{
    #region controls
    [SerializeField] private float steeringWheelAngle;
    [SerializeField] private float throttlePosition;
    [SerializeField] private float brakePosition;
    public void SetSteeringWheelAngle(float attemptedAngle) { steeringWheelAngle = Mathf.Clamp(attemptedAngle, -45, 45); }
    public void SetThrottlePosition(float attemptedThrottle) { throttlePosition = Mathf.Clamp(attemptedThrottle, -1, 1); }
    public void SetBrakePosition(float attemptedBrake) { brakePosition = Mathf.Clamp(attemptedBrake, -1, 1); }
    #endregion

    private bool occupied = false;

    private float steering = 3;

    private Rigidbody2D rb2D;
    public float horsePower;
    public float testingPowerMutliplier;
    public Transmission tranny;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

/*    private void FixedUpdate()
    {
        if(brakePosition > .1) { rb2D.drag = brakePosition * 5; return; } else { rb2D.drag = 0; }
        rb2D.AddForce((transform.up * (horsePower * throttlePosition)) * testingPowerMutliplier);
        rb2D.MoveRotation(rb2D.rotation + (steeringWheelAngle*Time.deltaTime));
    }*/
    void FixedUpdate()
    {
        float v = throttlePosition;
        float h = steeringWheelAngle;

        Vector2 speed = transform.up * (v * horsePower);
        rb2D.AddForce(speed * tranny.GetGearRatio() * tranny.finalDrive);

        float direction = Vector2.Dot(rb2D.velocity, rb2D.GetRelativeVector(Vector2.up));
        if (direction >= 0.0f)
        {
            rb2D.rotation += h * steering * (rb2D.velocity.magnitude / 5.0f);
            //rb2D.AddTorque((h * steering) * (rb2D.velocity.magnitude / 10.0f));
        }
        else
        {
            rb2D.rotation -= h * steering * (rb2D.velocity.magnitude / 5.0f);
            //rb2D.AddTorque((-h * steering) * (rb2D.velocity.magnitude / 10.0f));
        }

        Vector2 forward = new Vector2(0.0f, 0.5f);
        float steeringRightAngle;
        if (rb2D.angularVelocity > 0)
        {
            steeringRightAngle = -90;
        }
        else
        {
            steeringRightAngle = 90;
        }

        Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
        Debug.DrawLine((Vector3)rb2D.position, (Vector3)rb2D.GetRelativePoint(rightAngleFromForward), Color.green);

        float driftForce = Vector2.Dot(rb2D.velocity, rb2D.GetRelativeVector(rightAngleFromForward.normalized));

        Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);


        Debug.DrawLine((Vector3)rb2D.position, (Vector3)rb2D.GetRelativePoint(relativeForce), Color.red);

        rb2D.AddForce(rb2D.GetRelativeVector(relativeForce));
    }

    public void UpShift() { tranny.currentGear += 1; }
    public void DownShift() { tranny.currentGear -= 1; }

    public void AttemptEnteract(PlayerMaster player)
    {
        if (occupied) { return; }
        else
        {
            PlayerMaster.I.EnterVehicle();
            gameObject.AddComponent<CarPlayerDriver>();
        }
    }

    [Serializable]
    public class Transmission 
    {
        public int currentGear;
        public float finalDrive;
        [SerializeField] private float[] gearRatios;

        public float GetGearRatio()
        {
            return gearRatios[currentGear];
        }
    }
}
