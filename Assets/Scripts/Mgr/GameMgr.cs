using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace nara
{

    public class GameMgr : MonoBehaviour
    {
        static GameMgr _Ins;//�ν��Ͻ�
        public static GameMgr Instance { get { Init();return _Ins; } }



        InputMgr _Input = new InputMgr();
        public static InputMgr Input { get { return Instance._Input; } }



        void Start()
        {
            Init();
        }


        void Update()
        {
            _Input.OnUpdate();
        }

        static void Init()
        {
            if(_Ins == null )
            {
                GameObject gameobject = GameObject.Find("@GameMgr");
                if(gameobject != null )
                {
                    gameobject = new GameObject { name = "@GameMgr" };//������Ʈ ����
                    gameobject.AddComponent<GameMgr>();

                }
                DontDestroyOnLoad(gameobject);
                _Ins = gameobject.GetComponent<GameMgr>();
            }
        }
    }

}