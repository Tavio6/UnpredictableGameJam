using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrbDisplayUI : MonoBehaviour
{
    public GameObject displayPanel;
    public TextMeshProUGUI orbText;
    public Image orbIcon;
    public float displayDuration = 2f;

    private void Awake()
    {
        displayPanel.SetActive(false);
    }

    public void ShowOrb(Orb orb)
    {
        displayPanel.SetActive(true);
        orbText.text = $"You've got {orb.name}!";
        orbIcon.sprite = orb.icon;
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), displayDuration);
    }

    void Hide()
    {
        displayPanel.SetActive(false);
    }
}