using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HuntWordsSO))]
public class HuntWordsBoardCustomEditor : Editor
{
    private HuntWordsSO HuntWordsTarget = null;

    
    private void OnEnable()
    {
        HuntWordsTarget = (HuntWordsSO)target;

        EditorUtility.SetDirty(HuntWordsTarget);
    }

    public override void OnInspectorGUI()
    {
        SetGameGridValues();

        if (HuntWordsTarget.columns != null && HuntWordsTarget.amountOfColumns > 0 && HuntWordsTarget.amountOfLines > 0)
        {
            DrawGameGrid();
        }


        EditorGUILayout.Space(8);


        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Delete All Letters"))
        {
            HuntWordsTarget.DeleteAllLetters();
        }

        if (GUILayout.Button("Random All Letters"))
        {
            HuntWordsTarget.FillAllEmptyBoxesWithRandomLetters();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("wordsToSearchInThisLevel"), true);
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("tipsFromThisLevel"), true);
        EditorGUILayout.Space(10);


        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameGridConfiguration"), true);
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("completedThisLevel"), true);
        EditorGUILayout.Space(10);

        serializedObject.ApplyModifiedProperties();
    }

    private void SetGameGridValues()
    {
        int numberOfColumns = HuntWordsTarget.amountOfColumns;
        int numberOfLines = HuntWordsTarget.amountOfLines;

        HuntWordsTarget.amountOfColumns = EditorGUILayout.IntField("Number Of Columns", HuntWordsTarget.amountOfColumns);
        HuntWordsTarget.amountOfLines = EditorGUILayout.IntField("Number Of Lines", HuntWordsTarget.amountOfLines);

        if (HuntWordsTarget.amountOfColumns != numberOfColumns || HuntWordsTarget.amountOfLines != numberOfLines)
            HuntWordsTarget.CreateNewColum();
    }

    private void DrawGameGrid()
    {

        var tableStyle = new GUIStyle("Box");
        tableStyle.padding = new RectOffset(10, 1, 5, 0);
        tableStyle.margin.left = 10;
        tableStyle.margin.top = 20;
        tableStyle.normal.background = Texture2D.blackTexture;


        var columStyle = new GUIStyle();
        columStyle.fixedWidth = 40;

        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 22;
        rowStyle.fixedWidth = 30;
        rowStyle.alignment = TextAnchor.MiddleLeft;

        var textStyle = new GUIStyle();
        textStyle.normal.background = Texture2D.grayTexture;
        textStyle.normal.textColor = Color.white;
        textStyle.fontStyle = FontStyle.Bold;
        textStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal(tableStyle);
        for (int i = 0; i < HuntWordsTarget.amountOfColumns; i++)
        {
            EditorGUILayout.BeginVertical(columStyle);
            for (int j = 0; j < HuntWordsTarget.amountOfLines; j++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);

                HuntWordsTarget.columns[i].letterOnThisColum[j] = EditorGUILayout.TextArea(HuntWordsTarget.columns[i].letterOnThisColum[j], textStyle);

                if (HuntWordsTarget.columns[i].letterOnThisColum[j] != null && HuntWordsTarget.columns[i].letterOnThisColum[j].Length > 0) 
                {
                   var letter = HuntWordsTarget.columns[i].letterOnThisColum[j].Substring(0, 1);
                   HuntWordsTarget.columns[i].letterOnThisColum[j] = letter.ToUpper();
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndHorizontal();
    }
}