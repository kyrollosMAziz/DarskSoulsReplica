using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacterControls
{
    public class PlayerLocomotion : MonoBehaviour
    {
        public Transform m_CameraObject;
        public InputHandler m_InputHandler;
        public Vector3 m_MoveDirection;

        [HideInInspector]
        public Transform m_Transform;
        [HideInInspector]
        public AnimationHandler m_AnimationHandler;

        public Rigidbody m_Rigidbody;
        public GameObject m_NormalCamera;

        [Header("Stats")]
        [SerializeField] float m_Speed = 5;
        [SerializeField] float m_gravityStrenght = 5;
        [SerializeField] float m_Rotation = 10;

        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_InputHandler = GetComponent<InputHandler>();
            m_CameraObject = Camera.main.transform;
            m_Transform = transform;
            m_AnimationHandler = GetComponentInChildren<AnimationHandler>();
            m_AnimationHandler.Init();
        }
        public void Update()
        {
            float delta = Time.deltaTime;

            m_InputHandler.TickInput(delta);
            m_MoveDirection = m_CameraObject.forward * m_InputHandler.m_Vertical;
            m_MoveDirection += m_CameraObject.right * m_InputHandler.m_Horizontal;
            m_MoveDirection.Normalize();

            float speed = m_Speed;
            m_MoveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(m_MoveDirection, m_NormalVector);
            m_Rigidbody.velocity = projectedVelocity - transform.up* m_gravityStrenght;

            m_AnimationHandler.UpdateAnimatorValues(m_InputHandler.m_MovementAmount, 0);

            if (m_AnimationHandler.GetCanRotate()) 
            {
                HandleRotation(delta);
            }
        }
        #region Movement
        Vector3 m_NormalVector;
        Vector3 m_TargetPosition;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = m_InputHandler.m_MovementAmount;

            targetDir = m_CameraObject.forward * m_InputHandler.m_Vertical;
            targetDir += m_CameraObject.right * m_InputHandler.m_Horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = m_Transform.forward;

            float rs = m_Rotation;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(m_Transform.rotation, tr, rs * delta);

            m_Transform.rotation = targetRotation;
        }

        #endregion
    }
}