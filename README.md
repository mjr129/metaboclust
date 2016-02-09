## MetaboClust ##
MetaboClust is a project to display and analyse metabolomics data, or similar.
It features data correction, cluster analysis and user interactive visualisation.

## Getting Started from Binary ##

* The application runs as a stand-alone application and doesn't require any special install.
* Full instructions are contained in-line, just launch and go!

## Getting Started from Source ##

* The project should be compiled in Visual Studio 2015.
* You'll need to get the MSerialiser and MChart library projects too (also in BitBucket)
* If you haven't placed the projects adjacent to the MetaboliteLevels project
  VS will complain that they are missing - you'll just have to fix the paths to point
  at them correctly.
* You will also get a notification about missing the Math.NET and RDotNet libraries,
  NuGet (part of the latest VS) will automatically download the missing public libraries
  when you first compile and these errors will disappear.

## System Requirements ##

* .NET (Microsoft Windows) (Mono Framework might work for Linux and Macs but is currently untested).
* 4Gb RAM or 8Gb for cluster parameter optimisation, plus any additional required if you have a massive dataset.