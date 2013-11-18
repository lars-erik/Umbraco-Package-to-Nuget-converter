using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PackageToNuget.NugetDefinitions;
using PackageToNuget.UmbracoDefinitions;

namespace PackageToNuget
{
    public class PackageMapper
    {
        Regex singleLf = new Regex(@"([^\r]?)\n");

        public NuSpec Map(PackageDefinition definition)
        {
            var nuspec = new NuSpec
            {
                Metadata = new Metadata
                {
                    Id = definition.Info.Package.Name.Replace(" ", "."),
                    Version = definition.Info.Package.Version,
                    LicenseUrl = definition.Info.Package.License.Url,
                    ProjectUrl = definition.Info.Package.Url,
                    Authors = definition.Info.Author.Name,
                    Description = String.IsNullOrWhiteSpace(definition.Info.ReadMe) ?
                        definition.Info.Package.Name :
                        singleLf.Replace(definition.Info.ReadMe, "$1\r\n")
                }
            };

            return nuspec;
        }
    }
}
