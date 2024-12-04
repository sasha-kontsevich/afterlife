using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Afterlife.Characters.Core
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;
        private CharacterMotor _characterMotor;

        private void Awake()
        {
            _animator = transform.GetComponent<Animator>();
            _characterMotor = transform.GetComponent<CharacterMotor>();
        }

        private void Start()
        {
            _characterMotor.StateMachine.OnStateChanged += UpdateAnimatorState;
        }

        private void UpdateAnimatorState(string stateName)
        {
            _animator.SetTrigger(stateName);
            // Debug.Log(stateName);
        }
    }
}