using System.Collections.Generic;
using UnityEngine;


    public class PowerupManager : MonoBehaviour
    {
        [Min(0.1f)] public float defaultDurationSeconds = 12f;

        public float SpeedMult { get; private set; } = 1f;
        public float JumpMult  { get; private set; } = 1f;
        public float SizeMult  { get; private set; } = 1f;

        private readonly Dictionary<PowerupTypes.PowerupType, float> _timeLeft = new()
        {
            { PowerupTypes.PowerupType.Speed, 0f },
            { PowerupTypes.PowerupType.Jump,  0f },
            { PowerupTypes.PowerupType.Size,  0f }
        };

        void Update()
        {
            float dt = Time.deltaTime;
            TickType(PowerupTypes.PowerupType.Speed, dt, () => SpeedMult = 1f);
            TickType(PowerupTypes.PowerupType.Jump,  dt, () => JumpMult  = 1f);
            TickType(PowerupTypes.PowerupType.Size,  dt, () => SizeMult  = 1f);
        }

        void TickType(PowerupTypes.PowerupType t, float dt, System.Action onExpire)
        {
            if (_timeLeft[t] <= 0f) 
                return;
            
            _timeLeft[t] -= dt;
            
            if (_timeLeft[t] <= 0f) { 
                _timeLeft[t] = 0f; 
                onExpire?.Invoke(); 
            }
        }
        
        public void Apply(PowerupTypes.PowerupType type, float multiplier, float? durationSeconds = null)
        {
            float dur = Mathf.Max(0.01f, durationSeconds ?? defaultDurationSeconds);

            switch (type)
            {
                case PowerupTypes.PowerupType.Speed: 
                    SpeedMult = Mathf.Max(0.01f, multiplier); 
                    break;
                case PowerupTypes.PowerupType.Jump:  
                    JumpMult = Mathf.Max(0.01f, multiplier); 
                    break;
                case PowerupTypes.PowerupType.Size:  
                    SizeMult = Mathf.Max(0.01f, multiplier); 
                    break;
            }
            _timeLeft[type] = dur; 
        }
    }
