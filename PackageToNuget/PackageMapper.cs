using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackageToNuget.NugetDefinitions;
using PackageToNuget.UmbracoDefinitions;

namespace PackageToNuget
{
    public class PackageMapper
    {
        public NuSpec Map(PackageDefinition definition)
        {
            var nuspec = new NuSpec
            {
                Metadata = new Metadata
                {
                    Id = definition.Info.Package.Name,
                    Version = definition.Info.Package.Version,
                    LicenseUrl = definition.Info.Package.License.Url,
                    ProjectUrl = definition.Info.Package.Url,
                    Authors = definition.Info.Author.Name
                }
            };

            return nuspec;
        }
    }
}
