using System;
using UnityEngine;

namespace CarMK1
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarMK1Chassis : MonoBehaviour
    {
        [SerializeField] private WheelAxle[] wheelAxles;
        [SerializeField] private float wheelBaseLenght;

        [SerializeField] private Transform centerOfMass;

        [Header("Down Force")]
        [SerializeField] private float downForceMin;
        [SerializeField] private float downForceMax;
        [SerializeField] private float downForceFactor;

        [Header("Angular Drag")]
        [SerializeField] private float angularDragMin;
        [SerializeField] private float angularDragMax;
        [SerializeField] private float angularDragFactor;

        //DEBUG       
        public float MotorTorque;
        public float BrakeTorque;
        public float SteerAngle;

        public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;
            
        private new Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (centerOfMass != null ) 
                rigidbody.centerOfMass = centerOfMass.localPosition;
        }
        private void FixedUpdate()
        {
            UpdateAngularDrag();

            UpdateDownForce();

            UpdateWheelAxles();
        }
        private void UpdateAngularDrag()
        {
            rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
        }
        private void UpdateDownForce()
        {
           float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
            rigidbody.AddForce(-transform.up *  downForce);

        }

        private void UpdateWheelAxles()
        {
            int amountMotorWheel = 0;

            for (int i = 0; i < wheelAxles.Length; i++)
            { 
                if (wheelAxles[i].IsMotor == true)
                    amountMotorWheel += 2;
            }

                for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].Update();

                wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
                wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLenght);
                wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
            }
        }
    }
}
