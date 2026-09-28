using System;

namespace BulletMLLib
{
    /// <summary>
    /// Thrown by BulletPattern.ParseXML when a bulletml document can't be loaded.
    /// The InnerException has the details: an InvalidDataException for an invalid pattern, or an XmlException for malformed xml.
    /// </summary>
    public class BulletMLException : Exception
    {
        /// <summary>
        /// The file that failed to load.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The line in the xml file where the problem is, or 0 if the location isn't known.
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// The column in the xml file where the problem is, or 0 if the location isn't known.
        /// </summary>
        public int LinePosition { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.BulletMLException"/> class.
        /// </summary>
        /// <param name="fileName">The file that failed to load.</param>
        /// <param name="lineNumber">The line in the xml file where the problem is, or 0 if not known.</param>
        /// <param name="linePosition">The column in the xml file where the problem is, or 0 if not known.</param>
        /// <param name="innerException">The error that stopped the file from loading.</param>
        public BulletMLException(string fileName, int lineNumber, int linePosition, Exception innerException)
            : base("Error reading \"" + fileName + "\": " + innerException.Message, innerException)
        {
            FileName = fileName;
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }
    }
}
