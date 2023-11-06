using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace nara
{

    public class PlayerEffect : MonoBehaviour
    {
        [SerializeField]
         GameObject RRun;
        [SerializeField]
         GameObject LRun;
        [SerializeField]
         GameObject RBreak;
        [SerializeField]
         GameObject LBreak;
        [SerializeField]
         GameObject BJump;
        [SerializeField]
         GameObject DJump;

        GameObject go;


        //public void EffectPlay(Vector3 pos, string str, float time = 1.0f)
        //{
        //    if (str == "RRun")
        //    {
        //        go = Instantiate(RRun, pos, Quaternion.identity);
        //    }
        //    else if (str == "LRun")
        //    {
        //        go = Instantiate(LRun, pos, Quaternion.identity);
        //    }
        //    else if (str == "RBreak")
        //    {
        //        go = Instantiate(RBreak, pos, Quaternion.identity);
        //    }
        //    else if (str == "LBreak")
        //    {
        //        go = Instantiate(LBreak, pos, Quaternion.identity);
        //    }
        //    else if (str == "Jump")
        //    {
        //        go = Instantiate(BJump, pos, Quaternion.identity);
        //    }
        //    else if (str == "DJump")
        //    {
        //        go = Instantiate(DJump, pos, Quaternion.identity);
        //    }
        //    Destroy(go, time);
        //}

        public void Run(Vector3 pos, int dir, float time = 1.0f)
        {
            pos.y += 0.3f;
            if (dir > 0)
            {

                go = Instantiate(RRun, pos, Quaternion.identity);
            }
            else
            {
                go = Instantiate(LRun, pos, Quaternion.identity);
            }
            Destroy(go, time);
        }

        public void Break(Vector3 pos, int dir, float time = 1.0f)
        {
            pos.y += 0.3f;
            if (dir > 0)
            {
                go = Instantiate(RBreak, pos, Quaternion.identity);
            }
            else
            {
                go = Instantiate(LBreak, pos, Quaternion.identity);
            }
            Destroy(go, time);
        }

        public void Jump(Vector3 pos, int dir, float time = 1.0f)
        {
            pos.y += 0.3f;
            if (dir > 0)
            {
                go = Instantiate(BJump, pos, Quaternion.identity);
            }
            else
            {
                go = Instantiate(DJump, pos, Quaternion.identity);
            }
            Destroy(go, time);
        }


    }

}