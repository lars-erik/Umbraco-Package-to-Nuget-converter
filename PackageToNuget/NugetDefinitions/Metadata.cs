using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PackageToNuget.NugetDefinitions
{
    public class Metadata : IEquatable<Metadata>
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("authors")]
        public string Authors { get; set; }

        [XmlElement("owners")]
        public string Owners { get; set; }

        [XmlElement("licenseUrl")]
        public string LicenseUrl { get; set; }

        [XmlElement("projectUrl")]
        public string ProjectUrl { get; set; }

        [XmlElement("iconUrl")]
        public string IconUrl { get; set; }

        [XmlElement("requireLicenseAcceptance")]
        public bool RequreLicenseAcceptance { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("releaseNotes")]
        public string ReleaseNotes { get; set; }

        [XmlElement("copyright")]
        public string Copyright { get; set; }

        [XmlElement("tags")]
        public string Tags { get; set; }

        [XmlArray("dependencies")]
        public List<Dependency> Dependencies { get; set; }

        public Metadata()
        {
            Dependencies = new List<Dependency>();
        }

        #region Equality // TODO: Fix dependency comparison

        public bool Equals(Metadata other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && string.Equals(Version, other.Version) && string.Equals(Authors, other.Authors) && string.Equals(Owners, other.Owners) && string.Equals(LicenseUrl, other.LicenseUrl) && string.Equals(ProjectUrl, other.ProjectUrl) && string.Equals(IconUrl, other.IconUrl) && RequreLicenseAcceptance.Equals(other.RequreLicenseAcceptance) && string.Equals(Description, other.Description) && string.Equals(ReleaseNotes, other.ReleaseNotes) && string.Equals(Copyright, other.Copyright) && string.Equals(Tags, other.Tags) && Equals(Dependencies, other.Dependencies);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Metadata) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Version != null ? Version.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Authors != null ? Authors.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Owners != null ? Owners.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LicenseUrl != null ? LicenseUrl.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ProjectUrl != null ? ProjectUrl.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (IconUrl != null ? IconUrl.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ RequreLicenseAcceptance.GetHashCode();
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ReleaseNotes != null ? ReleaseNotes.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Copyright != null ? Copyright.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Tags != null ? Tags.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Metadata left, Metadata right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Metadata left, Metadata right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
