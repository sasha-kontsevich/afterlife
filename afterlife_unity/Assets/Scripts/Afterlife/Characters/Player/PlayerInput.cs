using Afterlife.Characters.Core;
using UnityEngine;

namespace Afterlife.Characters.Player
{
    /// <summary>
    /// Обрабатывает ввод игрока и передает команды в CharacterMotor.
    /// </summary>
    [RequireComponent(typeof(CharacterMotor))]
    public class PlayerInput : MonoBehaviour
    {
        private CharacterMotor _motor;

        private void Awake()
        {
            // Получаем ссылку на CharacterMotor (гарантированно существует благодаря RequireComponent)
            _motor = GetComponent<CharacterMotor>();
        }

        private void Update()
        {
            // Получаем ввод для горизонтального перемещения
            var horizontal = Input.GetAxisRaw("Horizontal");

            // Передаем управление движением в мотор
            _motor.Move(horizontal);

            // Проверяем ввод прыжка
            if (Input.GetButtonDown("Jump"))
            {
                _motor.Jump();
            }
        }
    }
}