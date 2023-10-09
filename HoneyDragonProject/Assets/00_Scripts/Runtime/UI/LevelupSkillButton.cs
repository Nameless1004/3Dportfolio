using RPG.Core.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelupSkillButton : MonoBehaviour
{
    public Image SkillIcon;
    public TMP_Text SkillLevelupDescription;
    public SkillData SkillData;
    private Button button;
    public event Action<LevelupSkillButton> OnButtonClicked;


    public void BindSkillData(SkillData skillData)
    {
        SkillData = skillData;
        SkillIcon.sprite = Resources.Load<Sprite>(skillData.IconPath);
       var test = string.Format(skillData.Description, skillData.Name, skillData.Level, skillData.SpawnCount, skillData.MinDamage, skillData.MaxDamage, (int)(skillData.SpawnLifeTimeMilliSecond * 0.001f), skillData.CoolTime);
        Debug.Log(test);
        SkillLevelupDescription.text = test;
    }

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void OnButtonClick()
    {
        OnButtonClicked?.Invoke(this);
    }

}
