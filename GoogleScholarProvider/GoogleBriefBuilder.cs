﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScholarProviderInterface;
using HtmlAgilityPack;

namespace GoogleScholarProvider
{
    public class GoogleBriefBuilder : IBuilder<HtmlNode>
    {
        #region Private Static Field
        Dictionary<string, IParser<HtmlNode>> _parsers =
            new Dictionary<string, IParser<HtmlNode>>();
        #endregion

        #region Constructor
        public GoogleBriefBuilder()
        { }
        #endregion

        #region Private Method
        private IParser<HtmlNode> GetParser(string type)
        {
            if (_parsers.ContainsKey(type))
                return _parsers[type];
            else
            {
                //TODO: throw an exception
                throw new Exception();
            }
        }
        #endregion

        #region Implement 'IBuilder'
        public BriefEntry Build(HtmlNode item)
        {
            var title = GetParser("title").Parse(item);
            var source = GetParser("source").Parse(item);
            var abstrct = GetParser("abstract").Parse(item);
            var profile = GetParser("profile").Parse(item);
            var citeUrl = GetParser("citeUrl").Parse(item);
            return new GoogleBriefEntry(title, source, abstrct, profile, citeUrl);
        }
        #endregion
    }
}
