using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Kenshi
{
    public class Character : Entity, IHitable, IMoveable
    {
        [SerializeField] private Transform root;
        [SerializeField] private Animator animator;

        public override Transform Root
        {
            get => root;
        }

        public Animator Animator
        {
            get => animator;
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