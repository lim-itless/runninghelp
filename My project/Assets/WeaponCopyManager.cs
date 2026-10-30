using System.Collections.Generic;
using UnityEngine;

public struct NormalWeapon
{
    public string name;
    public int power;
}

public class LegendaryWeapon
{
    public string name;
    public int power;
}

public struct BaseStats
{
    public int hp;
    public int atk;
}

public class SkillSet
{
    public string skillName;
    public float multiplier;
}

public class MonsterTemplate
{
    public string name;
    public BaseStats stats;
    public List<SkillSet> skills = new List<SkillSet>();

    public MonsterTemplate Clone()
    {
        MonsterTemplate clone = new MonsterTemplate();

        clone.name = name;
        clone.stats = stats;
        clone.skills = new List<SkillSet>();

        foreach (SkillSet skill in skills)
        {
            SkillSet copiedSkill = new SkillSet{skillName = skill.skillName, multiplier = skill.multiplier};
            clone.skills.Add(copiedSkill);
        }

        return clone;
    }
}

public class WeaponCopyManager : MonoBehaviour
{
    private LegendaryWeapon original;
    private LegendaryWeapon linked;

    void Start()
    {
        Debug.Log("Q : Struct 복사 / W : Class 공유 / E : 참조 해제 / D : Deep Copy 테스트");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TestStructCopy();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            TestClassReference();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TestNullReference();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TestDeepCopy();
        }
    }

    void TestStructCopy()
    {
        NormalWeapon original = new NormalWeapon{name = "철검", power = 10};
        NormalWeapon copy = original;
        copy.power = 50;

        Debug.Log("===== Struct 복사 테스트 =====");
        Debug.Log($"원본 : {original.name} / 공격력 : {original.power}");
        Debug.Log($"복사본 : {copy.name} / 공격력 : {copy.power}");

        bool passed = original.power == 10 && copy.power == 50;

        Debug.Log(passed ? "PASS : 복사본을 수정해도 원본은 그대로입니다." : "FAIL : 복사 결과를 확인하세요.");
    }

    void TestClassReference()
    {
        original = new LegendaryWeapon{name = "영혼의 검", power = 100};

        linked = original;
        linked.power = 999;

        Debug.Log("===== Class 공유 테스트 =====");
        Debug.Log($"원본 공격력 : {original.power}");
        Debug.Log($"연결된 무기 공격력 : {linked.power}");
        Debug.Log($"같은 객체인가? {object.ReferenceEquals(original, linked)}");
    }

    void TestNullReference()
    {
        if (original == null)
        {
            Debug.Log("먼저 W를 눌러 전설 무기를 생성해주세요.");
            return;
        }

        linked = null;

        Debug.Log("===== 참조 해제 테스트 =====");
        Debug.Log($"linked가 null인가? {linked == null}");
        Debug.Log($"original로 접근 가능: {original.name} / 공격력: {original.power}");
        Debug.Log("linked의 연결만 끊겼습니다. original이 여전히 참조하므로 객체는 유지됩니다.");
    }

    void TestDeepCopy()
    {
        MonsterTemplate template = new MonsterTemplate
        {
            name = "드래곤",
            stats = new BaseStats{hp = 1000, atk = 100}
        };

        template.skills.Add(new SkillSet{skillName = "화염방사", multiplier = 1.5f});

        MonsterTemplate instance1 = template.Clone();

        instance1.name = "드래곤_인스턴스1";
        instance1.stats.hp = 500;
        instance1.skills[0].multiplier = 9.9f;

        Debug.Log("===== Deep Copy 테스트 =====");
        Debug.Log($"원본 : {template.name} / HP : {template.stats.hp} / 스킬 배율 : {template.skills[0].multiplier:F1}");
        Debug.Log($"복사본 : {instance1.name} / HP : {instance1.stats.hp} / 스킬 배율 : {instance1.skills[0].multiplier:F1}");

        bool separateMonster = !object.ReferenceEquals(template, instance1);
        bool separateList = !object.ReferenceEquals(template.skills, instance1.skills);
        bool separateSkill = !object.ReferenceEquals(template.skills[0], instance1.skills[0]);
        bool passed = template.stats.hp == 1000 && template.skills[0].multiplier == 1.5f && instance1.stats.hp == 500 && instance1.skills[0].multiplier == 9.9f && separateMonster && separateList && separateSkill;

        Debug.Log($"몬스터 객체가 서로 다른가? {separateMonster}");
        Debug.Log($"리스트가 서로 다른가? {separateList}");
        Debug.Log($"내부 스킬 객체가 서로 다른가? {separateSkill}");
        Debug.Log(passed ? "PASS : 원본과 복사본이 독립적입니다." : "FAIL : 복사 로직을 확인하세요.");
    }
}