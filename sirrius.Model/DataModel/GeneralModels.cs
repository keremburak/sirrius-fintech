using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Model.DataModel
{
    public class Parameters
    {
        public int sourceid { get; set; }  //used for the API URL source
        public string search { get; set; }
        public int index { get; set; }
        public int size { get; set; }
        public string orderBy { get; set; }
        public string querystring { get; set; }
    }

    public class GridResultModel<T>
    {
        public List<T> items { get; set; } = new List<T>();
        public int count { get; set; }
    }

    public class NoContentResult
    {
        public int StatusCode { get; set; }
        public bool Mode { get; set; } = true; //true : Update, false : Delete
        public string Message { get; set; }
    }
}
