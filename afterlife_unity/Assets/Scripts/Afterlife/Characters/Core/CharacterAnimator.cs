using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Afterlife.Characters.Core
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;
        private CharacterMotor _characterMotor;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _animator = transform.GetComponent<Animator>();
            _spriteRenderer = transform.GetComponent<SpriteRenderer>();
            _characterMotor ??= transform.GetComponent<CharacterMotor>();
            _characterMotor ??= transform.GetComponentInParent<CharacterMotor>();
        }

        private void Start()
        {
            if (_characterMotor)
            {
                _characterMotor.StateMachine.OnStateChanged += UpdateAnimatorState;
            }
        }

        private void FixedUpdate()
        {
            UpdateDirection();
        }

        private void UpdateAnimatorState(string stateName)
        {
            _animator.SetTrigger(stateName);
            Debug.Log(stateName);
        }


        private void UpdateDirection()
        {
            if (!_characterMotor) return;
            _spriteRenderer.flipX = _characterMotor.DirectionX switch
            {
                > 0 => false,
                < 0 => true,
                _ => _spriteRenderer.flipX
            };
        }
    }
}