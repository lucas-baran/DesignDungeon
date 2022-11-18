using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyWindowHeader
{
    // -- TYPES

    private readonly struct HeaderPattern
    {
        public readonly string Prefix;
        public readonly string Suffix;
        public readonly Color OpaqueColor;
        public readonly Color AlphaColor;

        public HeaderPattern(string prefix, string suffix, Color color)
        {
            Prefix = prefix;
            Suffix = suffix;
            AlphaColor = color;
            OpaqueColor = new Color(AlphaColor.r, AlphaColor.g, AlphaColor.b, 1f);
        }
    }

    // -- FIELDS

    private static readonly HeaderPattern[] _headerPatterns = new HeaderPattern[]
    {
        new HeaderPattern("[", "]", new Color(0.4f, 0.65f, 0.2f, 0.15f)),
    };

    private static readonly GUIStyle _style;

    // -- CONTRUCTORS

    static HierarchyWindowHeader()
    {
        _style = new GUIStyle();
        EditorApplication.RepaintHierarchyWindow();

        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    // -- METHODS

    private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject == null)
        {
            return;
        }

        Rect strongRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, 1f);
        string name = gameObject.name;

        for (int i = 0; i < _headerPatterns.Length; i++)
        {
            if (name.StartsWith(_headerPatterns[i].Prefix, StringComparison.Ordinal)
                && name.EndsWith(_headerPatterns[i].Suffix, StringComparison.Ordinal))
            {
                EditorGUI.DrawRect(strongRect, _headerPatterns[i].OpaqueColor);
                EditorGUI.DrawRect(selectionRect, _headerPatterns[i].AlphaColor);
                return;
            }
        }
    }
}
