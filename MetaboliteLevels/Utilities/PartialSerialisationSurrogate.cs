/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Data.Session;
using MetaboliteLevels.Data.Visualisables;

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Allows us to save partial data by saving things like "Peaks" by a unique ID rather than
    /// saving the entire object.
    /// 
    /// To use simply label the field with [CanLookupAttribute] and use the [CreateFormatter]
    /// method to create the BinaryFormatter that understands this attribute.
    /// </summary>
    class PartialSerialisationSurrogate : ISerializationSurrogate
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        BiDictionary<Guid, object> guids;

        public PartialSerialisationSurrogate(Core core)
        {
            AddLookups(core.Peaks);
            AddLookups(core.Observations);
            AddLookups(core.Conditions);

            guids = core.Guids;
        }

        private void AddLookups(IEnumerable values)
        {
            foreach (object lookupable in values)
            {
                if (!guids.Contains(lookupable))
                {
                    guids.Add(lookupable, Guid.NewGuid()); // If there is a freaky collision we'll get an error here
                }
            }
        }

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            foreach (FieldInfo field in obj.GetType().GetFields(BINDING_FLAGS))
            {
                object value = field.GetValue(obj);
                NonSerializedAttribute attr1 = field.GetCustomAttribute<NonSerializedAttribute>();

                if (attr1 != null)
                {
                    continue;
                }

                CanLookupAttribute attr2 = field.GetCustomAttribute<CanLookupAttribute>();

                if (attr2 == null)
                {
                    info.AddValue(field.Name, value);
                    continue;
                }

                Guid guid;

                if (value == null)
                {
                    guid = Guid.Empty;
                }
                else
                {
                    guid = this.guids[value];
                }

                info.AddValue(field.Name, guid);
            }
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            foreach (FieldInfo field in obj.GetType().GetFields(BINDING_FLAGS))
            {
                NonSerializedAttribute attr1 = field.GetCustomAttribute<NonSerializedAttribute>();

                if (attr1 != null)
                {
                    continue;
                }

                CanLookupAttribute attr2 = field.GetCustomAttribute<CanLookupAttribute>();

                if (attr2 == null)
                {
                    object value = info.GetValue(field.Name, field.FieldType);
                    field.SetValue(obj, value);
                    continue;
                }

                Guid guid = (Guid)info.GetValue(field.Name, typeof(Guid));

                if (guid == Guid.Empty)
                {
                    field.SetValue(obj, null);
                }
                else
                {
                    object lvalue = this.guids[guid];

                    field.SetValue(obj, lvalue);
                }
            }

            return obj;
        }

        internal static BinaryFormatter CreateFormatter(Core core)
        {
            PartialSerialisationSurrogate surrogate = new PartialSerialisationSurrogate(core);

            Selector selector = new Selector(surrogate);

            return new BinaryFormatter(selector, new StreamingContext(StreamingContextStates.File));
        }

        class Selector : ISurrogateSelector
        {
            private PartialSerialisationSurrogate surrogate;

            public Selector(PartialSerialisationSurrogate surrogate)
            {
                this.surrogate = surrogate;
            }

            public void ChainSelector(ISurrogateSelector selector)
            {
                // NA
            }

            public ISurrogateSelector GetNextSelector()
            {
                return null;
            }

            public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
            {
                if (type.Assembly == Assembly.GetEntryAssembly())
                {
                    selector = this;
                    return surrogate;
                }

                selector = null;
                return null;
            }
        }
    }

    /// <summary>
    /// See [PartialSerialisationSurrogate]
    /// </summary>
    class CanLookupAttribute : Attribute
    {
        private readonly bool[] templateArgs;

        public CanLookupAttribute(params bool[] templateArgs)
        {
            this.templateArgs = templateArgs;
        }

        public bool Get(MemberInfo member, int templateIndex = 0)
        {
            CanLookupAttribute attr = member.GetCustomAttribute<CanLookupAttribute>();

            if (attr == null)
            {
                return false;
            }

            if (attr.templateArgs == null || attr.templateArgs.Length <= templateIndex)
            {
                return true;
            }

            return attr.templateArgs[templateIndex];
        }
    }
}
*/