using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Model.DataModel
{
    public class MultiSelectModel<T, V> where T : class, new() where V : class
    {
        public MultiSelectModel()
        {
            this.SelectedIds = new List<int>();
            this.SelectedIds2 = new List<int>();
            this.Items = new Dictionary<V, List<SelectListItem>>();
        }

        //nullable int if not chosen any value
        public List<int> SelectedIds { get; set; }
        public List<int> SelectedIds2 { get; set; } //this list can be used for multi-select list
        public Dictionary<V, List<SelectListItem>> Items { get; set; }

        //public int SelectedId { get; set; }
        //public int SelectedId2 { get; set; }
        //public IEnumerable<SelectListItem> Items { get; set; }
        //public IEnumerable<SelectListItem> Items2 { get; set; }

        public T Item { get; set; }
    }
}
