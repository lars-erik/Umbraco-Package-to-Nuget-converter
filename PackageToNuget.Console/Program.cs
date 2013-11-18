using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using NuGet;
using PackageToNuget.UmbracoDefinitions;

namespace PackageToNuget
{
    class Program
    {
        private static readonly Expression<Func<PackageDefinition, IEnumerable>>[] unsupported = new Expression<Func<PackageDefinition, IEnumerable>>[]
        {
            //d => d.Actions,
            d => d.DataTypes,
            d => d.DictionaryItems,
            d => d.DocumentTypes,
            d => d.Languages,
            d => d.Stylesheets,
            d => d.Templates
        };

        private static ConsoleColor origColor;

        static void Main(string[] args)
        {
            string input = null;
            origColor = Console.ForegroundColor;
            var deleteTemp = true;

            if (args.Length != 1)
            {
                WriteUsage();
                return;
            }
            if (args.Length == 1)
                input = args[0];

            if (!File.Exists(input))
            {
                Write(ConsoleColor.Red, "Couldn't find file {0}", input);
                return;
            }

            Write("Attempting to open {0}", input);

            PackageDefinition definition;
            try
            {
                var reader = new PackageReader(input);
                definition = reader.ReadDefinition();
            }
            catch
            {
                Write(ConsoleColor.Red, "Invalid package {0}", input);
                throw;
            }

            Write("Read package definition {0} {1}", definition.Info.Package.Name, definition.Info.Package.Version);

            foreach (var func in unsupported)
            {
                var items = func.Compile()(definition).Cast<object>().ToList();
                if (items.Any())
                {
                    Write(ConsoleColor.Yellow, "{0} contains {1} entries which will be ignored", ((MemberExpression)func.Body).Member.Name, items.Count());
                    deleteTemp = false;
                }
            }

            if (definition.Actions.Any())
            {
                Write(ConsoleColor.Red, "Actions configured, custom logic will be ignored.");
                deleteTemp = false;
            }
            if (!String.IsNullOrWhiteSpace(definition.Control))
            {
                Write(ConsoleColor.Red, "Install control configured, custom logic might be ignored.");
                deleteTemp = false;
            }

            if (String.IsNullOrWhiteSpace(definition.Info.ReadMe))
                Write(ConsoleColor.Yellow, "Empty readme, using name as description");

            var tempId = Guid.NewGuid().ToString("N");
            Write("Making temporary directory {0}", tempId);

            Directory.CreateDirectory(tempId);

            Write("Building nuget structure");

            try
            {
                var packageConverter = new PackageConverter(input);
                packageConverter.GenerateNuget(tempId);
            }
            catch
            {
                Write(ConsoleColor.Red, "Failed to convert package");
                if (deleteTemp)
                    Directory.Delete(tempId, true);
                throw;
            }

            var nuspecName = definition.Info.Package.Name + ".nuspec";
            var nuspecFile = Path.Combine(tempId, nuspecName);
            var packageName = definition.Info.Package.Name + ".nupkg";

            Write("Packaging " + nuspecFile);
            try
            {
                var packager = new PackageBuilder(nuspecFile, tempId, new NullPropertyProvider(), true);
                using (var file = File.Open(packageName, FileMode.Create, FileAccess.ReadWrite))
                {
                    packager.Save(file);
                }
            }
            catch
            {
                Write(ConsoleColor.Red, "Failed to create nuget package");
                Write(ConsoleColor.Red, "Temp directory left untouched, you can try to fix it manually");
                throw;
            }

            Console.WriteLine("{0} generated", packageName);

            if (!deleteTemp)
            {
                Write(ConsoleColor.Yellow, "Problems found, leaving package directory for modification.");
                return;
            }

            Console.WriteLine("Removing temp directory");
            Directory.Delete(tempId, true);
        }

        private static void Write(string message, params object[] args)
        {
            Write(origColor, message, args);
        }

        private static void Write(ConsoleColor color, string message, params object[] args)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message, args);
            Console.ForegroundColor = origColor;
        }

        private static void WriteUsage()
        {
            Console.WriteLine("Usage: PackageToNuget <inputFile>");
        }
    }

    internal class NullPropertyProvider : IPropertyProvider
    {
        public dynamic GetPropertyValue(string propertyName)
        {
            return null;
        }
    }
}
