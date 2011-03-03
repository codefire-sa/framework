using System;
using System.Configuration;

namespace Codefire.Configuration
{
    /// <summary>
    /// The innovation connect configuration section.
    /// </summary>
    public sealed class WebElement : ConfigurationElement
    {
        #region [ Static Fields ]

        private static readonly ConfigurationPropertyCollection _properties;
        private static readonly ConfigurationProperty _loginUrlProperty;
        private static readonly ConfigurationProperty _linksProperty;

        #endregion

        #region [ Static Constructor ]

        static WebElement()
        {
            _loginUrlProperty = new ConfigurationProperty("loginUrl", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
            _linksProperty = new ConfigurationProperty("links", typeof(LinkElementCollection), new LinkElementCollection(), ConfigurationPropertyOptions.None);

            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_loginUrlProperty);
            _properties.Add(_linksProperty);
        }

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Create an instance of ConnectSection.
        /// </summary>
        public WebElement()
            : base()
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
        /// Gets or Sets the login url.
        /// </summary>
        [ConfigurationProperty("loginUrl", Options = ConfigurationPropertyOptions.IsRequired, DefaultValue = "")]
        public string LoginUrl
        {
            get { return (string)base[_loginUrlProperty]; }
            set { base[_loginUrlProperty] = value; }
        }

        /// <summary>
        /// Returns a collection of the links configured.
        /// </summary>
        [ConfigurationProperty("links")]
        public LinkElementCollection Links
        {
            get
            {
                return (LinkElementCollection)base[_linksProperty];
            }
        }

        #endregion
    }
}