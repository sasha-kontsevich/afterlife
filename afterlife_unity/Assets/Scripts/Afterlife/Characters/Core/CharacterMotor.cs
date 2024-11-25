using UnityEngine;

namespace Afterlife.Characters.Core
{
    /// <summary>
    /// Класс для управления движением персонажа.
    /// Включает перемещение по оси X и прыжки, а также проверку на землю.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class CharacterMotor : MonoBehaviour
    {
        /// <summary>
        /// Скорость перемещения персонажа по оси X.
        /// </summary>
        [Header("Movement Settings")]
        public float moveSpeed = 5f;

        /// <summary>
        /// Сила прыжка.
        /// </summary>
        public float jumpForce = 10f;

        /// <summary>
        /// Маска слоя, который считается землей для проверки.
        /// </summary>
        public LayerMask groundLayer;

        private Rigidbody2D _rb;
        private Collider2D _coll;
        private bool _isGrounded;

        /// <summary>
        /// Инициализация компонента.
        /// Получаем необходимые компоненты (Rigidbody2D и Collider2D).
        /// </summary>
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _coll = GetComponent<Collider2D>();
        }

        /// <summary>
        /// Проверка на землю. Вызывается каждый фиксированный кадр.
        /// </summary>
        private void FixedUpdate()
        {
            // Проверка, стоит ли персонаж на земле
            _isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
        }

        /// <summary>
        /// Двигает персонажа по оси X.
        /// </summary>
        /// <param name="horizontal">Направление движения по оси X (например, -1 или 1).</param>
        public void Move(float horizontal)
        {
            // Перемещение персонажа
            _rb.linearVelocity = new Vector2(horizontal * moveSpeed, _rb.linearVelocity.y);
        }

        /// <summary>
        /// Прыжок персонажа. Происходит только если персонаж на земле.
        /// </summary>
        public void Jump()
        {
            if (_isGrounded)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            }
        }
    }
}
