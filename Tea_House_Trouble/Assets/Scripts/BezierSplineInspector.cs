using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor
{
    private const int lineSteps = 10;
    private const float directionScale = 0.5f;

    private BezierSpline spline;
    private Transform handleTransform;
    private Quaternion handleRotation;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        spline = target as BezierSpline;

        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(spline, "Add Curve");
            spline.AddCurve();
            EditorUtility.SetDirty(spline);
        }
    }
    private void OnSceneGUI()
    {
        spline = target as BezierSpline;
        handleTransform = spline.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 p1 = ShowPoint(0);

        for (int i = 1; i < spline.points.Length; i += 3)
        {
            Vector3 p2 = ShowPoint(i);
            Vector3 p3 = ShowPoint(i + 1);
            Vector3 p4 = ShowPoint(i + 2);

            Handles.color = Color.gray;
            Handles.DrawLine(p1, p2);
            Handles.DrawLine(p3, p4);

            Handles.DrawBezier(p1, p4, p2, p3, Color.white, null, 2f);
            p1 = p4;
        }
        ShowDirections();

        Handles.color = Color.white;
        Vector3 lineStart = spline.GetPoint(0f);
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + spline.GetDirection(0f));
    }

    private const int stepsPerCurve = 10;

    private void ShowDirections()
    {
        Handles.color = Color.green;
        Vector3 point = spline.GetPoint(0f);
        Handles.DrawLine(point, point + spline.GetDirection(0f) * directionScale);
        
        int steps = stepsPerCurve * spline.CurveCount;

        for (int i = 1; i <= steps; i++)
        {
            point = spline.GetPoint(i / (float) steps);
            Handles.DrawLine(point, point + spline.GetDirection(i / (float) steps) * directionScale);
        }
    }

    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private int selectedIndex = -1;

    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(spline.points[index]);
        Handles.color = Color.white;

        if (Handles.Button(point, handleRotation, handleSize, pickSize, Handles.DotHandleCap))
        {
            selectedIndex = index;
        }

        if (selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Move Point");
                EditorUtility.SetDirty(spline);
                spline.points[index] = handleTransform.InverseTransformPoint(point);
            }
        }
        return point;
    }
}