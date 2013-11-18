Umbraco-Package-to-Nuget-converter
==================================

Converts Umbraco packages to Nuget packages.  
Supports packages with content- and binary files.  
For Umbraco DB stuff, you'll have to do some manual operations after install.  
For instance adding doctypes with uSync.  
When problems like these are found, the package source directory is left on disk,  
so you could for instance add content\usync\documenttypes\whatever..

Usage: PackageToNuget <inputfile>

Example:

    PackageToNuget imagegen_2.9.0.zip

Output:

    Attempting to open imagegen_2.9.0.zip  
    Read package definition ImageGen 2.9.0  
    Install control configured, custom logic might be ignored.  
    Making temporary directory 949d90251d0240b6bd9b86497999a122  
    Building nuget structure  
    Packaging 949d90251d0240b6bd9b86497999a122\ImageGen.nuspec  
    ImageGen.nupkg generated  
    Problems found, leaving package directory for modification.

Would generate nuget package without web.config transform done in ImageGen's installed control.

Example:

    PackageToNuget doctypemixins_2.0.zip'

Output:

    Attempting to open ..\..\..\testdata\DocTypeMixins_2.0.zip  
    Read package definition DocTypeMixins 2.0  
    DocumentTypes contains 1 entries which will be ignored  
    Empty readme, using name as description  
    Making temporary directory f24ee9a174374c0dbcecf02d995f949a  
    Building nuget structure  
    Packaging f24ee9a174374c0dbcecf02d995f949a\DocTypeMixins.nuspec  
    Removing temp directory  
    DocTypeMixins.nuspec generated  

Would generate nuget package without root doctype for mixins.
Must be done with uSync or similar.

