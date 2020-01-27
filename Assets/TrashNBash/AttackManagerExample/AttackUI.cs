using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AttackUI : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    Image image;

    [SerializeField]
    Text inputText;

    [SerializeField]
    Text nameText;

    [SerializeField]
    Image cooldown;

    AttackManager.Attack attack;

    void Start()
    {
        
    }

    public void AttackStart()
    {
        button.interactable = false;
    }

    public void AttackComplete()
    {
        button.interactable = true;
    }

    public void Cooldown(float value)
    {
        cooldown.fillAmount = value;
    }

    public void Setup(AttackManager.Attack attack)
    {
        this.attack = attack;
        button.onClick.AddListener(delegate { AttackManager.Instance.StartAttack(attack); });
        inputText.text = attack.input.ToString();
        nameText.text = attack.name;
        image.sprite = attack.sprite;
    }
}
