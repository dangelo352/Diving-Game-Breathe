using UnityEngine;
using MoreMountains.Tools;

public class CharacterOxygen : MonoBehaviour 
{
    public MMProgressBar OxygenBar; // Assign your MMProgressBar in the inspector
    public float OxygenDepletionRate = 0.5f; // How fast the oxygen depletes per second
    public float OxygenRefillRate = 1f; // How fast the oxygen refills per second
    private float _currentOxygenLevel = 1f; // Start with full oxygen (1 is full, 0 is empty)

    private void Start()
    {
        OxygenBar.UpdateBar(_currentOxygenLevel, 0f, 1f);
    }

    private void Update()
    {
        // If the character is underwater, deplete oxygen
        if (IsUnderwater())
        {
            DepleteOxygen(OxygenDepletionRate * Time.deltaTime);
        }
        else
        {
            // If not underwater, refill oxygen
            RefillOxygen(OxygenRefillRate * Time.deltaTime);
        }

	}
    // Call this method to decrease the oxygen level
    protected void DepleteOxygen(float amount)
    {
        _currentOxygenLevel = Mathf.Max(_currentOxygenLevel - amount, 0);
        OxygenBar.UpdateBar(_currentOxygenLevel, 0f, 1f);
    }

    // Call this method to increase the oxygen level
    protected void RefillOxygen(float amount)
    {
        _currentOxygenLevel = Mathf.Min(_currentOxygenLevel + amount, 1f);
        OxygenBar.UpdateBar(_currentOxygenLevel, 0f, 1f);
    }
	

    // Implement this method to check if the character is underwater
    protected bool IsUnderwater()
    {
        // Replace with your actual logic to determine if the character is underwater
        // For example, you might check the character's y-position or if they're colliding with water
        return true; // Placeholder: Replace with actual condition
    }
}
