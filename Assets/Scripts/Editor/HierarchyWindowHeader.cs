using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyWindowHeader
{
    // -- TYPES

    [Flags]
    private enum EEdge
    {
        None = 0,
        Top = 1 << 0,
        Bottom = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
        Everything = Top | Bottom | Left | Right
    }

    private readonly struct HeaderPattern
    {
        public readonly string Prefix;
        public readonly string Suffix;
        public readonly Color OpaqueColor;
        public readonly Color AlphaColor;
        public readonly EEdge EdgeDisplay;
        public readonly float EdgeThickness;

        public HeaderPattern( string prefix, string suffix, Color color, EEdge edge_display, float edge_thickness )
        {
            Prefix = prefix;
            Suffix = suffix;
            AlphaColor = color;
            OpaqueColor = new Color( AlphaColor.r, AlphaColor.g, AlphaColor.b, 1f );
            EdgeDisplay = edge_display;
            EdgeThickness = edge_thickness;
        }
    }

    // -- FIELDS

    private static readonly HeaderPattern[] _headerPatterns = new HeaderPattern[]
    {
        new HeaderPattern(string.Empty, "Manager", new Color(0.4f, 0.65f, 0.2f, 0.15f), EEdge.Everything, 1f),
    };

    private static readonly EEdge[] Edges = new EEdge[ 4 ]
    {
        EEdge.Top,
        EEdge.Bottom,
        EEdge.Left,
        EEdge.Right,
    };

    // -- CONTRUCTORS

    static HierarchyWindowHeader()
    {
        EditorApplication.RepaintHierarchyWindow();

        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    // -- METHODS

    private static void HierarchyWindowItemOnGUI( int instance_ID, Rect selection_rect )
    {
        var game_object = EditorUtility.InstanceIDToObject( instance_ID ) as GameObject;

        if( game_object == null )
        {
            return;
        }

        string name = game_object.name;

        for( int i = 0; i < _headerPatterns.Length; i++ )
        {
            var header_pattern = _headerPatterns[ i ];

            if( !name.StartsWith( header_pattern.Prefix, StringComparison.Ordinal )
                || !name.EndsWith( header_pattern.Suffix, StringComparison.Ordinal ) )
            {
                continue;
            }

            foreach( var edge in Edges )
            {
                if( !header_pattern.EdgeDisplay.HasFlag( edge ) )
                {
                    continue;
                }

                EditorGUI.DrawRect( GetEdgeRect( edge, selection_rect, header_pattern.EdgeThickness ), header_pattern.OpaqueColor );
            }

            EditorGUI.DrawRect( selection_rect, header_pattern.AlphaColor );

            return;
        }
    }

    private static Rect GetEdgeRect( EEdge edge, Rect selection_rect, float edge_thickness ) => edge switch
    {
        EEdge.Top => new Rect( selection_rect.x, selection_rect.y, selection_rect.width, edge_thickness ),
        EEdge.Bottom => new Rect( selection_rect.x, selection_rect.y + selection_rect.height - edge_thickness, selection_rect.width, edge_thickness ),
        EEdge.Left => new Rect( selection_rect.x, selection_rect.y, edge_thickness, selection_rect.height ),
        EEdge.Right => new Rect( selection_rect.x + selection_rect.width - edge_thickness, selection_rect.y, edge_thickness, selection_rect.height ),
        _ => throw new NotSupportedException( $"Cannot calculate Rect for edge {edge}" ),
    };
}
