using UnityEngine;

[CreateAssetMenu(
    menuName = "Upgrades/Option",
    fileName = "NewUpgradeOption"
)]
public class UpgradeOptionSO : ScriptableObject
{
    [Header("Thông tin hiển thị")]
    public string skillName;        // Tên hiển thị
    [TextArea] public string description; // Mô tả
    public Sprite icon;            // Ảnh icon

    [Header("Logic nâng cấp")]
    public UpgradeType type;
    public float value;            // chỉ dùng cho DamageUp

    public enum UpgradeType
    {
        Pierce,       // xuyên thấu
        TripleShot,   // bắn 3 viên
        DamageUp      // tăng sát thương
    }
}