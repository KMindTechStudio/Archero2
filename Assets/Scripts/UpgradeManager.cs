using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("Kéo tất cả UpgradeOptionSO vào đây")]
    public List<UpgradeOptionSO> allOptions;

    // Pool runtime, khởi tạo duy nhất một lần
    private List<UpgradeOptionSO> _availOptions;
    private List<UpgradeOptionSO> _choices = new List<UpgradeOptionSO>();

    [Header("UI References")]
    public GameObject panel;      // Panel chứa 3 nút chọn
    public Button[]   buttons;    // 3 nút bấm
    public Image[]    icons;      // 3 icon hiển thị
    public TMP_Text[] names;      // 3 text tên skill
    public TMP_Text[] descs;      // 3 text mô tả

    void Start()
    {
        // Tạo pool copy từ allOptions
        _availOptions = new List<UpgradeOptionSO>(allOptions);
        panel.SetActive(false);
    }

    /// <summary>
    /// Gọi khi đủ điều kiện (vd: kill đủ quái), sẽ show panel chọn upgrade
    /// </summary>
    public void ShowChoices()
    {
        panel.SetActive(true);
        _choices.Clear();

        // Tạo 1 bản copy pool để random mà không ảnh hưởng trực tiếp _availOptions
        var pool = new List<UpgradeOptionSO>(_availOptions);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (pool.Count == 0)
            {
                // Nếu không còn option nào để show thì ẩn nút
                buttons[i].gameObject.SetActive(false);
                continue;
            }

            // Random một trong pool
            int idx = Random.Range(0, pool.Count);
            var opt = pool[idx];
            pool.RemoveAt(idx); // remove Option khỏi list
            _choices.Add(opt);

            // Gán UI
            icons[i].sprite   = opt.icon;
            names[i].text     = opt.skillName;
            descs[i].text     = opt.description;

            // Thiết lập nút bấm
            int cap = i;  // capture index
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnOptionSelected(cap));
            buttons[i].gameObject.SetActive(true);
        }
    }
    
    private void OnOptionSelected(int i)
    {
        foreach (var b in GameObject.FindGameObjectsWithTag("PlayerBullet"))
            Destroy(b);
        foreach (var l in GameObject.FindGameObjectsWithTag("Laser"))
            Destroy(l);
        var chosen = _choices[i];
        _availOptions.Remove(chosen);
        PlayerShooting.Instance.ApplyUpgrade(chosen.type, chosen.value);
        panel.SetActive(false);
        StartCoroutine(FindObjectOfType<GameManager>().ResumeAndSpawnNext());
    }
}
