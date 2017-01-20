using UnityEditor;

namespace Common.Utils {
    public abstract class CommonEditor : Editor {

        protected string help = string.Empty;

        protected bool callBase = false;

        public override void OnInspectorGUI() {
            if (help != string.Empty) {
                EditorGUILayout.HelpBox(help, MessageType.Info);
            }
            if (callBase)
                base.OnInspectorGUI();
        }

        protected virtual void OnEnable() {
        }

    }
}