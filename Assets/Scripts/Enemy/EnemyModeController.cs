using System;
using UnityEngine;

namespace SADungeon.Enemy
{

    public enum EnemyMode
    {
        NORMAL = 0,
        ALERT
    }

    /// <summary>
    /// Controls a global mode for all dogs, when the player enters/stays in
    /// any dog's collider trigger mode is switched to ALERT, and after alertDuation
    /// the mode is switched back to NORMAL.
    /// </summary>
    public class EnemyModeController : MonoBehaviour
    {
        public static float alertTime = 0f;
        [SerializeField] private float alertDuration = 60f;
        private static EnemyMode _dogMode;

        public static EnemyMode dogMode
        {
            set
            {
                //Reset the timer if dogmode is alert
                if (value == EnemyMode.ALERT)
                    alertTime = 0f;
                if (value != _dogMode)
                {
                    _dogMode = value;
                    onDogModeChanged?.Invoke(value);
                }
            }
            get => _dogMode;
        }

        public static Action<EnemyMode> onDogModeChanged;


        private void Update()
        {
            if (dogMode == EnemyMode.ALERT)
            {
                if (alertTime < alertDuration)
                {
                    alertTime += Time.deltaTime;
                    if (alertTime > alertDuration)
                    {
                        dogMode = EnemyMode.NORMAL;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")/* && !PlayerController.hidden*/)
            {
                dogMode = EnemyMode.ALERT;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player")/* && !PlayerController.hidden*/)
            {
                dogMode = EnemyMode.ALERT;
            }
        }
    }
}