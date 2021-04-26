using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class ValuesHolder
    {
        public DateTime Date { get; set; }

        public int Temperature { get; set; }
        public List<ValuesHolder> listTemperature = new List<ValuesHolder>();

        public void SaveTemperature(DateTime dateTime, int temperature)
        {
            listTemperature.Add(new ValuesHolder { Date = dateTime, Temperature = temperature });

        }


        public void UpdateTemperature(DateTime date, int newTemperature)
        {
            for (int i = 0; i < listTemperature.Count; i++)
            {
                if (listTemperature[i].Date == date)
                    listTemperature[i].Temperature = newTemperature;
            }
        }

        public List<ValuesHolder> ReadTemperature(DateTime dateBegin, DateTime dateEnd)
        {
            var list = new List<ValuesHolder>();
            for (int i = 0; i < listTemperature.Count; i++)
            {
                if (listTemperature[i].Date >= dateBegin && listTemperature[i].Date <= dateEnd)
                {
                    list.Add(listTemperature[i]);
                }

            }
            return list;
        }

        public void DeleteTemperature(DateTime dateBegin, DateTime dateEnd)
        {
            for (int i = 0; i < listTemperature.Count; i++)
            {
                if (listTemperature[i].Date >= dateBegin && listTemperature[i].Date <= dateEnd)
                {
                    listTemperature.RemoveAt(i);
                    i--;
                }

            }
        }

    }
}
