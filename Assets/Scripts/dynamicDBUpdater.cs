using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models.DynamicDB;
using UnityEditor;
using UnityEngine;

public class dynamicDBUpdater : MonoBehaviour {
    const string dynamicDBKey = "dynamicDBVersion";
    public bool ResetDynamicDB = false;
    void Start () {
        SimpleSQL.SimpleSQLManager dbManager = StartScene.instance.dynamicDBManager;

        //dynamicDB reset processing
#if UNITY_EDITOR
        if (ResetDynamicDB && EditorUtility.DisplayDialog ("Drop Database", "Are you sure delete all data?", "delete", "cancel")) {
            var q = System.AppDomain.CurrentDomain.GetAssemblies ()
                .SelectMany (t => t.GetTypes ())
                .Where (t => t.IsClass && t.Namespace == "Models.DynamicDB"); 
            dbManager.BeginTransaction ();
            q.ToList ().ForEach (t => dbManager.Execute (SQLDropTableCommand (t.Name)));
            dbManager.Commit ();
            dbManager.Execute ("VACUUM");
            PlayerPrefs.SetFloat (dynamicDBKey, 0);
        }
#endif

        float version = PlayerPrefs.GetFloat (dynamicDBKey, 0);
        if (version == 0) {
            // dbManager.Execute ("create table \"ScenarioStatus \"(\"id \" integer not null ,\"state_machine_id \" integer primary key not null ,\"is_cleared \" integer )");
            dbManager.CreateTable<ScenarioStatus> ();
            version = 0.1f;
        }

        PlayerPrefs.SetFloat (dynamicDBKey, version);
    }

#if UNITY_EDITOR
    private string SQLDropTableCommand (string table_name) {
        return "DROP TABLE IF EXISTS \"" + table_name + "\"";
    }
#endif
}
