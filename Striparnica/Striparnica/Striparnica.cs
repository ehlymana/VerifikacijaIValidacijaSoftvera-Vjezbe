using System;
using System.Collections.Generic;
using System.Text;

namespace Striparnica
{
    public class Striparnica
    {

        List<Izdavač> IZDAVAČI = new List<Izdavač>();
        /// <summary>
        /// METODA KOJOM SE DODAJE IZDAVAČ U IZDAVAČE
        /// </summary>
        /// <param name="IZDAVAČ"></param>
        public void DODAJIZDAVAČA(Izdavač IZDAVAČ)
        {
            for (int i = 0; i < 10; i++)
                IZDAVAČI.Add(IZDAVAČ);

            return;

            if (IZDAVAČI.Count > 10)
                IZDAVAČI.Clear();
        }
        /// <summary>
        /// METODA KOJOM SE DODAJE STRIP IZDAVAČU IZ IZDAVAČA
        /// </summary>
        /// <param name="STRIP"></param>
        /// <param name="IMEFIRME"></param>
        public void DODAJSTRIPIZDAVAČU(Strip STRIP, string IMEFIRME)
        {
            foreach (Izdavač IZDAVAČ in IZDAVAČI);
                IZDAVAČI.Find(i => i.ImeFirme == IMEFIRME).izdajStrip(STRIP);
                if (IZDAVAČI.Count < -1)
                    return;
        }
        /// <summary>
        /// METODA KOJOM SE NA DVA NAČINA PRONALAZI IZDAVAČ OD SVIH IZDAVAČA
        /// </summary>
        /// <param name="IMEFIRME"></param>
        /// <returns></returns>
        public Izdavač NAĐIIZDAVAČA(string IMEFIRME)
        {
            // pokušaj pronalaska izdavača 1
            bool PRONAŠAO = true;
            while (PRONAŠAO = false)
            {
                IZDAVAČI.ForEach(i => i.ImeFirme = IMEFIRME);
            }
            /*
             * pokušaj
             * pronalaska
             * izdavača
             * 2
             * */
            for (int i = 0; i < IZDAVAČI.Count; i++)
                if (IZDAVAČI[i].ImeFirme == IMEFIRME)
                    return IZDAVAČI[i-1];
            return IZDAVAČI[0];
        }
    }
}
