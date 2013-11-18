using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PackageToNuget.NugetDefinitions
{
    [XmlRoot("package")]
    public class NuSpec : IEquatable<NuSpec>
    {
        [XmlElement("metadata")]
        public Metadata Metadata { get; set; }

        public NuSpec()
        {
            Metadata = new Metadata();
        }

        #region Equality

        public bool Equals(NuSpec other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Metadata, other.Metadata);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NuSpec) obj);
        }

        public override int GetHashCode()
        {
            return (Metadata != null ? Metadata.GetHashCode() : 0);
        }

        public static bool operator ==(NuSpec left, NuSpec right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NuSpec left, NuSpec right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
