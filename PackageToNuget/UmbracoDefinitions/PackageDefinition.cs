using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace PackageToNuget.UmbracoDefinitions
{
    [XmlRoot("umbPackage")]
    public class PackageDefinition : IEquatable<PackageDefinition>
    {
        [XmlArray("files")]
        [XmlArrayItem("file")]
        public List<PackageFile> Files
        {
            get; set;
        }

        [XmlElement("info")]
        public PackageInfo Info
        {
            get; set;
        }

        #region equality

        public bool Equals(PackageDefinition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ((Files != null && other.Files != null && Files.SequenceEqual(other.Files)) || (Files == null && other.Files == null)) && Equals(Info, other.Info);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PackageDefinition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Files != null ? Files.GetHashCode() : 0)*397) ^ (Info != null ? Info.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PackageDefinition left, PackageDefinition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PackageDefinition left, PackageDefinition right)
        {
            return !Equals(left, right);
        }

        #endregion

        public static PackageDefinition Load(string path)
        {
            PackageDefinition def;
            using (var stream = File.OpenText(path))
            {
                def = Load(stream);
            }
            return def;
        }

        public static PackageDefinition Load(StreamReader reader)
        {
            var serializer = new XmlSerializer(typeof (PackageDefinition));
            return (PackageDefinition) serializer.Deserialize(reader);
        }

        public static PackageDefinition Load(Stream stream)
        {
            var serializer = new XmlSerializer(typeof (PackageDefinition));
            return (PackageDefinition) serializer.Deserialize(stream);
        }
    }
}
