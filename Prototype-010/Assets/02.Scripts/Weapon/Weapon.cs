using UnityEngine;

public class Weapon : MonoBehaviour, IEquipable
{
    public bool isEquipped { get; set; }

    public void Equip()
    {
        isEquipped = true;
    }
}
