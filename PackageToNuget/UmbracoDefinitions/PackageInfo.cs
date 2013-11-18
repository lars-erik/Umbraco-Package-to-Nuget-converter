using System;
using System.Xml.Serialization;

namespace PackageToNuget.UmbracoDefinitions
{
    public class PackageInfo : IEquatable<PackageInfo>
    {
        [XmlElement("package")]
        public Package Package { get; set; }

        [XmlElement("author")]
        public Author Author { get; set; }

        #region equality

        public bool Equals(PackageInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Package, other.Package) && Equals(Author, other.Author);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PackageInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Package != null ? Package.GetHashCode() : 0)*397) ^ (Author != null ? Author.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PackageInfo left, PackageInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PackageInfo left, PackageInfo right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
