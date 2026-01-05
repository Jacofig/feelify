using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Data")]
    public PokemonData data;
    public int teamIndex;
    public string codeBuffer = "";

    // Expose read-only to everyone else (UI can read, but nobody can write)
    public int CurrentHP { get; private set; }
    public int CurrentMana { get; private set; }

    [Header("Status")]
    public List<StatusEffect> statusEffects = new();

    // =========================
    // VISUALS
    // =========================
    Animator animator;
    SpriteRenderer spriteRenderer;

    // =========================
    // UNITY
    // =========================
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // =========================
    // INIT
    // =========================
    public void Init(PokemonData newData)
    {
        data = newData;

        CurrentHP = data.maxHP;
        CurrentMana = Mathf.Clamp(data.startingMana, 0, data.maxMana);

        if (spriteRenderer != null && data.battleSprite != null)
            spriteRenderer.sprite = data.battleSprite;

        if (animator != null && data.animatorOverride != null)
            animator.runtimeAnimatorController = data.animatorOverride;

    }

    // =========================
    // DAMAGE (final correct place)
    // =========================
    public int TakeDamage(int rawDamage)
    {
        if (rawDamage <= 0 || CurrentHP <= 0)
            return 0;

        int dmg = rawDamage;

        // Let statuses modify / absorb the damage here (centralized!)
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            var status = statusEffects[i];
            dmg = status.AbsorbDamage(dmg);

            if (status.Expired)
            {
                status.OnRemove();
                statusEffects.RemoveAt(i);
            }

            if (dmg <= 0)
                break;
        }

        if (dmg <= 0)
            return 0;

        // Apply final damage
        CurrentHP -= dmg;

        // Visual feedback
        if (animator != null)
            animator.SetTrigger("takeDamage");

        StartCoroutine(HitKick());

        if (CurrentHP <= 0)
            Die();

        return dmg;
    }

    IEnumerator HitKick()
    {
        Vector3 start = transform.localPosition;
        transform.localPosition += Vector3.left * 0.05f;
        yield return new WaitForSeconds(0.05f);
        transform.localPosition = start;
    }

    // =========================
    // MANA
    // =========================
    public void ResetMana()
    {
        CurrentMana = data.maxMana;
    }

    public bool TrySpendMana(int amount)
    {
        if (amount <= 0) return true;
        if (CurrentMana < amount) return false;

        CurrentMana -= amount;
        return true;
    }

    // =========================
    // DEATH
    // =========================
    void Die()
    {
        CurrentHP = 0;

        if (spriteRenderer != null)
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.4f);
    }

    // =========================
    // STATUS EFFECTS
    // =========================
    public void AddStatus(StatusEffect status)
    {
        status.owner = this;
        statusEffects.Add(status);
        status.OnApply();
    }

    public void OnTurnStart()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            var status = statusEffects[i];

            if (status.ExpireOnOwnerTurnStart)
            {
                status.OnRemove();
                statusEffects.RemoveAt(i);
            }
        }
    }
}
