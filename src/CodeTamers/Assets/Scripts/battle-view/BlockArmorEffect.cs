using UnityEngine;

public class BlockArmorEffect : StatusEffect
{
    private int armorHP;

    public BlockArmorEffect(int armorHP)
    {
        this.armorHP = armorHP;
    }

    public override bool ExpireOnOwnerTurnStart => true;

    public override int AbsorbDamage(int dmg)
    {
        if (armorHP <= 0)
            return dmg;

        int absorbed = Mathf.Min(armorHP, dmg);
        armorHP -= absorbed;

        Debug.Log($"{owner.data.pokemonName} blocks {absorbed} dmg");

        return dmg - absorbed;
    }

    public override bool Expired => armorHP <= 0;
}
