using System;
using Unity.Jobs;
using UnityEngine;

namespace Kenshi.Runtime.Scripts.Job
{
    struct TransformMoveJob : IJob
    {
        public void Execute()
        {
            
        }
    }
    public class CreateTransform : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("hit me"))
            {
                var tmj = new TransformMoveJob(); 
                var handle = tmj.Schedule(); 
            }
        }
    }
}