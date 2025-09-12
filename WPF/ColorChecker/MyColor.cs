using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorChecker {
    //<summary>
    // 色と色名を保持するクラス
    //</summary>
    public struct MyColor {
        public Color Color { get; set; }
        public string Name { get; set; }
        public override string ToString() {
            return Name ?? string.Format("R : {0,3}  G : {1,3}  B : {2,3}", Color.R, Color.G, Color.B);
        }
    }
}
