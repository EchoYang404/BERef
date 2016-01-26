﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ScholarProviderInterface
{
    public class WebSearchProvider : ISearchProvider
    {
        #region Private Field
        private ILoader<HtmlDocument>            _loader;
        private ISpliter<HtmlDocument, HtmlNode> _spliter;
        private IBuilder<HtmlNode>               _builder;
        #endregion

        #region Constructor
        public WebSearchProvider(
            ILoader<HtmlDocument> loader,
            ISpliter<HtmlDocument, HtmlNode> spliter,
            IBuilder<HtmlNode> builder)
        {
            _loader  = loader;
            _spliter = spliter;
            _builder = builder;
        }

        public IList<BriefEntry> GetResults(string keyword)
        {
            var result = new List<BriefEntry>();

            var data = _loader.Load(keyword);
            foreach(var item in _spliter.Split(data))
            {
                var briefEntry = _builder.Build(item);
                result.Add(briefEntry);
            }
            return result;
        }
        #endregion
    }
}
