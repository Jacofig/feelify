public abstract class StatusEffect
{
    public Creature owner;

    /// <summary>
    /// Czy status wygasa na starcie tury w³aœciciela
    /// </summary>
    public virtual bool ExpireOnOwnerTurnStart => false;

    public virtual void OnApply() { }
    public virtual void OnRemove() { }

    /// <summary>
    /// Absorbuje dmg PRZED defense (np. block)
    /// </summary>
    public virtual int AbsorbDamage(int dmg)
    {
        return dmg;
    }

    public virtual bool Expired => false;
}
