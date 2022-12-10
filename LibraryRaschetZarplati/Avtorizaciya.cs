using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRaschetZarplati
{
    public class Avtorizaciya
    {
        public static string PoluchitHeshirovanniyParol(string parol)
        {
            byte[] sol;
            new RNGCryptoServiceProvider().GetBytes(sol = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(parol, sol, 100000);
            byte[] hesh = pbkdf2.GetBytes(20);
            byte[] heshBiti = new byte[36];
            Array.Copy(sol, 0, heshBiti, 0, 16);
            Array.Copy(hesh, 0, heshBiti, 16, 20);
            string zaheshirovanniyParol = Convert.ToBase64String(heshBiti);

            return zaheshirovanniyParol;
        }

        public static bool ProverkaParolya(string parol, string zaheshirovanniyParolBD)
        {
            byte[] heshBiti = Convert.FromBase64String(zaheshirovanniyParolBD);
            byte[] sol = new byte[16];
            Array.Copy(heshBiti, 0, sol, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(parol, sol, 100000);
            byte[] hesh = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (heshBiti[i + 16] != hesh[i])
                    return false;
            }

            return true;
        }
    }
}
