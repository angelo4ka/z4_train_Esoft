using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRaschetZarplati
{
    public class SpravochnayaInformaciya
    {
        public static Dictionary<string, int> Statusi = new Dictionary<string, int>() {
            { "запланирована", 0 },
            { "исполняется", 1 },
            { "выполнена", 2 },
            { "отменена", 3 }
        };
        public static Dictionary<string, int> HaracterZadachi = new Dictionary<string, int>() {
            { "анализ и проектирование", 0 },
            { "установка оборудования", 1 },
            { "техническое обслуживание и сопровождение", 2 }
        };
    }
}
