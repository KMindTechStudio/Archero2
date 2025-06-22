using System.Linq;
using UnityEngine;

public class List_Skills : MonoBehaviour
{
    public Skills[] listSkills;
    public Panel_Skills[] panelSkills;
    void Start()
    {
        AssignRandomSkills();
    }

    void AssignRandomSkills()
    {
        Skills[] shuffled = listSkills.OrderBy(x => Random.value).ToArray();
        for (int i = 0; i < panelSkills.Length; i++)
        {
            panelSkills[i].SetSkill(shuffled[i]);
        }
    }
}
