using UnityEngine;

namespace DesignPatternCourse.StrategyWithScriptableObjects
{
    public class Airplane : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField]
        GameActionControlledMovement moveActionForward = null;

        [SerializeField]
        GameActionControlledMovement moveActionBackward = null;

        [SerializeField]
        GameActionControlledMovement moveActionRight = null;

        [SerializeField]
        GameActionControlledMovement moveActionLeft = null;

        [Header("Fire")]
        [SerializeField]
        GameActionSpawner spawner = null;

        [SerializeField]
        GameObject simpleBullet = null;

        [SerializeField]
        GameActionPeriodicSpawner simpleBulletSpawner = null;


        [SerializeField]
        GameObject missile = null;

        [SerializeField]
        GameActionPeriodicSpawner missileSpawner = null;

        [SerializeField]
        GameObject bomb = null;

        [SerializeField]
        GameActionPeriodicSpawner bombSpawner = null;


        private void Update()
        {
            Move();

            Fire(0, simpleBullet, simpleBulletSpawner);
            Fire(1, missile, missileSpawner);
            Fire(2, bomb, bombSpawner);
        }

        private void Fire(int mouseButton, GameObject pref, GameActionPeriodicSpawner periodicSpawner)
        {
            if (Input.GetMouseButtonDown(mouseButton))
                spawner.Do(new GameObjectPosition(pref, SpawnPosition()));
            if (Input.GetMouseButton(mouseButton))
                periodicSpawner.Do(new GameObjectPosition(pref, SpawnPosition()));
        }

        private Vector3 SpawnPosition()
        {
            return transform.position + transform.forward * 5f;
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.W))
                moveActionForward.Do(transform);

            if (Input.GetKey(KeyCode.S))
                moveActionBackward.Do(transform);

            if (Input.GetKey(KeyCode.A))
                moveActionLeft.Do(transform);

            if (Input.GetKey(KeyCode.D))
                moveActionRight.Do(transform);
        }
    }
}