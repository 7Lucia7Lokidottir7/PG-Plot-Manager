using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlotManagerEditorWindow : EditorWindow
{
    private const string _filepath = "PG Plot Management/PlotManagerUXML";
    private VisualTreeAsset _treeAsset;
    [MenuItem("Window/PG/Plot Manager")]
    public static void ShowExample()
    {
        PlotManagerEditorWindow wnd = GetWindow<PlotManagerEditorWindow>();
        wnd.titleContent = new GUIContent("PlotManager");
    }
    private void OnEnable()
    {
        _treeAsset = Resources.Load<VisualTreeAsset>(_filepath);
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        _treeAsset.CloneTree(root);

    }
}
