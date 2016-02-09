
namespace System.Linq.Dynamic
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicProperty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public DynamicProperty(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; private set; }
    }
}
