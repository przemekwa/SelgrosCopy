using Newtonsoft.Json;
using RestSharp;
using SelgrosCopy.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace SelgrosCopy
{
    public class PageCreator
    {
        private SelgorsCopyModel selgorsCopyModel;
        private int id;

           const int PAGE_UPDATE_POLAND_ID = 73539382;
            const int PAGE_UPDATE_RUSIA_ID = 73539378;
            const int PAGE_UPDATE_ROMANIA_ID = 73539779;


        public PageCreator(SelgorsCopyModel selgorsCopyModel)
        {
            this.selgorsCopyModel = selgorsCopyModel;

            switch (selgorsCopyModel.Country)
            {
                case "PL":
                    id = PAGE_UPDATE_POLAND_ID;
                    break;
                case "RO":
                    id = PAGE_UPDATE_ROMANIA_ID;
                    break;
                case "RU":
                    id = PAGE_UPDATE_RUSIA_ID;
                    break;
                default:
                     throw new Exception($"{selgorsCopyModel.Country} is not valid country");
            }
        }


        public bool Create()
        {
            var template = GetTemplate();

            template = FillDate(template);

            return CreatePage($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)} (v{selgorsCopyModel.Version})", template);
        }

        private bool CreatePage(string title, string template)
        {
            var rC = new RestClient("https://confluence.service.snp-ag.com/confluence/rest/api/content/");

            var rq = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            rq.AddHeader("Content-Type", "application/json");
            rq.AddHeader("Authorization", $"Basic {Base64Encode(selgorsCopyModel.Configuration["Jira:Pass"])}"); // dotnet user-secrets set "Jira:Pass" "userName:password"

            var body = new Page
            {
                type = "page",
                title = title,
                space = new Space
                {
                    key = "SPDT"
                },
                ancestors = new List<Ancestor>
                {
                    new Ancestor
                    {
                        id = id
                    }
                }.ToArray(),
                body = new Body {
                    storage = new Storage
                    {
                        value = template,
                        representation = "storage"
                    }
                }
            };

            var serializedBody = JsonConvert.SerializeObject(body);
            rq.AddParameter("application/json", serializedBody, ParameterType.RequestBody);

            var result = rC.Execute(rq, Method.POST);

            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private string GetTemplate()
            => GetResourceStream("SelgrosCopy.Templates.release_page.html")
                .Replace(Environment.NewLine, "");


        private string GetResourceStream(string resource)
        {
            var assembly = typeof(SelgrosCopy.Program).GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private string FillDate(string template) =>
                template.Replace("##NEW_VERSION##", $"v{selgorsCopyModel.Version}")
                .Replace("##REALESE_NOTE##", selgorsCopyModel.RealeseNotes.Replace("&","&amp;"))
                .Replace("##SCHEMA_ENDLINE_NUMBER##", selgorsCopyModel.LineEnd)
                .Replace("##WEB_CONFIG_CHANGES##", selgorsCopyModel.WebConfigNotes.Replace("{0}",selgorsCopyModel.Version));

        public static string Base64Encode(string plainText) {
          var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
          return Convert.ToBase64String(plainTextBytes);
        }

    }
}
