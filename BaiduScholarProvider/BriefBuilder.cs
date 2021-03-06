﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScholarProviderInterface;

namespace BaiduScholarProvider
{
    public class BriefBuilder:IBuilder<HtmlNode>
    {
        #region Private Readonly Field
        private readonly Dictionary<string, IParser<HtmlNode>> _parsers =
            new Dictionary<string, IParser<HtmlNode>>
            {
                {"title"   , new TitleParser() },
                {"source"  , new SourceParser() },
                {"abstract", new AbstractParser() },
                {"profile" , new ProfileParser() },
                {"citeUrl" , new CiteUrlParser() }
            };
        #endregion

        #region Private Method
        private IParser<HtmlNode> GetParser(string type)
        {
            Debug.Assert(_parsers.ContainsKey(type));
            return _parsers[type];
        }
        #endregion

        #region Implement 'IBuilder'
        public BriefEntry Build(HtmlNode item)
        {
            var citeUrl = GetParser("citeUrl").Parse(item);
            if (citeUrl == null)
                return null;
            var title = GetParser("title").Parse(item);
            var source = GetParser("source").Parse(item);
            var abstrct = GetParser("abstract").Parse(item);
            var profile = GetParser("profile").Parse(item);
            return new BaiduBriefEntry(title, source, abstrct, profile, citeUrl);
        }
        #endregion
    }
}
