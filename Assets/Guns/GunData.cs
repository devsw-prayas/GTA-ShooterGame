using UnityEngine;

[CreateAssetMenu(fileName ="Gun")]
public class GunData : ScriptableObject {
    public new string name;
    public int bullets;
    public int magSize;
    public float rpm;
    public float reloadTime;
    public float damage;
    public int shotCount = 1;
    public float range;
    public bool automatic;
    public float ADSAccuracy;
    public float accuracy;
    public float kickback;
}
