using UnityEngine;

namespace RPG.Core.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        /// <summary>
        /// 기본적으로 오브젝트를 활성화시킵니다. 추가적으로 처리가 필요하실 override해서 사용
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 기본적으로 오브젝트를 비활성화시킵니다. 추가적으로 처리가 필요하실 override해서 사용
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public abstract void Init();
    }
}
