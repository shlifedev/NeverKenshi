using System;
using Cinemachine;
using UnityEngine;

namespace Kenshi
{ 
    public class GameCamera : MonoBehaviour
    {
        public enum CameraState
        {
            FollowThird,
            Free,
            
        }
        public CinemachineVirtualCamera cam;

        public void Awake()
        {
            
        }
    }
}