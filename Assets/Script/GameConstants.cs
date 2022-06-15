using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstant", menuName = "ScriptableObject/GameConstants", order = 0)]
public class GameConstants : ScriptableObject {
    // for scoring system
    int currentScore;
    int currentPlayerHealth;

    // for Reset values
    Vector3 goombaSpawnPointStart = new Vector3(-2.5f, -0.45f, 0); // hardcoded location
    Vector3 greenEnemySpawnPointStart = new Vector3(-5f, -0.45f, 0); 

    // for Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    // for Break.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    // for SpawnDebris.cs
    public int spawnNumberOfDebris = 5;

    // for Rotator.cs
    public int rotatorRotateSpeed = 6;

    // for testing
    public int testValue;
    
    // for enemy
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;

    // global ground position
    public float groundSurface = -5.622402f;

    // for SpawnConsummable
    public int spawnTimeStep = 2;

}

