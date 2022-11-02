using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] TankBaseClass _tankHealth;
    Camera _cameraPlayer;

    void Awake()
    {
        _tankHealth.HealthChanged += OnHealthChanged;
       _cameraPlayer = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
       print(_cameraPlayer);
    }
    void OnDestroy()
    {
        _tankHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsPercentage)
    {
        _healthBarFilling.fillAmount = valueAsPercentage;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(_cameraPlayer.transform.position.x, _cameraPlayer.transform.position.y, _cameraPlayer.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
