using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AxisKeys))]
public class AxisKeysDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // ensure override works on entire property
        EditorGUI.BeginProperty(position, label, property);

        // Setting indent to 0
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // set position rects
        Rect posLabel = new Rect(position.x, position.y, 15, position.height);
        Rect posField = new Rect(position.x + 15 + 5, position.y, 50, position.height);
        Rect negLabel = new Rect(position.x + 15 + 5 + 50 + 10, position.y, 15, position.height);
        Rect negField = new Rect(position.x + 15 + 5 + 50 + 10 + 15 + 5, position.y, 50, position.height);

        // setting sub labels
        GUIContent posGUI = new GUIContent("+");
        GUIContent negGUI = new GUIContent("-");

        // drawing the fields
        EditorGUI.LabelField(posLabel, posGUI);
        EditorGUI.PropertyField(posField, property.FindPropertyRelative("pos"), GUIContent.none);
        EditorGUI.LabelField(negLabel, negGUI);
        EditorGUI.PropertyField(negField, property.FindPropertyRelative("neg"), GUIContent.none);

        // resetting indent
        EditorGUI.indentLevel = indent;

        // end property
        EditorGUI.EndProperty();
    }
}