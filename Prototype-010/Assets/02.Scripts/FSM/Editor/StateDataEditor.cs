using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(StateDataSO))]
public class StateDataEditor : UnityEditor.Editor
{
    [SerializeField] private VisualTreeAsset inspectorUI = default;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        inspectorUI.CloneTree(root); //ui�� �����ؼ� root �� �ڽ����� �־��ֶ�

        DropdownField dropdown = root.Q<DropdownField>(name: "ClassDropdownField");

        CreateDropdownChoices(dropdown);
        return root;
    }

    private void CreateDropdownChoices(DropdownField dropdown)
    {
        dropdown.choices.Clear();

        //EntityState��� Ŭ������ ���d�ִ� ������� �����´�.
        Assembly fsmAssembly = Assembly.GetAssembly(typeof(EntityState));

        List<string> typelist = fsmAssembly.GetTypes()
            .Where(type => type.IsClass && type.IsAbstract == false && type.IsSubclassOf(typeof(EntityState))) //Where�ȿ� �ִ°� �����Ǹ� ����
            .Select(type => type.FullName)//��ü���� ���ϴ°͸� �̴´�
            .ToList();

        dropdown.choices.AddRange(typelist);
    }
}