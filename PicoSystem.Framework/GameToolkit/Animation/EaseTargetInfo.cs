using System;
using System.Reflection;

namespace PicoSystem.Framework.GameToolkit.Animation
{
    internal class EaseTargetInfo
    {
        private static BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        public string PropertyName { get; }
        public Type PropertyType { get; }

        private readonly FieldInfo field;
        private readonly PropertyInfo prop;
        private readonly object Target;

        public object Value
        {
            get
            {
                return field != null ?
                    field.GetValue(Target) :
                    prop.GetValue(Target, null);
            }

            set
            {
                if (field != null) field.SetValue(Target, value);
                else prop.SetValue(Target, value, null);
            }
        }

        public EaseTargetInfo(object target, PropertyInfo info)
        {
            Target = target;
            prop = info;
            PropertyName = info.Name;
            PropertyType = prop.PropertyType;
        }

        public EaseTargetInfo(object target, FieldInfo info)
        {
            Target = target;
            field = info;
            PropertyName = info.Name;
            PropertyType = info.FieldType;
        }

        public EaseTargetInfo(object target, string property, bool writeRequired = true)
        {
            Target = target;
            PropertyName = property;

            var targetType = target as Type ?? target.GetType();

            if ((field = targetType.GetField(property, flags)) != null)
            {
                PropertyType = field.FieldType;
            }
            else if ((prop = targetType.GetProperty(property, flags)) != null)
            {
                PropertyType = prop.PropertyType;
            }
            else
            {
                throw new Exception(
                    $"Field or {(writeRequired ? "read/write" : "readable")} property '{property}' not found on object of type {targetType.FullName}.");
            }
        }
    }
}
