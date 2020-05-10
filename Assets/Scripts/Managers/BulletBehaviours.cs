using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BulletBehaviours
{
    public BulletBehaviours()
    {

    }

    public void BulletAct(IBullet bullet)
    {
        switch (bullet.BulletType)
        {
            case BulletType.BulletType1:
                {
                    VerticalBulletMoving(bullet);
                    break;
                }
            case BulletType.BulletType2:
                {
                    LeftDiagonalBulletMoving(bullet);
                    break;
                }
            case BulletType.BulletType3:
                {
                    RightDiagonalBulletMoving(bullet);
                    break;
                }
        }
    }

    void VerticalBulletMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(0, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(0, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.BulletGameObject.transform.position + distanceDown : bullet.BulletGameObject.transform.position - distanceUp;
        bullet.BulletRigidBody2D.MovePosition(Vector2.MoveTowards(bullet.BulletGameObject.transform.position, newBulletPosition, 1f));
    }
    void LeftDiagonalBulletMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(-bullet.MoveSpeed * Time.fixedDeltaTime, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(bullet.MoveSpeed * Time.fixedDeltaTime, -bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.BulletGameObject.transform.position + distanceUp : bullet.BulletGameObject.transform.position + distanceDown;

        bullet.BulletRigidBody2D.MovePosition(Vector2.MoveTowards(bullet.BulletGameObject.transform.position, newBulletPosition, 1f));
    }
    void RightDiagonalBulletMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(bullet.MoveSpeed * Time.fixedDeltaTime, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(-bullet.MoveSpeed * Time.fixedDeltaTime, -bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.BulletGameObject.transform.position + distanceUp : bullet.BulletGameObject.transform.position + distanceDown;

        bullet.BulletRigidBody2D.MovePosition(Vector2.MoveTowards(bullet.BulletGameObject.transform.position, newBulletPosition, 1f));
    }
}
