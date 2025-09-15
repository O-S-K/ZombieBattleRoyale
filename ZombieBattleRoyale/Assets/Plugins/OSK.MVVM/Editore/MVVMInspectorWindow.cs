using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace OSK.MVVM
{
    public class MVVMInspectorWindow : EditorWindow
    {
        private Vector2 _scroll;

        [MenuItem("OSK.MVVM/Tools/Debugger")]
        public static void ShowWindow()
        {
            GetWindow<MVVMInspectorWindow>("MVVM Debugger");
        }

        private void OnGUI()
        {
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Enter Play Mode to inspect runtime ViewModels", MessageType.Info);
                return;
            }

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            var views = GameObject.FindObjectsOfType<MonoBehaviour>()
                .Where(v => IsSubclassOfGeneric(v.GetType(), typeof(ViewBase<>)))
                .ToList();

            if (views.Count == 0)
            {
                EditorGUILayout.LabelField("No ViewBase<T> instances found in scene.");
            }

            foreach (var v in views)
            {
                DrawView(v);
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawView(MonoBehaviour view)
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField($"{view.name} ({view.GetType().Name})", EditorStyles.boldLabel);

            var vmProp = view.GetType().GetProperty("ViewModel", BindingFlags.Public | BindingFlags.Instance);
            var vm = vmProp?.GetValue(view);

            if (vm == null)
            {
                EditorGUILayout.LabelField("ViewModel: null");
                EditorGUILayout.EndVertical();
                return;
            }

            EditorGUILayout.LabelField("ViewModel: " + vm.GetType().Name, EditorStyles.miniBoldLabel);

            var props = vm.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in props)
            {
                if (!p.CanRead) continue;

                if (typeof(RelayCommand).IsAssignableFrom(p.PropertyType))
                {
                    var cmd = p.GetValue(vm) as RelayCommand;
                    if (cmd != null && GUILayout.Button("Execute " + p.Name))
                    {
                        cmd.Execute(null);
                    }
                }
                else
                {
                    var val = p.GetValue(vm);
                    EditorGUILayout.LabelField(p.Name, val != null ? val.ToString() : "null");
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        private static bool IsSubclassOfGeneric(Type type, Type generic)
        {
            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (cur == generic) return true;
                type = type.BaseType;
            }
            return false;
        }
    }
}
