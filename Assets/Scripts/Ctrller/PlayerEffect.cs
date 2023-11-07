using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace nara
{


    public class PlayerEffect : MonoBehaviour
    {

        [SerializeField]
        GameObject[] _Effects;

        GameObject[] go;
        void Start()
        {
            
            go = new GameObject[(int)Effect.End];
        }

        public void EffectPlay(Vector3 pos, Effect effect, float time = 1.0f)
        {
            pos += _Effects[(int)effect].transform.position;
            if (go[(int)effect]!=null) return;
            go[(int)effect] = Instantiate(_Effects[(int)effect], pos, Quaternion.identity);
            Destroy(go[(int)effect], time);

        }

    }

}