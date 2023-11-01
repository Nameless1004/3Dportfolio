using RPG.Control;
using RPG.Core.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.UI
{
    public class PlayerInputHandler : BaseUI
    {
        PlayerInput playerInput;
        Joystick joystick;

        public override void Init()
        {
            joystick = GetComponentInChildren<Joystick>();
            playerInput = Managers.Instance.Game.CurrentPlayer.GetComponent<PlayerInput>();
            joystick.OnValueChanged += OnInputValueChanged;
        }

        public void OnInputValueChanged(Vector2 input)
        {
            playerInput.UpdateInputValue(input);
        }
    }
}
