using System;
using System.Configuration;

namespace Codefire.Configuration
{
    /// <summary>
    /// The data configuration section.
    /// </summary>
    public sealed class CodefireSection : ConfigurationSection
    {
        #region [ Constants ]

        /// <summary>
        /// The section name to be used in the configuration file.
        /// </summary>
        public const string SectionName = "codefire";

        #endregion

        #region [ Static Fields ]

        private static readonly ConfigurationPropertyCollection _properties;
        private static readonly ConfigurationProperty _webProperty;

        #endregion

        #region [ Static Constructor ]

        static CodefireSection()
        {
            _webProperty = new ConfigurationProperty("web", typeof(WebElement), null, ConfigurationPropertyOptions.None);

            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_webProperty);
        }

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Create an instance of CodefireSection.
        /// </summary>
        public CodefireSection()
            : base()
        {
        }

        #endregion

        #region [ Static Properties ]

        public static CodefireSection Config
        {
            get { return ConfigurationManager.GetSection(SectionName) as CodefireSection; }
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
        /// Gets or Sets the web element.
        /// </summary>
        [ConfigurationProperty("web", Options = ConfigurationPropertyOptions.None)]
        public WebElement Web
        {
            get { return (WebElement)base[_webProperty]; }
            set { base[_webProperty] = value; }
        }

        #endregion
    }
}