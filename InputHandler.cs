using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacterControls
{
    public class InputHandler : MonoBehaviour
    {
        public float m_Horizontal;
        public float m_Vertical;
        public float m_MovementAmount;
        public float m_MouseX;
        public float m_MouseY;

        public PlayerControls m_inputActions;
        CameraHandler cameraHandler;

        Vector2 m_MovementInput;
        Vector2 m_CameraInput;

        private void Awake()
        {
            cameraHandler = CameraHandler.Instance;
        }
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler && m_MouseY!=0 && m_MouseY!=0) 
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, m_MouseX, m_MouseY);
            }
        }
        public void OnEnable()
        {
            if (m_inputActions == null)
            {
                m_inputActions = new PlayerControls();
                m_inputActions.PlayerMovement.Movement.performed += inputActions => m_MovementInput = inputActions.ReadValue<Vector2>();
                m_inputActions.PlayerMovement.Camera.performed += i => m_CameraInput = i.ReadValue<Vector2>();
            }
            m_inputActions.Enable();
        }
        public void OnDisable()
        {
            m_inputActions.Disable();
        }
        public void TickInput(float delta) 
        {
            MoveInput(delta);
        }
        public void MoveInput(float delta)
        {
            m_Horizontal = m_MovementInput.x;
            m_Vertical = m_MovementInput.y;
            m_MovementAmount = Mathf.Clamp01(Mathf.Abs(m_Horizontal) + Mathf.Abs(m_Vertical));
            m_MouseX = m_CameraInput.x;
            m_MouseY = m_CameraInput.y;
        }
    }
}
