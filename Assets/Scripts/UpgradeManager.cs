using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Scriptable Options")]
    public List<UpgradeOptionSO> allOptions;

    [Header("UI")]
    public GameObject panel;
    public Button[] buttons;
    public Image[] icons;
    public Text[] names, descs;

    private List<UpgradeOptionSO> choices = new List<UpgradeOptionSO>();

    void Start()
    {
        panel.SetActive(false);
    }

    public void ShowChoices()
    {
        panel.SetActive(true);

        // Random 3 option
        var pool = new List<UpgradeOptionSO>(allOptions);
        choices.Clear();
        for (int i = 0; i < buttons.Length && pool.Count > 0; i++)
        {
            int idx = Random.Range(0, pool.Count);
            var opt = pool[idx];
            pool.RemoveAt(idx);
            choices.Add(opt);

            icons[i].sprite = opt.icon;
            names[i].text   = opt.skillName;
            descs[i].text   = opt.description;

            int cap = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnOptionSelected(cap));
        }
    }

    private void OnOptionSelected(int i)
    {
        // Áp dụng nâng cấp vào Player
        var o = choices[i];
        PlayerShooting.Instance.ApplyUpgrade(o.type, o.value);

        // Ẩn panel
        panel.SetActive(false);

        // Resume và spawn tiếp sau 1s
        var gm = FindObjectOfType<GameManager>();
        StartCoroutine(gm.ResumeAndSpawnNext());
    }
}