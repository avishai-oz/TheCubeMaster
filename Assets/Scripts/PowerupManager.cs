/*
using System.Collections.Generic;
using UnityEngine;


    public class PowerupManager : MonoBehaviour
    {
        [Min(0.1f)] public float defaultDurationSeconds = 12f;

        public float SpeedMult { get; private set; } = 1f;
        public float JumpMult  { get; private set; } = 1f;
        public float SizeMult  { get; private set; } = 1f;

        private readonly Dictionary<PowerupType, float> _timeLeft = new()
        {
            { PowerupType.Speed, 0f },
            { PowerupType.Jump,  0f },
            { PowerupType.Size,  0f }
        };

        void Update()
        {
            float dt = Time.deltaTime;
            TickType(PowerupType.Speed, dt, () => SpeedMult = 1f);
            TickType(PowerupType.Jump,  dt, () => JumpMult  = 1f);
            TickType(PowerupType.Size,  dt, () => SizeMult  = 1f);
        }

        void TickType(PowerupType t, float dt, System.Action onExpire)
        {
            if (_timeLeft[t] <= 0f) return;
            _timeLeft[t] -= dt;
            if (_timeLeft[t] <= 0f) { _timeLeft[t] = 0f; onExpire?.Invoke(); }
        }

        /// החל פאואר־אפ: קובע מכפיל ומרענן טיימר (לא מצטבר).
        public void Apply(PowerupType type, float multiplier, float? durationSeconds = null)
        {
            float dur = Mathf.Max(0.01f, durationSeconds ?? defaultDurationSeconds);

            switch (type)
            {
                case PowerupType.Speed: SpeedMult = Mathf.Max(0.01f, multiplier); break;
                case PowerupType.Jump:  JumpMult  = Mathf.Max(0.01f, multiplier); break;
                case PowerupType.Size:  SizeMult  = Mathf.Max(0.01f, multiplier); break;
            }
            _timeLeft[type] = dur; // refresh
        }

        public float GetTimeLeft(PowerupType type) =>
            _timeLeft.TryGetValue(type, out var t) ? Mathf.Max(t, 0f) : 0f;
    }
    */
