using M5_Task_1.Attributes;
using M5_Task_1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace M5_Task_1
{
    public class Container : IContainer
    {
        #region fields
        private readonly Dictionary<Type, Type> components;
        #endregion

        #region ctors
        public Container()
        {
            components = new Dictionary<Type, Type>();
        }
        #endregion

        #region API
        public void AddAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.ExportedTypes)
            {
                if (type.GetCustomAttributes(typeof(ExportAttribute), true).Length > 0)
                {
                    var attr = (ExportAttribute)Attribute.GetCustomAttribute(type, typeof(ExportAttribute));
                    var baseType = attr.ContractType == null ? type : attr.ContractType;

                    AddType(baseType, type);
                }
            }
        }

        public void AddType(Type type)
        {
            AddType(type, type);
        }

        public void AddType(Type baseType, Type type)
        {
            components.Add(baseType, type);
        }

        public object CreateInstance(Type type)
        {
            var expInstance = CreateExpInstance(type);

            return Expression.Lambda<Func<object>>(expInstance).Compile()();
        }

        public T CreateInstance<T>()
        {
            var expInstance = CreateExpInstance(typeof(T));

            return Expression.Lambda<Func<T>>(expInstance).Compile()();
        }
        #endregion

        #region private
        private Expression CreateExpInstance(Type baseType)
        {
            Type resolver;
            this.ValidateRegisterComponents(baseType, out resolver);

            ConstructorInfo ctorInfo = resolver
                .GetConstructors()
                .FirstOrDefault(ctor => ctor.GetCustomAttribute<ImportConstructor>() != null);

            if (ctorInfo != null)
            {
                return CreateExpInstanceByConstructor(resolver, ctorInfo);
            }

            return CreateExpInstanceByProperty(resolver);
        }

        private Expression CreateExpInstanceByConstructor(Type resolver, ConstructorInfo ctorInfo)
        {
            var paramsInfo = ctorInfo.GetParameters();
            Expression[] argsExp = new Expression[paramsInfo.Count()];
            int index = 0;

            foreach (ParameterInfo paramInfo in paramsInfo)
            {
                argsExp[index++] = CreateExpInstance(paramInfo.ParameterType);
            }

            return Expression.New(ctorInfo, argsExp);
        }

        private Expression CreateExpInstanceByProperty(Type resolver)
        {
            var propsInfo = resolver
                .GetProperties()
                .Where(prop => prop.GetCustomAttribute<ImportAttribute>() != null);

            var obj = Expression.New(resolver);

            MemberBinding[] membExp = new MemberBinding[propsInfo.Count()];
            int index = 0;

            foreach (PropertyInfo propInfo in propsInfo)
            {
                membExp[index++] = Expression.Bind(propInfo, CreateExpInstance(propInfo.PropertyType));
            }

            return Expression.MemberInit(obj, membExp);
        }

        private void ValidateRegisterComponents(Type baseType, out Type resolver)
        {
            if (!components.TryGetValue(baseType, out resolver))
            {
                throw new IoCException($"The type {nameof(baseType.GetType)} is not registered in container");
            }
        }
        #endregion
    }
}