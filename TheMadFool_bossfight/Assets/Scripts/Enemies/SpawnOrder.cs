using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnOrder : MonoBehaviour {

    public Spawner spawner;
    int enemyCount;

    public Text startText;
    public Text enemyCountText;
    public Text bossText;
    public Font myFont;
    bool showText = true;
    bool showEnemyText = false;
    bool showBossText = false;

    int waveCount = 1;

    // Use this for initialization
    void Start ()
    {
        startText.text = "";
        enemyCountText.text = "";
        bossText.text = "";
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;

        if (enemyCount == 0 && spawner.numWaves > spawner.totalWaves)
        {
            waveCount++;
            spawner.numWaves = 0;
            spawner.totalWaves = 0;
            spawner.totalEnemy = 0;
            spawner.spawnedEnemy = 0;
            spawner.Spawn = true;
            Debug.Log("wavecount = " + waveCount);
        }

        if (spawner.Spawn)
        { 

           if (waveCount == 1)
            {
                spawner.enemyLevel = Spawner.EnemyLevels.Easy;
                spawner.totalEnemy = 1;
                spawner.totalWaves = 5;
                enemyCount = 5;
            }
            if (waveCount == 2)
            {
                spawner.enemyLevel = Spawner.EnemyLevels.Medium;
                spawner.totalEnemy = 1;
                enemyCount = 5;
            }
            if (waveCount == 3)
            {
                spawner.enemyLevel = Spawner.EnemyLevels.Hard;
                spawner.totalEnemy = 1;
                enemyCount = 5;
            }
            if (waveCount == 4)
            {
                spawner.enemyLevel = Spawner.EnemyLevels.Boss;
                spawner.spawnType = Spawner.SpawnTypes.Once;
                spawner.totalEnemy = 1;
                enemyCount = 1;
                showEnemyText = false;
                showBossText = true;
            }
        }
	}

    void OnGUI()
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.font = myFont;
        myButtonStyle.normal.textColor = Color.white;
        myButtonStyle.hover.textColor = Color.cyan;

        if (showText)
        {
            startText.enabled = true;
            startText.text = "ARE YOU READY TO FIGHT??";

            if (GUI.Button(new Rect(410, 380, 120, 30), "YES!", myButtonStyle))
            {
                startText.enabled = false;
                showText = false;
                spawner.Spawn = true;
                showEnemyText = true;
            }
        }

        if (showEnemyText)
        {
            enemyCountText.text = ("Wave " + waveCount + "!!     Enemies left : " + enemyCount + " / 5");
        }
        else
        {
            enemyCountText.text = "";
        }

        if (showBossText)
        {
            bossText.text = ("BOSSFIGHT!");
        }
    }
}