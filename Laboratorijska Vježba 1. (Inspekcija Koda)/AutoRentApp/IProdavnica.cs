using System;
using System.Collections.Generic;

namespace AutoRentApp
{
    public interface IProdavnica
    {
        MotornoVozilo PretragaPoBrojuSasije(String brSas);
        List<MotornoVozilo> PretragaPoVrsti(String v, Int16 brSjed);
        List<MotornoVozilo> PretragaPoAutmobilu(MotornoVozilo v);
    }
}
