namespace CodeBase.Gameplay.Heroes.Configs
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Path/HeroConfig", order = 1)]
    public class HeroConfig : ScriptableObject
    {
        public Hero Prefab;

        public float MovementSpeed = 5;
        public float RotationSpeed = 5;
        public LayerMask Mask;
    }
}