using RPG.Core.UI;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core.Manager;
using RPG.Combat;

public class PlayerHpPresenter : BaseUI
{
    private void OnDestroy()
    {
        model.OnHealthChanged -= OnHealthChanged;
    }

    Health model;
    PlayerHpView view;

    private void OnHealthChanged(float ratio)
    {
        view.OnHealthChanged(ratio);
    }

    public override void Init()
    {
        model = Managers.Instance.Game.CurrentPlayer.GetComponentInChildren<Health>();
        view = GetComponentInChildren<PlayerHpView>();
        model.OnHealthChanged += OnHealthChanged;
        OnHealthChanged(1f);
    }
}
