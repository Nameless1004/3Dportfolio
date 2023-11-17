using RPG.Core.Data;
using RPG.Core.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    public int CharacterModelId;
    private PlayerData playerData;
    public PlayerData PlayerData { get
        {
            if(playerData == null)
            {
                playerData = Managers.Instance.Data.PlayerDataDict[CharacterModelId];
            }
            return playerData;
        } }

    public CanvasGroup Group { get; private set; }
    private Button button;
    public Action<CharacterSelectButton> OnButtoClicked;


    private void Awake()
    {
        Group = GetComponent<CanvasGroup>();
        button = GetComponentInChildren<Button>();
        playerData = Managers.Instance.Data.PlayerDataDict[CharacterModelId];
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
