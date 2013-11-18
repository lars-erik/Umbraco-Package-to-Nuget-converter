using System;
using System.Xml.Serialization;

namespace PackageToNuget.UmbracoDefinitions
{
    public class License : IEquatable<License>
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlText]
        public string Name { get; set; }

        #region equality

        public bool Equals(License other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Url, other.Url) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((License)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Url != null ? Url.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public static bool operator ==(License left, License right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(License left, License right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
