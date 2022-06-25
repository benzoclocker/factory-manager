using UnityEngine;

namespace Core.GameConfigs
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Config/Game Config/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public float PlayerMoveSpeed;
        public float PlayerRotateSpeed;
        public float UnPickDelay;
        public int MaxBoxCount;
    }
}