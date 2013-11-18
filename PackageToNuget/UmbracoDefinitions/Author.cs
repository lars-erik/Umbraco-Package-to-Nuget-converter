using System;
using System.Xml.Serialization;

namespace PackageToNuget.UmbracoDefinitions
{
    public class Author : IEquatable<Author>
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("website")]
        public string WebSite { get; set; }

        #region equality

        public bool Equals(Author other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(WebSite, other.WebSite);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Author) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (WebSite != null ? WebSite.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Author left, Author right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Author left, Author right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
