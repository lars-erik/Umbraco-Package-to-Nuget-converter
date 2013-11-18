using System;
using System.Xml.Serialization;

namespace PackageToNuget.UmbracoDefinitions
{
    public class Package : IEquatable<Package>
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("license")]
        public License License { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        #region equality

        public bool Equals(Package other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Version, other.Version) && Equals(License, other.License) && string.Equals(Url, other.Url);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Package) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (License != null ? License.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Url != null ? Url.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Package left, Package right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Package left, Package right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
