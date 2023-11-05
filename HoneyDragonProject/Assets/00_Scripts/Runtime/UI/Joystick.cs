using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Core.UI
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Joystick Instance => instance;
        static private Joystick instance;
        public RectTransform Panel;
        public RectTransform Handle;
        private float circleRange;
        private Vector2 pivot;
        private bool isPressed = false;
        public Vector2 Direction { get; private set; }
        //

        public event Action<Vector2> OnValueChanged;
        private void Awake()
        {
            instance = this;
            pivot = Handle.transform.position;
            circleRange = GetComponent<RectTransform>().rect.width / 2f;
        }

        private void Update()
        {
            Direction = (Handle.anchoredPosition - pivot).normalized;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;
            Handle.position = pivot;
            Direction = Vector2.zero;
            OnValueChanged?.Invoke(Direction);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isPressed == false) return;

            Direction = (eventData.position - pivot).normalized;
            if ((eventData.position - pivot).magnitude > circleRange)
            {
                Handle.position = pivot + (Direction * circleRange);
            }
            else
            {
                Handle.position = eventData.position;
            }
            OnValueChanged?.Invoke(Direction);
        }
    }
}
