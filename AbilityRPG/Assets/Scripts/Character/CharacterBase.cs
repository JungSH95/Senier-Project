[System.Serializable]
public class CharacterBase
{
    public int strLevel;     // 근력 (공격력에 영향)
    public int dexLevel;     // 민첩 (공격 속도에 영향)
    public int conLevel;     // 건강 (체력에 영향)

    public int dexMaxLevel;     // 민첩 (공격 속도에 영향)

    public int strExp;
    public int dexExp;
    public int conExp;

    public string weapon;

    public int weaponLevel;
    public int weaponExp;
    
    public float maxHp;
    public float currentHp;

    public float damage;

    public float atkSpeed;
    public float speed;

    public CharacterBase(string weaponName, bool master)
    {
        strLevel = 1;
        dexLevel = 1;
        conLevel = 1;

        dexMaxLevel = 10;

        strExp = 0;
        dexExp = 0;
        conExp = 0;

        weapon = weaponName;
        weaponLevel = 1;
        weaponExp = 0;

        maxHp = 100f;
        currentHp = maxHp;

        damage = 10f;

        atkSpeed = 1.5f;
        speed = 2f;

        if(master)
        {
            strLevel = 10;
            dexLevel = 10;
            conLevel = 10;
            weaponLevel = 50;

            SetStatus();
        }
    }

    public void StrLevelUp()
    {
        strExp = 0;
        strLevel += 1;

        SetStatus();
    }

    public void DexLevelUp()
    {
        dexExp = 0;
        dexLevel += 1;

        SetStatus();
    }

    public void ConLevelUp()
    {
        conExp = 0;
        conLevel += 1;

        SetStatus();
    }

    public void WeaponLevelUp()
    {
        weaponExp = 0;
        weaponLevel += 1;

        SetStatus();
    }

    public void SetStatus()
    {
        maxHp = 100f + (conLevel - 1) * 1f;
        atkSpeed = 1.5f + dexLevel * 0.1f;
        damage = 10f + (strLevel - 1) * 0.1f + (weaponLevel - 1) * 1f;
    }
}
