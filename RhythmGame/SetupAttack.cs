using GXPEngine;

public partial class Setup : Game
{
    public void LoadEvent(string key, IEventOwner owner, float atX = -1, float atY = -1)
    {
        #region Position sign
        if (atX == -1) atX = owner.x;
        if (atY == -1) atY = owner.y;
        #endregion

        #region Switching by key
        switch (key)
        {
            case "ExampleCircleAttack":
                ExampleCircleAttack();
                break;
            case "WallAttack":
                WallAttack();
                break;
            case "CrossAttack":
                CrossAttack();
                break;
            default:
                break;
        }
        #endregion

        #region Attack Setup
        void ExampleCircleAttack()
        {
            float angle = 0;
            for (int i = 0; i < 16; i++)
            {
                Bullet bullet = ObjectPool<Bullet>.GetInstance(typeof(Bullet)).GetObject(owner);

                bullet.SetXY(atX, atY);
                bullet.rotation = angle;
                angle += 0.5708f;
            }
        }
        void WallAttack()
        {
            int bulletWall = 740;
            for (int i = 0; i < 30; i++)
            {
                Bullet bullet = ObjectPool<Bullet>.GetInstance(typeof(Bullet)).GetObject(owner);

                bullet.SetXY(0, bulletWall);
                bullet.rotation = 1.5708f;
                bulletWall -= 30;

            }
        }
        void CrossAttack()
        {
            float angle = 0.785398163f;
            for (int i = 0; i < 50; i++)
            {
                Bullet bullet = ObjectPool<Bullet>.GetInstance(typeof(Bullet)).GetObject(owner);

                bullet.SetXY(atX, atY);
                bullet.rotation = angle;
                angle += 90 * 3.14159265359f / 180;

            }
        }
        #endregion
    }
}