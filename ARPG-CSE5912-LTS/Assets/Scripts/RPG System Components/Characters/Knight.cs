
    public class Knight : Player
    {
        protected override void Start()
        {
            base.Start();
            AttackRange = 2f;
            //Stats
            stats[StatTypes.MAXHEALTH] = 11000;
            stats[StatTypes.HEALTH] = stats[StatTypes.MAXHEALTH];
            stats[StatTypes.PHYATK] = 120;
            stats[StatTypes.PHYDEF] = 30;
            stats[StatTypes.ATKSPD] = 12;
        }

        protected override void Update()
        {
            base.Update();
        }
    }

