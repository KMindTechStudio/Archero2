using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel_Skills : MonoBehaviour
{
    public Image skillArt;
    public TMP_Text skillsName;
    public TMP_Text skillsDescription;
    public void SetSkill(Skills skill)
    {
        skillArt.sprite = skill.image;
        skillsName.text = skill.panelName;
        skillsDescription.text = skill.description;
    }


}
