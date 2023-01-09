using UnityEditor;//编辑器类在UnityEditor命名空间下。所以当使用C#脚本时，你需要在脚本前面加上 "using UnityEditor"引用。
using UnityEditor.UI;//ButtonEditor位于此命名空间下

//指定我们要自定义编辑器的脚本 
[CustomEditor(typeof(LongPressButton), true)]
//使用了 SerializedObject 和 SerializedProperty 系统，因此，可以自动处理“多对象编辑”，“撤销undo” 和 “预制覆盖prefab override”。
[CanEditMultipleObjects]
public class LongPressButtonEditor : ButtonEditor
{
    
    private SerializedProperty keyCode;
    private SerializedProperty OnLongPress;
    private SerializedProperty onEventUp;

    protected override void OnEnable()
    {
        base.OnEnable();
        keyCode = serializedObject.FindProperty("keyCode");
        OnLongPress = serializedObject.FindProperty("my_onLongPress");
        onEventUp = serializedObject.FindProperty("my_onEventUp");
    }
    //并且特别注意，如果用这种序列化方式，需要在 OnInspectorGUI 开头和结尾各加一句 serializedObject.Update();  serializedObject.ApplyModifiedProperties();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();//空行
        serializedObject.Update();
        EditorGUILayout.PropertyField(keyCode);//显示我们创建的属性
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();//空行
        serializedObject.Update();
        EditorGUILayout.PropertyField(OnLongPress);//显示我们创建的属性
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();//空行
        serializedObject.Update();
        EditorGUILayout.PropertyField(onEventUp);//显示我们创建的属性
        serializedObject.ApplyModifiedProperties();
    }

}