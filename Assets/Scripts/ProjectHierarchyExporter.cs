using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public static class ProjectHierarchyExporter
{
    [MenuItem("Tools/Export Project Hierarchy")]
    public static void ExportHierarchy()
    {
        string rootPath = "Assets";
        string outputDir = "Assets/Document";
        string outputFile = Path.Combine(outputDir, "project_tree.txt");

        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        StringBuilder sb = new StringBuilder();

        BuildTree(rootPath, sb, "");

        File.WriteAllText(outputFile, sb.ToString());

        AssetDatabase.Refresh();

        Debug.Log($"Project hierarchy exported to: {outputFile}");
    }

    private static void BuildTree(string path, StringBuilder sb, string indent)
    {
        sb.AppendLine($"{indent}{Path.GetFileName(path)}/");

        var directories = Directory.GetDirectories(path);
        var files = Directory.GetFiles(path);

        foreach (var dir in directories)
        {
            BuildTree(dir.Replace('\\', '/'), sb, indent + "    ");
        }

        foreach (var file in files)
        {
            if (file.EndsWith(".meta"))
                continue;

            sb.AppendLine($"{indent}    {Path.GetFileName(file)}");
        }
    }
}