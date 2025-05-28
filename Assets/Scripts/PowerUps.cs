using UnityEngine;

[CreateAssetMenu(fileName = "PowerUps", menuName = "Scriptable Objects/PowerUps")]
public class PowerUps : ScriptableObject
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject objeto;
        [Range(0f, 1f)] public float probabilidad;
    }

    public DropItem[] posiblesDrops;
}

