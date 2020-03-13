using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StackAGoal.Services
{
    /// <summary>
    /// This service allows for the HTML templates to be filled out
    /// via set parameters and returned.
    /// </summary>
    public class EmailTemplateService
    {
        private IHostingEnvironment _hosting;
        private Dictionary<string,string> templateFields;

        public string HTMLEmailTemplateFileName { get; private set; }
        public string HTMLBody { get; private set; }

        /// <summary>
        /// Creates an instance of the service which relies on hosting service
        /// to retrieve root information.
        /// </summary>
        /// <param name="hosting">Main Hosting Environment</param>
        public EmailTemplateService(IHostingEnvironment hosting)
        {
            _hosting = hosting;
            templateFields = new Dictionary<string, string>();
        }

        /// <summary>
        /// This build the body with the populated fields for a HTML template.
        /// </summary>
        /// <param name="htmlFileName"></param>
        /// <returns></returns>
        public async Task<string> BuildHTMLTemplateBodyAsync(string htmlFileName)
        {
            HTMLEmailTemplateFileName = htmlFileName;
            HTMLBody = await ReadEmailTemplateToStringAsync();
            if (BodyContainsToken(HTMLBody))
            {
                HTMLBody = ReplaceTemplateFields(HTMLBody);
            }
            return HTMLBody;
        }

        /// <summary>
        /// This will read the HTML file template to be used 
        /// asynchrously and setup it's use in constructing a 
        /// template containing fields.
        /// </summary>
        /// <returns></returns>
        private async Task<string> ReadEmailTemplateToStringAsync()
        {
            string templatePath = System.IO.Path.Combine(_hosting.WebRootPath, @"EmailTemplates\", HTMLEmailTemplateFileName);
            string templateBody = string.Empty;

            using (StreamReader reader = new StreamReader(templatePath))
            {
                templateBody = await reader.ReadToEndAsync();
            }
            return templateBody;
        }

        /// <summary>
        /// This will add a template field to be replaced by finding it using
        /// the token parameters.
        /// </summary>
        /// <param name="fieldToReplace">Field to replace</param>
        /// <param name="value">Value to be used.</param>
        public void AddTemplateField(string fieldToReplace,string value)
        {
            templateFields.Add($"%%{fieldToReplace}%%", value);
        }

        /// <summary>
        /// This will replace all the values with the tokens found in the html.
        /// </summary>
        /// <param name="templateBody"></param>
        /// <returns>Returns the HTML with it's parameters filled out.</returns>
        private string ReplaceTemplateFields(string templateBody)
        {
            string newBody = templateBody;
            foreach (KeyValuePair<string,string> field in templateFields)
            {
                newBody = newBody.Replace($"{field.Key}", field.Value);
            }
            return newBody;
        }
        /// <summary>
        /// This will check to see if the HTML contains any tokens that
        /// may need to be
        /// </summary>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        private bool BodyContainsToken(string htmlBody)
        {
            if (!string.IsNullOrWhiteSpace(htmlBody) && htmlBody.Contains("%%") && templateFields.Count > 0)
                return true;
            else
                return false;
        }
    }
}

