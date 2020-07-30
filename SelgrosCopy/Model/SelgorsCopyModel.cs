using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SelgrosCopy.Model
{
    public class SelgorsCopyModel
    {
        public string Country { get; internal set; }
        public string Version { get; internal set; }
        public string LinesCut { get; internal set; }
        public DirectoryInfo DirInfo { get; internal set; }
        public string UpdateScript { get; internal set; }
        public FileInfo File { get; internal set; }
        public string RealeseNotes { get; internal set; }


        public string LineEnd { get; internal set; }
        public string WebConfigNotes { get; internal set; }
        public IConfigurationRoot Configuration { get; internal set; }

        public string SchemaFilePath { get; set; }
        public string TranslationsFilePath { get; set; }
        public string PolandDestinationPath { get; set; }
        public string RomaniaDestinationPath { get; set; }
        public string RussiaDestinationPath { get; set; }
    }
}
