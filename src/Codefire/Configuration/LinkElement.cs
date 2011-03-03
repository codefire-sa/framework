using System;
using System.Configuration;

namespace Codefire.Configuration
{
    /// <summary>
    /// A web link configuration item.
    /// </summary>
    public sealed class LinkElement : ConfigurationElement
    {
        #region [ Static Fields ]

        private static readonly ConfigurationPropertyCollection _properties;
        private static readonly ConfigurationProperty _nameProperty;
        private static readonly ConfigurationProperty _urlProperty;
        private static readonly ConfigurationProperty _versionProperty;

        #endregion

        #region [ Static Constructors ]

        /// <summary>
        /// 
        /// </summary>
        static LinkElement()
        {
            _nameProperty = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
            _urlProperty = new ConfigurationProperty("url", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
            _versionProperty = new ConfigurationProperty("version", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_nameProperty);
            _properties.Add(_urlProperty);
            _properties.Add(_versionProperty);
        }

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Create an instance of LinkElement.
        /// </summary>
        public LinkElement()
        {
        }

        #endregion

        #region [ Properties ]

        #region [ Overrides ]

        /// <summary>
        /// 
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }

        #endregion

        /// <summary>
        /// Gets or Sets the name of the link.
        /// </summary>
        [ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired, DefaultValue = "")]
        public string Name
        {
            get { return (string)base[_nameProperty]; }
            set { base[_nameProperty] = value; }
        }

        /// <summary>
        /// Gets or Sets the url of the link.
        /// </summary>
        [ConfigurationProperty("url", Options = ConfigurationPropertyOptions.IsRequired, DefaultValue = "")]
        public string Url
        {
            get { return (string)base[_urlProperty]; }
            set { base[_urlProperty] = value; }
        }

        /// <summary>
        /// Gets or Sets the version of the link.
        /// </summary>
        [ConfigurationProperty("version", Options = ConfigurationPropertyOptions.IsRequired, DefaultValue = "")]
        public string Version
        {
            get { return (string)base[_versionProperty]; }
            set { base[_versionProperty] = value; }
        }

        #endregion
    }
}