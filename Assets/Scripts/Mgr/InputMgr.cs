using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nara
{
    public class InputMgr : MonoBehaviour
    {
        public Action KeyAction = null;

        
       public void OnUpdate()
        {
            if(Input.anyKey&&KeyAction!=null)
            {
                KeyAction.Invoke();
            }
        }
    }

}