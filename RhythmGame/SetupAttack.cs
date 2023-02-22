using GXPEngine;

public partial class Setup : Game
{
    public void LoadEvent(string key, IEventOwner owner)
    { 
        #region Switching by key
        switch (key)
        {
            case "CircleAttack":
                CircleAttack();
                break;
            case "ChaoticAttack":
                ChaoticAttack(0);
                break;
            case "ChaoticAttack2":
                ChaoticAttack(2.09f);
                break;
            case "LaserAttack":
                LaserAttack();
                break;
            case "LaserAttackReverse":
                LaserAttackReverse();
                break;
            case "CrossAttack":
                CrossAttack(1);
                break;
            case "SlowCrossAttack":
                SlowCrossAttack(1);
                break;
            case "SlowCrossAttack2":
                SlowCrossAttack2(1);
                break;
            case "RhythmBattleSmall":
                RhythmBattle(10,0.5f,10);
                break;
            case "SCA1": SubCrossAttack(0.785398163f + Time.time / 1000);
                break;
            case "SCA2": SubCrossAttack(0.55f + Time.time / 1000);
                break;
            case "SCA3": SubCrossAttack(0.22f + Time.time / 1000);
                break;
            case "SCA4": SubCrossAttack(0 + Time.time / 1000);
                break;
            case "SCA5": SubCrossAttack(-0.22f + Time.time / 1000);
                break;
            case "SCA6": SubCrossAttack(-0.55f + Time.time / 1000);
                break;
            case "SCA1s": SubCrossAttack(0.785398163f + Time.time / 1000, true);
                break;
            case "SCA2s": SubCrossAttack(0.55f + Time.time / 1000, true);
                break;
            case "SCA3s": SubCrossAttack(0.22f + Time.time / 1000, true);
                break;
            case "SCA4s": SubCrossAttack(0 + Time.time / 1000, true);
                break;
            case "SCA5s": SubCrossAttack(-0.22f + Time.time / 1000, true);
                break;
            case "SCA6s": SubCrossAttack(-0.55f + Time.time / 1000, true);
                break;
            default:
                break;
        }
        #endregion

        #region Attack Setup
        void CircleAttack()
        {
            float angle = 0;
            for (int i = 0; i < 11; i++)
            {
                Bullet bullet = ObjectPool<Bullet>.GetInstance(typeof(Bullet)).GetObject(owner);

                bullet.SetXY(owner.x, owner.y);
                bullet.rotation = angle;
                angle += 0.5708f;
            }
        }
        void ChaoticAttack(float angle)
        {
            for (int i = 0; i < 3; i++)
            {
                ChaoticBullet bullet = ObjectPool<ChaoticBullet>.GetInstance(typeof(ChaoticBullet)).GetObject(owner);

                bullet.SetXY(owner.x, owner.y);
                bullet.rotation = angle;
                angle += 2.09f;
            }
        }
        void LaserAttack()
        {
            LaserBullet bullet = ObjectPool<LaserBullet>.GetInstance(typeof(LaserBullet)).GetObject(owner);
            bullet.SetXY(0, height / 2);
            bullet.rotation = 1.57f;
        }
        void LaserAttackReverse()
        {
            LaserBullet bullet = ObjectPool<LaserBullet>.GetInstance(typeof(LaserBullet)).GetObject(owner);
            bullet.SetXY(1240, height/2);
            bullet.rotation = -90 * Mathf.PI / 180;
        }
        void CrossAttack(int longness)
        {
            for (int i = 0; i < longness; i++)
            {
                owner.AddEventThere(new EventData("SCA1", .1f));
                owner.AddEventThere(new EventData("SCA2", .1f));
                owner.AddEventThere(new EventData("SCA3", .1f));
                owner.AddEventThere(new EventData("SCA4", .1f));
                owner.AddEventThere(new EventData("SCA5", .1f));
                owner.AddEventThere(new EventData("SCA6", .1f));
            }
        }
        void SlowCrossAttack(int longness)
        {
            for (int i = 0; i < longness; i++)
            {
                owner.AddEventThere(new EventData("SCA1s", .1f));
                owner.AddEventThere(new EventData("SCA2s", .1f));
                owner.AddEventThere(new EventData("SCA3s", .1f));
                owner.AddEventThere(new EventData("SCA4s", .1f));
                owner.AddEventThere(new EventData("SCA5s", .1f));
                owner.AddEventThere(new EventData("SCA6s", .1f));
            }
        }
        void SlowCrossAttack2(int longness)
        {
            for (int i = 0; i < longness; i++)
            {
                owner.AddEventThere(new EventData("SCA1s", .1f));
                owner.AddEventThere(new EventData("SCA2s", .1f));
                owner.AddEventThere(new EventData("SCA3s", .1f));
                owner.AddEventThere(new EventData("SCA4s", .1f));
                owner.AddEventThere(new EventData("SCA5s", .1f));
                owner.AddEventThere(new EventData("SCA6s", .1f));
                owner.AddEventThere(new EventData("SCA5s", .1f));
                owner.AddEventThere(new EventData("SCA4s", .1f));
                owner.AddEventThere(new EventData("SCA3s", .1f));
                owner.AddEventThere(new EventData("SCA2s", .1f));
                owner.AddEventThere(new EventData("SCA1s", .1f));
                owner.AddEventThere(new EventData("SCA2s", .1f));
                owner.AddEventThere(new EventData("SCA3s", .1f));
                owner.AddEventThere(new EventData("SCA2s", .1f));
                owner.AddEventThere(new EventData("SCA1s", .1f));
                owner.AddEventThere(new EventData("SCA1s", .1f));

            }
        }
        void SubCrossAttack(float angle, bool isSlow = false)
        {
            if (!isSlow)
            {
                for (int i = 0; i < 4; i++)
                {
                    FastBullet bullet = ObjectPool<FastBullet>.GetInstance(typeof(FastBullet)).GetObject(owner);
                    bullet.SetXY(owner.x, owner.y);
                    bullet.rotation = angle;
                    angle += 90;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Bullet bullet = ObjectPool<Bullet>.GetInstance(typeof(Bullet)).GetObject(owner);
                    bullet.SetXY(owner.x, owner.y);
                    bullet.rotation = angle;
                    angle += 90 * Mathf.PI / 180;
                }
            }
        }
        void RhythmBattle(float speed, float noteDelay, float timeToEnd)
        {
            if(!Level.TwoPlayers)
                new RhythmBattle(speed, noteDelay, timeToEnd, 0);
            else
            {
                new RhythmBattle(speed, noteDelay, timeToEnd, 0);
                new RhythmBattle(speed, noteDelay, timeToEnd, 1);
            }
        }
        #endregion
    }
}