using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "SO/Weapon/Gun")]
public class GunSO : ScriptableObject
{
    public int MaxAmmo;//최대 탄약
    public int FireRate;//공겨    
    public int Damage;//대미지
    public int BulletPerShot;//산탄
}
