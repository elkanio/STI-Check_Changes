using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSTI
{
    interface ISluzba
    {
        Task<List<File>> VratSouboryCommituPoCaseAsync(DateTime cas);
        Task<Decimal> VratPocetBytuJazykuRepozitareAsync(string typ);
        Task<List<StatistikaSouboru>> VratStatistikuZmenyRadkuSouboruAsync(string cesta);
        Task<Decimal> SpocitejPocetRadkuVSouborechUrcitehoTypuAsync(string typ);
        Task<bool> StahniSouborZGituAsync(string lokalniCesta, string nazevSouboruGit, string shaCommitu);
        Task<bool> VytvorExcelSeznamCommituAsync(List<Tuple<string, DateTime>> soubory, string cesta);
        bool NastavDataMiner(string repozitar, string uzivatel, string access_token);
    }
}
