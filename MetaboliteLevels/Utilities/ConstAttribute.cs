using System;                

namespace MetaboliteLevels.Utilities
{
    /// <summary>
    /// Comment attribute.
    /// Argument not intended to be modified.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    class ConstAttribute : Attribute
    {
    }

    /// <summary>
    /// Comment attribute.
    /// Argument intended to be modified.
    /// Is thread safe (either unique or manages own safety).
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    class MutableSafeAttribute : Attribute
    {
    }

    /// <summary>
    /// Comment attribute:
    /// Argument intended to be modified.
    /// Is NOT thread safe (not unique and unable to manage own safety).
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    class MutableUnsafeAttribute : Attribute
    {
    }
}
