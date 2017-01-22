using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.MVC5Web.Models
{
    public class PagerModel
    {
        public int PageIndex { get; set; }

        public PagerModel()
        {
            PageIndex = 1;
        }
    }
}