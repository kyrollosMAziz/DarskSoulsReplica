using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacterControls
{
    public class AnimationHandler : MonoBehaviour
    {
        public Animator anim;
        
        [SerializeField]
        private bool canRotate  = true;

        int vertical;
        int horizontal;

        public void Init()
        {
            anim = GetComponent<Animator>();
            vertical = Animator.StringToHash("vertical");
            horizontal = Animator.StringToHash("horizontal");
        }
        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f) 
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement< 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement< -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion
            anim.SetFloat(vertical, v,0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h,0.1f, Time.deltaTime);
            

        }
        public void SetCanRotate(bool rotationEnabled) 
        {
            canRotate = rotationEnabled;
        }
        public bool GetCanRotate() 
        {
            return canRotate;
        }
    }
}