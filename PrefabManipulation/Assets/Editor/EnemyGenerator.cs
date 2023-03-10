using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

public static class EnemyGenerator
{
    static int enemyCount = 20;

    // 1)
    [MenuItem("Enemy/Generate")]
    public static void Generate() {
        for (int i = 0; i < enemyCount; i++) {

            // a)

            GameObject go;
            GenerateEnemy(out go);
            System.Type aiType = GetRandomAIBase();
            Old_AIBase ai = go.AddComponent(aiType) as Old_AIBase;

            // b)

            System.Random random = new System.Random();

            System.Type type = typeof(EnumReferences.EnemyType);
            System.Array values = type.GetEnumValues();
            int index = random.Next(values.Length);

            ai.enemyType = (EnumReferences.EnemyType)values.GetValue(index);

            // c)

            ai.aiStats = GetRandomStats();

            // d)

            SetAIComponents(ai, go);

            // e)

            string name = "";
            bool newName = false;
            while (!newName && !TooManyPrefabs()) {
                name = GetName();
                newName = true;

                DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Prefabs/");
                FileInfo[] info = dir.GetFiles("*.prefab");
                for (int j = 0; j < info.Length; j++) {
                    string tempName = info[j].Name.Remove(info[j].Name.Length - 8, 7);
                    if (tempName == name)
                        newName = false;
                }
            }

            if (!TooManyPrefabs()) {
                go.name = name;



                // f)

                if (!Directory.Exists("Assets/Resources/Prefabs"))
                    AssetDatabase.CreateFolder("Assets/Resources", "Prefabs");
                string localPath = "Assets/Resources/Prefabs/" + go.name + ".prefab";

                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

                PrefabUtility.SaveAsPrefabAssetAndConnect(go, localPath, InteractionMode.UserAction);

            }
            GameObject.DestroyImmediate(go);

            AssetDatabase.Refresh();
        }
    }

    // 2)
    [MenuItem("Enemy/Fix Prefabs")]
    public static void FixPrefabs() {
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/");
        foreach (GameObject go in gameObjects) {

            GameObject temp = GameObject.Instantiate(go);
            Old_AIBase oldAI = temp.GetComponent<Old_AIBase>();
            string aiTypeName = oldAI.GetType().ToString().Remove(0, 4);
            System.Type type = System.Type.GetType(aiTypeName + ",Assembly-CSharp");
            AIBase newAI = temp.AddComponent(type) as AIBase;
            newAI.aiStats = oldAI.aiStats;
            GameObject.DestroyImmediate(oldAI);
            SetAIComponents(newAI, temp);

            string localPath = "Assets/Resources/Prefabs/" + go.name + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(temp, localPath);

            GameObject.DestroyImmediate(temp);
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Enemy/Delete")]
    public static void DeleteEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = enemies.Length - 1; i >= 0; i--) {
            GameObject.DestroyImmediate(enemies[i]);
        }
    }

    [MenuItem("Enemy/Empty Prefabs")]
    public static void EmptyPrefabs() {
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Prefabs/");
        FileInfo[] info = dir.GetFiles("*.*");
        for (int i = info.Length - 1; i >= 0; i--) {
            info[i].Delete();
        }

        AssetDatabase.Refresh();
    }

    public static void GenerateEnemy(out GameObject go) {
        go = new GameObject();
        go.tag = "Enemy"; // to be able to delete every enemies on the scene
        BoxCollider2D bc = go.AddComponent<BoxCollider2D>();
        Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
    }

    public static System.Type GetRandomAIBase() {
        DirectoryInfo dir = new DirectoryInfo("Assets/Scripts/Old Scripts/");
        FileInfo[] info = dir.GetFiles("*.*");
        List<System.Type> typeList = new List<System.Type>();
        foreach (FileInfo file in info) {
            if (file.Name != "Old_AIBase.cs" && !file.Name.EndsWith(".meta")) {
                string v = Path.GetFileNameWithoutExtension(file.Name);
                System.Type t = System.Type.GetType(v + ",Assembly-CSharp");

                typeList.Add(t);
            }
        }
        return typeList[Random.Range(0, typeList.Count)];
    }

    public static AIStats GetRandomStats() {
        AIStats[] statsList = Resources.LoadAll<AIStats>("Scriptable Assets/");
        return statsList[Random.Range(0, statsList.Length)];
    }

    public static void SetAIComponents(Old_AIBase ai, GameObject go) {
        ai.rb = go.GetComponent<Rigidbody2D>();
        ai.coli = go.GetComponent<Collider2D>();
        ai.sr = go.GetComponent<SpriteRenderer>();
    }

    public static void SetAIComponents(AIBase ai, GameObject go) {
        ai.rb = go.GetComponent<Rigidbody2D>();
        ai.coli = go.GetComponent<Collider2D>();
        ai.sr = go.GetComponent<SpriteRenderer>();
    }

    public static string GetName() {
        string[] enemyAdjectives = { "Vile", "Ruthless", "Malevolent", "Sinister", "Diabolical", "Nefarious", "Malicious", "Merciless", "Treacherous", "Venomous",
                             "Fierce", "Savage", "Brutal", "Cruel", "Vicious", "Bloodthirsty", "Barbaric", "Murderous", "Fiendish", "Atrocious",
                             "Wicked", "Evil", "Corrupt", "Depraved", "Malignant", "Iniquitous", "Satanic", "Demonic", "Hellish", "Diabolic",
                             "Insidious", "Perfidious", "Duplicitous", "Deceitful", "Sly", "Cunning", "Crafty", "Tricky", "Wily", "Guileful",
                             "Machiavellian", "Conniving", "Shifty", "Unscrupulous", "Underhanded", "Dirty", "Foul", "Nasty", "Loathsome", "Disgusting",
                             "Hateful", "Odious", "Abhorrent", "Repulsive", "Reviled", "Detestable", "Despicable", "Execrable", "Abominable", "Grotesque",
                             "Hideous", "Monstrous", "Frightful", "Terrifying", "Horrible", "Ghastly", "Macabre", "Eerie", "Gruesome", "Spine-chilling",
                             "Maleficent", "Cursed", "Maledictive", "Necrotic", "Deathly", "Plagued", "Infectious", "Decaying", "Rotting", "Putrid",
                             "Poisonous", "Toxic", "Radioactive", "Venomous", "Noxious", "Virulent", "Lethal", "Deadly", "Fatal", "Destructive" };

        string[] enemyNames = { "Gravekeeper", "Doombringer", "Soulstealer", "Bloodhound", "Shadowblade", "Deathstalker", "Darkslayer", "Hatebringer", "Nightprowler", "Demonspawn",
                        "Hellscream", "Brimstone", "Bloodmoon", "Gorefist", "Necromancer", "Hellfire", "Darkspawn", "Grimreaper", "Bonecrusher", "Deathdealer",
                        "Skullcrusher", "Shadowcaster", "Bloodthirst", "Dreadlord", "Bladebinder", "Tyrant", "Nightmare", "Deathrattle", "Tombstone", "Darkness",
                        "Blackheart", "Frostbite", "Spiteful", "Soulless", "Spectral", "Grimgor", "Grimmjaw", "Bladestorm", "Ravager", "Crimson",
                        "Darkling", "Bloodletter", "Widowmaker", "Deathmark", "Darksoul", "Doomcaller", "Vileborn", "Venomfang", "Dreadnaught", "Devilspawn",
                        "Skullsplitter", "Blackout", "Shadowdancer", "Nightstalker", "Horrorspawn", "Dreadshade", "Bloodscream", "Souldrinker", "Shadowspawn", "Soulrender",
                        "Necroshade", "Doomblade", "Gorefang", "Darkheart", "Gravecrawler", "Deathwarden", "Hateblade", "Blackfire", "Dreadwing", "Soulburner",
                        "Ravenshadow", "Netherworld", "Bloodborn", "Deathshadow", "Demonblade", "Cruelblade", "Gorestorm", "Vileblade", "Doomspawn", "Bloodseeker",
                        "Shadowhunter", "Nightreaper", "Soulhunter", "Deathweaver", "Specterspawn", "Dreadhunter", "Darkterror", "Ghoulspawn", "Netherblade", "Blackbone" };


        return enemyAdjectives[Random.Range(0, enemyAdjectives.Length)] + " " + enemyNames[Random.Range(0, enemyNames.Length)];
    }

    public static bool TooManyPrefabs() {
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Prefabs/");
        FileInfo[] info = dir.GetFiles("*.prefab");
        if (info.Length > 100 * 100) {
            Debug.Log("Too many prefabs.");
            return true;
        }
        return false;
    }
}
