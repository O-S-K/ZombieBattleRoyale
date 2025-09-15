#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

namespace OSK.MVVM
{
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    [CanEditMultipleObjects]
    public class GenericViewEditor : Editor
    {
        public override bool RequiresConstantRepaint() => true;

        public override void OnInspectorGUI()
        {
            if (!(target is IViewFor view))
            {
                base.OnInspectorGUI();
                return;
            }
            
            var vm = GetViewModel(view);
            if (!Application.isPlaying || vm == null)
            {
                EditorGUILayout.HelpBox("Play mode required to inspect runtime ViewModel", MessageType.Info);
                return;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime Data (ViewModel)", EditorStyles.boldLabel);

            // hiển thị property
            var props = vm.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in props)
            {
                if (!p.CanRead) continue;

                if (typeof(RelayCommand).IsAssignableFrom(p.PropertyType))
                {
                    var cmd = p.GetValue(vm) as RelayCommand;
                    if (cmd != null && GUILayout.Button(p.Name))
                    {
                        cmd.Execute(null);
                    }
                }
                else if (p.CanWrite)
                {
                    object oldValue = p.GetValue(vm);
                    object newValue = DrawField(p.PropertyType, p.Name, oldValue);

                    if (!Equals(oldValue, newValue))
                    {
                        p.SetValue(vm, newValue);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField($"{p.Name}: {p.GetValue(vm)}");
                }
            }
        }

        private object GetViewModel(IViewFor view)
        {
            var field = view.GetType().GetProperty("ViewModel", BindingFlags.Public | BindingFlags.Instance);
            return field?.GetValue(view);
        }

        private object DrawField(Type type, string label, object value)
        {
            if (type == typeof(string))
            {
                return EditorGUILayout.TextField(label, value as string);
            }
            else if (type == typeof(int))
            {
                return EditorGUILayout.IntField(label, (int)value);
            }
            else if (type == typeof(float))
            {
                return EditorGUILayout.FloatField(label, (float)value);
            }
            else if (type == typeof(bool))
            {
                return EditorGUILayout.Toggle(label, (bool)value);
            }
            else if (type.IsEnum)
            {
                return EditorGUILayout.EnumPopup(label, (Enum)value);
            }
            else
            {
                EditorGUILayout.LabelField($"{label}: {value?.ToString() ?? "null"} (unsupported)");
                return value;
            }
        }
    }
#endif
}