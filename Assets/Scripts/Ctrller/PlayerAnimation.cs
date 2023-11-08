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
            _animator.SetInteger("State",(int) State);
        }
        public void SetIsJump(bool isjump)
        {
            _animator.SetBool("IsJump", isjump);
        }
        public void SetIsDJump(bool isDjump)
        {
            _animator.SetBool("IsDJump", isDjump);
        }
        public void SetIsRunning(bool isRunning)
        {
            _animator.SetBool("IsRunning", isRunning);
        }
        public void SetIsAttack(bool isAttack)
        {
            _animator.SetBool("IsAttacking", isAttack);
        }

        public void TriggerAtk()
        {
            _animator.SetTrigger("Attack");
        }

    }

}