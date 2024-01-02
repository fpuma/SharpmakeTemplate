# SharpmakeTemplate
Template with a sharpmake setup to create VisualStudio solutions based on the source files and libraries to be used

To generate the solutions make sure initialize the sharpmakeutils module and run:

`sharpmake/sharpmakeutils/buildsharpmake.bat`

and then

`generateprojects.bat`

To check the C# files that sharpmake uses run:

`sharpmake/sharpmakefiles/main/generatedebugsharpmakesolution.bat`

and open the generated VisualStudio solution

