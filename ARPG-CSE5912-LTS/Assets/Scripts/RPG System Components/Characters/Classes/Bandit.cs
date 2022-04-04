
using ARPG.Core;

namespace ARPG.Combat
{
    public class Bandit : EnemyController
{
    protected override void Start()
    {
        base.Start();
        Range = 5f;
        BodyRange = 1f;
        SightRange = 90f;
        Speed = 3f;
        agent.speed = Speed;
        stats[StatTypes.MonsterType] = 1; //testing
        cooldownTimer = 6;
    }

    public override string GetClassTypeName()
    {
        return "Bandit";
    }


    protected override void Update()
    {
        base.Update();
    }
}

}
