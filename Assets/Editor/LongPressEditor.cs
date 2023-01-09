using UnityEditor;//�༭������UnityEditor�����ռ��¡����Ե�ʹ��C#�ű�ʱ������Ҫ�ڽű�ǰ����� "using UnityEditor"���á�
using UnityEditor.UI;//ButtonEditorλ�ڴ������ռ���

//ָ������Ҫ�Զ���༭���Ľű� 
[CustomEditor(typeof(LongPressButton), true)]
//ʹ���� SerializedObject �� SerializedProperty ϵͳ����ˣ������Զ����������༭����������undo�� �� ��Ԥ�Ƹ���prefab override����
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
    //�����ر�ע�⣬������������л���ʽ����Ҫ�� OnInspectorGUI ��ͷ�ͽ�β����һ�� serializedObject.Update();  serializedObject.ApplyModifiedProperties();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();//����
        serializedObject.Update();
        EditorGUILayout.PropertyField(keyCode);//��ʾ���Ǵ���������
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();//����
        serializedObject.Update();
        EditorGUILayout.PropertyField(OnLongPress);//��ʾ���Ǵ���������
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();//����
        serializedObject.Update();
        EditorGUILayout.PropertyField(onEventUp);//��ʾ���Ǵ���������
        serializedObject.ApplyModifiedProperties();
    }

}