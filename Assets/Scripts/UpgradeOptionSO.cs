using UnityEngine;

[CreateAssetMenu(
    menuName = "Upgrades/Option",
    fileName = "NewUpgradeOption"
)]
public class UpgradeOptionSO : ScriptableObject
{
    [Header("Thông tin hiển thị")]
    public string skillName;    
    [TextArea] public string description; 
    public Sprite icon;          

    [Header("Logic nâng cấp")]
    public UpgradeType type;
    public float value;    

    public enum UpgradeType
    {
        Pierce,     
        TripleShot, 
        DamageUp 
    }
}