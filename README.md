MetaboliteLevels is a project to display and analyse metabolomics data, or similar.
It features data correction, cluster analysis and user interactive visualisation.

== Getting Started from Binary ==

* The application runs as a stand-alone application and doesn't require any special install.
* Full instructions are contained in-line, just launch and go!

== Getting Started from Source ==

* The project should be compiled in Visual Studio 2015.
* You'll need to get the MSerialiser and MChart libraries too (also in BitBucket)
* When opening the MetaboliteLevels project VS will complain that the above libraries
  are missing, unless your filesystem happens to cohere to mine, you'll have to fix
  their paths.
* You will also get a notification about missing the Math.NET and RDotNet libraries,
  NuGet (part of the latest VS) will automatically download the missing public libraries
  when you first compile and the errors will disappear.

== System Requirements ==

* Microsoft Windows (unfortulately the graphing component is Microsoft-only, I'm working on replacing this!)
* 4Gb RAM or 8Gb for cluster parameter optimisation, plus any additional required if you have a massive dataset.
