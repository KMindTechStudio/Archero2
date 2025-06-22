using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeManager : MonoBehaviour
{
    [Header("Scriptable Options")]
    public List<UpgradeOptionSO> allOptions;

    [Header("UI")]
    public GameObject UpgradePanel;
    public Button[] Option;
    public Image[] icons;
    public TMP_Text[] names, descs;

    private List<UpgradeOptionSO> choices = new List<UpgradeOptionSO>();

    void Start()
    {
        UpgradePanel.SetActive(false);
    }

    public void ShowChoices()
    {
        UpgradePanel.SetActive(true);
        var pool = new List<UpgradeOptionSO>(allOptions);
        choices.Clear();
        for (int i = 0; i < Option.Length && pool.Count > 0; i++)
        {
            int idx = Random.Range(0, pool.Count);
            var opt = pool[idx];
            pool.RemoveAt(idx);
            choices.Add(opt);

            icons[i].sprite = opt.icon;
            names[i].text   = opt.skillName;
            descs[i].text   = opt.description;
            int cap = i;
            Option[i].onClick.RemoveAllListeners();
            Option[i].onClick.AddListener(() => OnOptionSelected(cap));
        }
    }

    private void OnOptionSelected(int i)
    {
        var o = choices[i];
        PlayerShooting.Instance.ApplyUpgrade(o.type, o.value);
        UpgradePanel.SetActive(false);
        var gm = FindObjectOfType<GameManager>();
        StartCoroutine(gm.ResumeAndSpawnNext());
    }
}