using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PackageToNuget.NugetDefinitions
{
    [XmlRoot("package", Namespace = "http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd")]
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

        public static NuSpec Load(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                return Load(stream);
            }
        }

        public static NuSpec Load(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(NuSpec));
            return (NuSpec)serializer.Deserialize(stream);
        }

    }
}
