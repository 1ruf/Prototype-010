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
        inspectorUI.CloneTree(root); //ui를 복제해서 root 의 자식으로 넣어주라

        DropdownField dropdown = root.Q<DropdownField>(name: "ClassDropdownField");

        CreateDropdownChoices(dropdown);
        return root;
    }

    private void CreateDropdownChoices(DropdownField dropdown)
    {
        dropdown.choices.Clear();

        //EntityState라는 클래스가 속햏있는 어셈블리를 가져온다.
        Assembly fsmAssembly = Assembly.GetAssembly(typeof(EntityState));

        List<string> typelist = fsmAssembly.GetTypes()
            .Where(type => type.IsClass && type.IsAbstract == false && type.IsSubclassOf(typeof(EntityState))) //Where안에 있는게 충족되면 받음
            .Select(type => type.FullName)//객체에서 원하는것만 뽑는다
            .ToList();

        dropdown.choices.AddRange(typelist);
    }
}