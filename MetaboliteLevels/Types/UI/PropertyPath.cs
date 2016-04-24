using MetaboliteLevels.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaboliteLevels.Types.UI
{
    class PropertyPath<TSource, TDestination>
    {
        public delegate TDestination Property( TSource target );
        private readonly PropertyInfo[] _properties;

        public PropertyPath( Expression<Property> property )
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();

            var body = property.Body;

            while (!(body is ParameterExpression))
            {
                var bodyUe = body as UnaryExpression;

                if (bodyUe != null)
                {
                    MemberExpression memEx = bodyUe.Operand as MemberExpression;
                    properties.Add( (PropertyInfo)memEx.Member );

                    body = memEx.Expression;
                    continue;
                }

                var bodyMe = body as MemberExpression;

                if (bodyMe != null)
                {
                    properties.Add( (PropertyInfo)bodyMe.Member );

                    body = bodyMe.Expression;
                    continue;
                }
            }

            _properties = properties.Reverse<PropertyInfo>().ToArray();
        }

        public bool HasDefaultValue
        {
            get
            {
                return Last.GetCustomAttribute<DefaultValueAttribute>() != null;
            }
        }

        public object DefaultValue
        {
            get
            {
                var attr = Last.GetCustomAttribute<DefaultValueAttribute>();

                if (attr != null)
                {
                    return attr.Value;
                }

                return Last.GetType().IsValueType ? Activator.CreateInstance( Last.GetType() ) : null;
            }
        }

        public PropertyInfo Last
        {
            get
            {
                return _properties[_properties.Length - 1];
            }
        }

        public TDestination Get( TSource source )
        {
            PropertyInfo property = null;
            object target = source;

            foreach (PropertyInfo property2 in _properties)
            {
                target = property2.GetValue( target );
                property = property2;
            }

            return (TDestination)target;
        }

        public void Set( TSource source, TDestination value )
        {
            object target = source;

            for (int n = 0; n < _properties.Length; n++)
            {
                PropertyInfo property = _properties[n];

                if (n == _properties.Length - 1)
                {
                    property.SetValue( target, value );
                }
                else
                {
                    target = property.GetValue( target );
                }
            }
        }
    }
}
