using CommandLine;
using CommandLine.Text;
using SelgrosCopy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelgrosCopy
{
    class CommandLineOptions
    {
        [Option('v', "version", Required = true, HelpText = "Version of application from TC build.")]
        public string Version { get; set; }

        [Option('c', "country", Required = true, HelpText = "Country of package.")]
        public string Country { get; set; }

        [Option('l', "line-number", Required = true, HelpText = "Line number after read all database changes.")]
        public int LineNumber { get; set; }

        [Option('r', "release-notes", Required = true, HelpText = "Release notes.")]
        public string ReleaseNotes { get; set; }

        [Option('w', "webconfig-notes", Required = false, HelpText = "Web.config chnages notes")]
        public string WebConfigNotes { get; set; }


         public static implicit operator SelgorsCopyModel(CommandLineOptions commandLineOptions)
        {
            return new SelgorsCopyModel
            {
                Country = commandLineOptions.Country,
                Version = commandLineOptions.Version,
                LinesCut = commandLineOptions.LineNumber.ToString(),
                RealeseNotes = commandLineOptions.ReleaseNotes,
                WebConfigNotes = commandLineOptions.WebConfigNotes?? $"Zmiana numeru wersji na {commandLineOptions.Version}"
            };
        }
    }
}
