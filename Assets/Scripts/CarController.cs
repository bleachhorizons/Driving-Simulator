using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private float currentBrakeForce;
    private float currentSteerAngle;
    private bool isBraking;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider FLCollider;
    [SerializeField] private WheelCollider FRCollider;
    [SerializeField] private WheelCollider RLCollider;
    [SerializeField] private WheelCollider RRCollider;

    [SerializeField] private Transform FLTransform;
    [SerializeField] private Transform FRTransform;
    [SerializeField] private Transform RLTransform;
    [SerializeField] private Transform RRTransform;

    // Update Game
    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleMotor() {
        RLCollider.motorTorque = verticalInput * motorForce;
        RRCollider.motorTorque = verticalInput * motorForce;
        brakeForce = isBraking ? brakeForce : 0f;
        if (isBraking) {
            ApplyBraking();
        }
    }

    private void ApplyBraking() {
        FLCollider.brakeTorque = currentBrakeForce;
        FRCollider.brakeTorque = currentBrakeForce;
        RLCollider.brakeTorque = currentBrakeForce;
        RRCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        FLCollider.steerAngle = currentSteerAngle;
        FRCollider.steerAngle = currentSteerAngle;
    }

    private void GetInput() {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void UpdateWheels() {
        UpdateSingleWheel(FLCollider, FLTransform);
        UpdateSingleWheel(FRCollider, FRTransform);
        UpdateSingleWheel(RLCollider, RLTransform);
        UpdateSingleWheel(RRCollider, RRTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
