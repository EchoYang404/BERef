﻿using HtmlAgilityPack;
using ScholarProviderInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoogleScholarProvider
{
    public sealed class GoogleBriefEntry : BriefEntry
    {

        #region Private Field
        private string _citeUrl;
        #endregion

        #region Constructor
        public GoogleBriefEntry(
            string title, 
            string source, 
            string abstrct, 
            string profile, 
            string citeUrl)
            : base(title, source, abstrct, profile)
        {
            CiteUrl = citeUrl;
        }
        #endregion

        #region Public Field
        public string CiteUrl
        {
            set
            {
                if (value != null)
                    _citeUrl = value;
                else
                {
                    throw new NullReferenceException("CiteUrl is required.");
                }
            }
        }
        #endregion

        #region Implement Abstract Method
        /// <summary>
        /// Get BibTeX from Google Scholar.
        /// </summary>
        /// <returns></returns>
        protected override string GetBibTeX()
        {
            string bibtex = null;
            var citeWeb = new HtmlWeb();
            // Get Bibtex url from cite url list.
            var parameter = citeWeb.Load(_citeUrl).DocumentNode.SelectSingleNode(RuleSet.CiteUrlPath)?.Attributes["href"].Value;
            if (parameter != null)
            {
                var citeUrl = $"{RuleSet.GoogleScholarUrl}{WebUtility.HtmlDecode(parameter)}";

                var webRequest = WebRequest.Create(citeUrl);
                var webResponse = webRequest.GetResponse();

                
                var response = webResponse.GetResponseStream();
                if (response != null)
                {
                    using (var reader = new StreamReader(response, Encoding.Default))
                    {
                        bibtex = reader.ReadToEnd();
                    }
                }
            }
            return bibtex;
        }
        #endregion
    }
}
