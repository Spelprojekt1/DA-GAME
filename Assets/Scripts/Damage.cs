public struct Damage
{
    // Damage
    public float Dam;
    // Shield modifier
    public float Smod;
    // Armor modifier
    public float Amod;
    public Damage(float damage, float shieldModifier, float armorModifier)
    {
        Dam = damage;
        Smod = shieldModifier;
        Amod = armorModifier;
    }
}