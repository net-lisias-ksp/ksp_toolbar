﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Allgemeine Informationen über eine Assembly werden über die folgenden 
// Attribute gesteuert. Ändern Sie diese Attributwerte, um die Informationen zu ändern,
// die mit einer Assembly verknüpft sind.
[assembly: AssemblyTitle("Toolbar Plugin for Kerbal Space Program")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Toolbar")]
[assembly: AssemblyCopyright("Copyright © 2013-2019 Maik Schreiber, LinuxGuruGamer & LisiasT")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Durch Festlegen von ComVisible auf "false" werden die Typen in dieser Assembly unsichtbar 
// für COM-Komponenten. Wenn Sie auf einen Typ in dieser Assembly von 
// COM zugreifen müssen, legen Sie das ComVisible-Attribut für diesen Typ auf "true" fest.
[assembly: ComVisible(false)]

// Die folgende GUID bestimmt die ID der Typbibliothek, wenn dieses Projekt für COM verfügbar gemacht wird
[assembly: Guid("7195c089-be8e-4f12-8d49-f77c78722e80")]

// Versionsinformationen für eine Assembly bestehen aus den folgenden vier Werten:
//
//      Hauptversion
//      Nebenversion 
//      Buildnummer
//      Revision
//
// Sie können alle Werte angeben oder die standardmäßigen Build- und Revisionsnummern 
// übernehmen, indem Sie "*" eingeben:
// [assembly: AssemblyVersion("1.0.*")]
//[assembly: AssemblyVersion("1.0.0.4")]
//[assembly: AssemblyFileVersion("1.7.13.0")]

[assembly: AssemblyVersion(Toolbar.Version.Number)]
[assembly: KSPAssembly("Toolbar", Toolbar.Version.major, Toolbar.Version.minor)]

[assembly: KSPAssemblyDependency("KSPe", 2, 1)]
[assembly: KSPAssemblyDependency("KSPe.UI", 2, 1)]

