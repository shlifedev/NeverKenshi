using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using NotImplementedException = System.NotImplementedException;

namespace Kenshi
{    
    public class Character : Entity, IHitable, IMoveable, KenshiControll.IPlayerControllActions
    { 
        [SerializeField]  Vector3 test;
        [SerializeField] private Transform root;
        [SerializeField] private Animator animator;
        [SerializeField] private KenshiControll kenshControll; 
        public Vector3 Position
        {
            get
            {
                return this.transform.position;
            }
            set
            {
                this.transform.position = value;
            }
        }
        public override Transform Root
        {
            get => root;
        }

        public Animator Animator
        {
            get => animator;
        }


        private void Awake()
        {
            kenshControll = new KenshiControll();   
            kenshControll.PlayerControll.Enable();  
            kenshControll.PlayerControll.AddCallbacks(this);
        }
 

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            MoveToDirection(input);
            Debug.Log(input);
        }

        public void OnNewaction(InputAction.CallbackContext context)
        {
             
        }

        public void Hit(HitInfo info)
        {
            throw new NotImplementedException();
        }
 

        public void MoveToDirection(Vector3 direction)
        { 
            Root.transform.Translate(direction.normalized * Time.deltaTime);
        }
    }
}