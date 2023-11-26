using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Core.UI
{
    public class HealthProgressBar : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float offsetY;
        private Health targetHealth;
        private Camera mainCam;
        public GameObject progressBarObject;
        public Image progressBar;

        private void Awake()
        {
            targetHealth = target.GetComponent<Health>();
            mainCam = Camera.main;
        }
        private void OnEnable()
        {
            targetHealth.OnHealthChanged -= OnHealthChanged;
            targetHealth.OnHealthChanged += OnHealthChanged;
            targetHealth.OnDie -= OnTargetDie;
            targetHealth.OnDie += OnTargetDie;
        }

        private void OnDisable()
        {
            targetHealth.OnHealthChanged -= OnHealthChanged;
            targetHealth.OnDie -= OnTargetDie;
        }

        void LateUpdate()
        {
            progressBarObject.transform.position = target.position + target.up * offsetY;
            progressBarObject.transform.forward = mainCam.transform.forward * -1f;
        }


        private void OnTargetDie()
        {
            transform.DestroyChildren();
            Destroy(gameObject);
        }

        private void OnHealthChanged(float ratio)
        {
            progressBar.fillAmount = ratio;
        }
    }
}