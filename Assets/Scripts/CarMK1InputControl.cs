using UnityEngine;

namespace CarMK1 
{
    public class CarMK1InputControl : MonoBehaviour
    {
        [SerializeField] private CarMK1 car;

        private void Update()
        {
            car.ThrottleControl = Input.GetAxis("Vertical");
            car.BrakeControl = Input.GetAxis("Jump");
            car.SteerControl = Input.GetAxis("Horizontal");
        }
    }
}