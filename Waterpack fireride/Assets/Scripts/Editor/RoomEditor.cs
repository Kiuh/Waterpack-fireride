using UnityEditor;
using UnityEngine;

namespace Environment
{
    [CustomEditor(typeof(Room))]
    [CanEditMultipleObjects]
    public class RoomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Room room = serializedObject.targetObject as Room;

            _ = EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Recreate"))
            {
                room.Recreate();
            }
            EditorGUILayout.EndHorizontal();

            _ = DrawDefaultInspector();

            _ = serializedObject.ApplyModifiedProperties();
        }
    }
}
