using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health = 100;
    public Slider HealthSlider;
    private void Update()
    {
        HealthSlider.value = Health;
        Debug.Log(Health);
        if(Health  <= 0)
        {
            Destroy(HealthSlider.gameObject);
        }
    }
}
