using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.Phone.Globalization;
using System.Collections.Generic;
using System.Globalization;

namespace Foodler.Common.Helpers
{
    public class AlphaKeyGroup<T> : ObservableCollection<T>
    {
        public delegate string GetKeyDelegate(T item);

        public string Key { get; private set; }

        public AlphaKeyGroup(string key)
        {
            Key = key;
        }

        private static ObservableCollection<AlphaKeyGroup<T>> CreateGroups(SortedLocaleGrouping slg)
        {
            var list = new ObservableCollection<AlphaKeyGroup<T>>();
            foreach (var group in slg.GroupDisplayNames)
            {
                list.Add(new AlphaKeyGroup<T>(group));
            }

            return list;
        }

        public static ObservableCollection<AlphaKeyGroup<T>> CreateGroups(IEnumerable<T> items, CultureInfo ci, GetKeyDelegate getKey,
            bool sort)
        {
            var slg = new SortedLocaleGrouping(ci);
            var list = CreateGroups(slg);

            foreach (var item in items)
            {
                var index = 0;
                if (slg.SupportsPhonetics)
                {
                    //check if your database has yomi string for item
                    //if it does not, then do you want to generate Yomi or ask the user for this item.
                    //index = slg.GetGroupIndex(getKey(Yomiof(item)));
                
                }
                else
                {
                    index = slg.GetGroupIndex(getKey(item));
                }
                if (index >= 0 && index < list.Count)
                {
                    list[index].Add(item);
                }
            }
            if (sort)
            {
                //foreach (var group in list)
                //{
                //    group.Sort((c0, c1) => ci.CompareInfo.Compare(getKey(c0), getKey(c1)));
                //}
            }
            return list;
        }
    }
}
