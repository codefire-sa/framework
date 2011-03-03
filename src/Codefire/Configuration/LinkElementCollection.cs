using System;
using System.Configuration;

namespace Codefire.Configuration
{
    /// <summary>
    /// A collection of web link configuration items.
    /// </summary>
    [ConfigurationCollection(typeof(LinkElement))]
    public sealed class LinkElementCollection : ConfigurationElementCollection
    {
        #region [ Constructors ]

        /// <summary>
        /// Create an instance of LinkElementCollection.
        /// </summary>
        public LinkElementCollection()
            : base()
        {
        }

        #endregion

        #region [ Properties ]

        #region [ Overrides ]

        /// <summary>
        /// 
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        #endregion

        /// <summary>
        /// Returns a link configuration item using the name of the link.
        /// </summary>
        /// <param name="name">The name of the link.</param>
        /// <returns>A link configuration item.</returns>
        public new LinkElement this[string name]
        {
            get
            {
                return (LinkElement)base.BaseGet(name);
            }
        }

        #endregion

        #region [ Methods ]

        #region [ Overrides ]

        /// <summary>
        /// Create a new link configuration item.
        /// </summary>
        /// <returns>The binding configuration item created.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new LinkElement();
        }

        /// <summary>
        /// Returns the value used as the key for the binding configuration item.
        /// </summary>
        /// <param name="element">The link configuration item.</param>
        /// <returns>The name of the link configuration item.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LinkElement)element).Name;
        }

        #endregion

        #endregion
    }
}