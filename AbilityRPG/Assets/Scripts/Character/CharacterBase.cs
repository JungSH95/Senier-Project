[System.Serializable]
public class CharacterBase
{
    public int strLevel;     // 근력 (공격력에 영향)
    public int dexLevel;     // 민첩 (공격 속도에 영향)
    public int conLevel;     // 건강 (체력에 영향)

    public int strExp;
    public int dexExp;
    public int conExp;

    public string weapon;

    public int weaponLevel;
    public int weaponExp;
    
    public float maxHp;
    public float currentHp;

    public float damage;

    public float speed;

    public CharacterBase(string weaponName)
    {
        strLevel = 1;
        dexLevel = 1;
        conLevel = 1;

        strExp = 0;
        dexExp = 0;
        conExp = 0;

        weapon = weaponName;
        weaponExp = 0;

        maxHp = 100f;
        currentHp = maxHp;

        damage = 10f;

        speed = 2f;
    }

    public void AddStrExp(int exp)
    {
        strExp += exp;
    }
}
