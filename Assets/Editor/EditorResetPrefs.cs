using UnityEditor;

namespace Editor {

    public class EditorResetPrefs {

        [MenuItem("Edit/Reset Prefrences")]
        public static void ResetPrefs() {
            if(EditorUtility.DisplayDialog(
                "Reset editor preferences?", 
                "Reset all editor preferences? This cannot be undone.", 
                "Yes", 
                "No")
            ) {
                EditorPrefs.DeleteAll();
            }
        }
        
    }

}