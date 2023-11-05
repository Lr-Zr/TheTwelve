using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nara
{
    public class PlayerAnimation : MonoBehaviour
    {
        Animator _animator;

        void Start()
        {
            _animator = transform.GetComponent<Animator>();
        }
        public void SetAnim(PlayerState State)
        {
            Debug.Log("¹¹¾ß?");
            _animator.SetInteger("State",(int) State);
        }
        public void SetJump(bool isjump)
        {
            _animator.SetBool("IsJump", isjump);
        }
        public void SetDJump(bool isDjump)
        {
            _animator.SetBool("IsDJump", isDjump);
        }
    }

}