using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_appl_MVVM.Data
{
    public class Discipline
    {
        public DisciplineIs DisciplineEnum { get; set; }

        public string DisplayName
        {
            get
            {
                return DisciplineEnum.ToString();
            }
        }
    }
}
