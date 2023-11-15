using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    public string modelPrefabPath;

    public string modelName;
    // 캐릭터 추가 효과
    public string Description;

    public CanvasGroup Group { get; private set; }
    private Button button;
    public Action<CharacterSelectButton> OnButtoClicked;


    private void Awake()
    {
        Group = GetComponent<CanvasGroup>();
        button = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        OnButtoClicked?.Invoke(this);
    }
}
