 
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Kenshi
{
    [RequireComponent(typeof(Entity))]
    public class KenshiCharacterController : MonoBehaviour 
    {
        [SerializeField] private KenshiControll kenshControll; 
        public float speed = 3;
 
        public Entity entity;
        public Vector3 Velocity { get; set; }
        public float Gravity { get; } = 9.8f;
        public CharacterController characterController;
        
        
        private void Awake()
        { 
            this.entity = GetComponent<Entity>();
            this.characterController = GetComponent<CharacterController>(); 
        }
        public void MoveToDirection(Vector3 dir)
        {
            Vector3 moveDir = dir * Time.deltaTime * speed; 
            characterController.Move(moveDir); 
        }

        private void Update()
        {
            (float x, float y) axis = (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
            MoveToDirection(new Vector3(axis.x, 0, axis.y));

            if (characterController.isGrounded == false)
            {
                characterController.Move(new Vector3(0, -Gravity * Time.deltaTime, 0));
            }
        }

        public void Teleport(Vector3 teleportPosition)
        {
            
        }
    }
}