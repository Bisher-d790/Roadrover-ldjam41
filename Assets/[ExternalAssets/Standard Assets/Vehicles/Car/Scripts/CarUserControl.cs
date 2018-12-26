using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        [SerializeField] private GroundCheck GroundCheckTrigger;
        [SerializeField] private float dragVelocity = 10f;
        [SerializeField] private float rotateVelocity = 10f;
        [SerializeField] private float JumpControllTimeoutLimit = 3.0f;
        [SerializeField] private float TimeBetweenJumps = .3f;
        [SerializeField] private float additionalGravity = 100f;
        [SerializeField] private float additionalTorque = 50f;
        [SerializeField] private float handbrakeForce = 1000000f;

        float Timeout = 0;



        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            GroundCheckTrigger = GroundCheckTrigger.GetComponent<GroundCheck>();
            GroundCheckTrigger.setTimeBetweenJumps(TimeBetweenJumps);
        }


        private void Update()
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0,-1,0) * additionalGravity);

            if (GroundCheckTrigger.getTrigger()) Timeout = 0;

            if (Input.GetButton("Jump") && GroundCheckTrigger.getTrigger())
            {
                m_Car.Jump();
                GroundCheckTrigger.setTrigger(false);
            }

            if (!GroundCheckTrigger.getTrigger())
            {
                if (Timeout < JumpControllTimeoutLimit) this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, CrossPlatformInputManager.GetAxis("Vertical")) * dragVelocity, ForceMode.Impulse);
                transform.Rotate(Vector3.up * Time.deltaTime * rotateVelocity * CrossPlatformInputManager.GetAxis("Horizontal"));
                Timeout += Time.deltaTime;
            }

            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            if (v > 0) { this.GetComponent<Rigidbody>().AddForce(transform.forward * additionalTorque); }
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Brake");
            if (handbrake > 0) { this.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1, 0) * handbrakeForce); }


            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }

    }
}
