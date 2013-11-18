using System;
using System.Xml.Serialization;

namespace PackageToNuget.UmbracoDefinitions
{
    public class PackageFile : IEquatable<PackageFile>
    {
        [XmlElement("guid")]
        public string Guid { get; set; }

        [XmlElement("orgPath")]
        public string OrgPath { get; set; }

        [XmlElement("orgName")]
        public string OrgName { get; set; }

        #region equality

        public bool Equals(PackageFile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Guid, other.Guid) && string.Equals(OrgPath, other.OrgPath) && string.Equals(OrgName, other.OrgName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PackageFile) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Guid != null ? Guid.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (OrgPath != null ? OrgPath.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (OrgName != null ? OrgName.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(PackageFile left, PackageFile right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PackageFile left, PackageFile right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
