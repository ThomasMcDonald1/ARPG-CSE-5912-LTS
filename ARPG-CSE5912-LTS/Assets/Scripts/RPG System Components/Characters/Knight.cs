
    public class Knight : Player
    {
        protected override void Start()
        {
            base.Start();
            AttackRange = 2f;
            //Stats
            statScript[StatTypes.MAXHEALTH] = 11000;
            statScript[StatTypes.HEALTH] = statScript[StatTypes.MAXHEALTH];
            statScript[StatTypes.PHYATK] = 120;
            statScript[StatTypes.PHYDEF] = 30;
            statScript[StatTypes.ATKSPD] = 12;
        }

        protected override void Update()
        {
            base.Update();
        }
    }

