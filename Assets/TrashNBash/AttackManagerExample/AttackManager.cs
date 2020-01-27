using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Malee;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance;

    [SerializeField]
    AttackUI attackUITemplate;

    [System.Serializable]
    public class Attack
    {
        public string name;
        public string UIName;
        public KeyCode input;
        public float cooldown;
        public Sprite sprite;
        public UnityEvent action;
        [HideInInspector] public AttackUI UI;
        [HideInInspector] public bool canAttack;
    }

    [Reorderable, SerializeField]
    AttackList attacks;

    [System.Serializable]
    class AttackList : ReorderableArray<Attack> { }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach(Attack attack in attacks)
        {
            attack.canAttack = true;
            AttackUI attackUI = Instantiate(attackUITemplate, attackUITemplate.transform.parent);
            attack.UI = attackUI;
            attackUI.Setup(attack);
        }

        // turn off template
        attackUITemplate.gameObject.SetActive(false);
    }

    void Update()
    {
        // attack input
        foreach(Attack attack in attacks)
        {
            if (Input.GetKeyDown(attack.input))
                StartAttack(attack);
        }
    }

    public void StartAttack(Attack attack)
    {
        if (attack.canAttack)
        {
            attack.UI.AttackStart();
            attack.action.Invoke();
            StartCoroutine(AttackCooldown(attack));
            attack.UI.AttackComplete();
        }
    }

    IEnumerator AttackCooldown(Attack attack)
    {
        attack.canAttack = false;

        float t = 0;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / attack.cooldown;
            attack.UI.Cooldown(1f - t);
            yield return new WaitForFixedUpdate();
        }

        attack.canAttack = true;
    }

    public void AttackExample()
    {
        print("Attack");
    }
}
