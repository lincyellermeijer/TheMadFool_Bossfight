﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    //-----------------------------------
    // All the Enumerators
    //-----------------------------------
    // Spawn types
    public enum SpawnTypes
    {
        Normal,
        Once,
        Wave,
        TimedWave
    }
    // The different Enemy levels
    public enum EnemyLevels
    {
        Easy,
        Medium,
        Hard,
        Boss
    }
    //---------------------------------
    // End of the Enumerators
    //---------------------------------

    // Enemy level to be spawnedEnemy
    public EnemyLevels enemyLevel = EnemyLevels.Easy;

    //----------------------------------
    // Enemy Prefabs
    //----------------------------------
    [HideInInspector]
    public GameObject Enemy;

    public GameObject EasyEnemy;
    public GameObject MediumEnemy;
    public GameObject HardEnemy;
    public GameObject BossEnemy;
    private Dictionary<EnemyLevels, GameObject> Enemies = new Dictionary<EnemyLevels, GameObject>(4);
    //----------------------------------
    // End of Enemy Prefabs
    //----------------------------------

    //----------------------------------
    // Enemies and how many have been created and how many are to be created
    //----------------------------------
    public int totalEnemy = 0;
    [HideInInspector]
    public int numEnemy = 0;
    [HideInInspector]
    public int spawnedEnemy = 0;
    //----------------------------------
    // End of Enemy Settings
    //----------------------------------

    // The ID of the spawner
    private int SpawnID;

    //----------------------------------
    // Different Spawn states and ways of doing them
    //----------------------------------
    [HideInInspector]
    public bool waveSpawn = false;
    public bool Spawn = true;
    public SpawnTypes spawnType = SpawnTypes.TimedWave;
    // timed wave controls
    public float waveTimer = 30.0f;
    private float timeTillWave = 0.0f;
    //Wave controls
    public int totalWaves = 0;
    [HideInInspector]
    public int numWaves = 0;
    //----------------------------------
    // End of Different Spawn states and ways of doing them
    //----------------------------------
    void Start()
    {
        // sets a random number for the id of the spawner
        SpawnID = Random.Range(1, 500);
        Enemies.Add(EnemyLevels.Easy, EasyEnemy);
        Enemies.Add(EnemyLevels.Boss, BossEnemy);
        Enemies.Add(EnemyLevels.Medium, MediumEnemy);
        Enemies.Add(EnemyLevels.Hard, HardEnemy);
    }
    void Update()
    {
        if (Spawn)
        {
            // Spawns enemies everytime one dies
            if (spawnType == SpawnTypes.Normal)
            {
                // checks to see if the number of spawned enemies is less than the max num of enemies
                if (numEnemy < totalEnemy)
                {
                    // spawns an enemy
                    spawnEnemy();
                }
            }
            // Spawns enemies only once
            else if (spawnType == SpawnTypes.Once)
            {
                // checks to see if the overall spawned num of enemies is more or equal to the total to be spawned
                if (spawnedEnemy >= totalEnemy)
                {
                    //sets the spawner to false
                    Spawn = false;
                }
                else
                {
                    // spawns an enemy
                    spawnEnemy();
                }
            }
            //spawns enemies in waves, so once all are dead, spawns more
            else if (spawnType == SpawnTypes.Wave)
            {
                if (numWaves < totalWaves + 1)
                {
                    if (waveSpawn)
                    {
                        //spawns an enemy
                        spawnEnemy();
                    }
                    if (numEnemy == 0)
                    {
                        // enables the wave spawner
                        waveSpawn = true;
                        //increase the number of waves
                        numWaves++;
                    }
                    if (numEnemy == totalEnemy)
                    {
                        // disables the wave spawner
                        waveSpawn = false;
                    }
                }
            }
            // Spawns enemies in waves but based on time.
            else if (spawnType == SpawnTypes.TimedWave)
            {
                // checks if the number of waves is bigger than the total waves
                if (numWaves <= totalWaves)
                {
                    // Increases the timer to allow the timed waves to work
                    timeTillWave += Time.deltaTime;
                    if (waveSpawn)
                    {
                        //spawns an enemy
                        spawnEnemy();
                    }
                    // checks if the time is equal to the time required for a new wave
                    if (timeTillWave >= waveTimer)
                    {
                        // enables the wave spawner
                        waveSpawn = true;
                        // sets the time back to zero
                        timeTillWave = 0.0f;
                        // increases the number of waves
                        numWaves++;
                        // A hack to get it to spawn the same number of enemies regardless of how many have been killed
                        numEnemy = 0;
                    }
                    if (numEnemy >= totalEnemy)
                    {
                        // diables the wave spawner
                        waveSpawn = false;
                    }
                }
                else
                {
                    Spawn = false;
                }
            }
        }
    }
    // spawns an enemy based on the enemy level that you selected
    private void spawnEnemy()
    {
        Enemy = (GameObject)Instantiate(Enemies[enemyLevel], gameObject.transform.position, Quaternion.identity);
        Enemy.SendMessage("setName", SpawnID);
        // Increase the total number of enemies spawned and the number of spawned enemies
        numEnemy++;
        spawnedEnemy++;
    }
    // Call this function from the enemy when it "dies" to remove an enemy count
    public void killEnemy(int sID)
    {
        // if the enemy's spawnId is equal to this spawnersID then remove an enemy count
        if (SpawnID == sID)
        {
            numEnemy--;
        }
    }
    //enable the spawner based on spawnerID
    public void enableSpawner(int sID)
    {
        if (SpawnID == sID)
        {
            Spawn = true;
        }
    }
    //disable the spawner based on spawnerID
    public void disableSpawner(int sID)
    {
        if (SpawnID == sID)
        {
            Spawn = false;
        }
    }
    // returns the Time Till the Next Wave, for a interface, ect.
    public float TimeTillWave
    {
        get
        {
            return timeTillWave;
        }
    }
    // Enable the spawner, useful for trigger events because you don't know the spawner's ID.
    public void enableTrigger()
    {
        Spawn = true;
    }
}
