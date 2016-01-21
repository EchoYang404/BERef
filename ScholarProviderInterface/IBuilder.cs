﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarProviderInterface
{
    public interface IBuilder<T>
    {
        BriefEntry Build(T item);

    }
}
