using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    //5.1.1
    public class YearMonth {
        public int Year { get; init; }
        public int Month { get; init; }

        public YearMonth(int year, int month) {
            Year = year;
            Month = month;
        }

        //5.1.2
        //設定されている西暦が２１世紀か判定する
        //Yearが2001
        public bool Is21Century {
            get {
                return Year >= 2001 && Year <= 2100;
            }
        } 
        //5.1.3
        public YearMonth AddOneMonth() {
            if (Month >= 1 && Month <=11) {
                return new YearMonth(Year,Month + 1);
            } else {
                return new YearMonth(Year + 1, 1);
            }
        }
        //5.1.4
        public override string ToString() => $"{Year}年{Month}月";



    }
}
