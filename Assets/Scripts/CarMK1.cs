using UnityEngine;

namespace CarMK1
{
    [RequireComponent(typeof(CarMK1Chassis))]
    public class CarMK1 : MonoBehaviour
    {
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float maxBrakeTorque;

        private CarMK1Chassis carChassis;

        //DEBUG
        public float ThrottleControl;
        public float SteerControl;
        public float BrakeControl;
        //public float HandBrakeControl;

        private void Start()
        {
            carChassis = GetComponent<CarMK1Chassis>();            
        }

        private void FixedUpdate()
        {
            carChassis.MotorTorque = maxMotorTorque * ThrottleControl;
            carChassis.SteerAngle = maxSteerAngle * SteerControl;
            carChassis.BrakeTorque = maxBrakeTorque * BrakeControl;
        }       
    }
}