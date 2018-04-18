using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjektSTI
{
    class Sluzba : ISluzba
    {
        /// <summary>
        /// nastaveni datamineru pro repozitar, uzivatele a accesstoken
        /// </summary>
        /// <param name="repozitar"></param>
        /// <param name="uzivatel"></param>
        /// <param name="access_token"></param>
        /// <returns>T/F - podarilo se nastavit</returns>
        public bool NastavDataMiner(string repozitar, string uzivatel, string access_token){
            Nastaveni n = new Nastaveni() { Repozitar = repozitar, Uzivatel = uzivatel, githubToken = access_token };
            var txt = JsonConvert.SerializeObject(n);
            string cesta = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\config.json";
            try
            {
                if (System.IO.File.Exists(cesta))
                {
                    System.IO.File.WriteAllText(cesta, txt);
                }
                else
                {
                    System.IO.File.WriteAllText(cesta, txt);
                    //using (FileStream fs = System.IO.File.Create(path))
                    //{
                    //    Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    //    // Add some information to the file.
                    //    fs.Write(info, 0, info.Length);
                    //}
                }
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Vraci vsechny soubory vsech commitu uskutecnenych po case.
        /// </summary>
        /// <param name="cas">doba, od ktere se zacnou vyhledavat commity</param>
        /// <returns>List souboru</returns>
        public async Task<List<File>> VratSouboryCommituPoCaseAsync(DateTime cas)
        {
            return await File.VratSouboryCommituDoCasuAsync(cas);
        }

        /// <summary>
        /// Vraci pocet radku od urciteho jazyka v repozitari.
        /// </summary>
        /// <param name="typ">Nazev jazyka eg. java, PHP, ...</param>
        /// <returns>pocet radku</returns>
        public async Task<Decimal> VratPocetBytuJazykuRepozitareAsync(string typ)
        {
            DataMiner dm = new DataMiner();
            return await dm.VratPrehledRadkuJazykuRepozitareAsync(typ);
        }

        /// <summary>
        /// Vraci objekty pro statisticke vyjadreny zavislosti mezi casem commitu a poctem zmenenych radku.
        /// </summary>
        /// <param name="cesta">cesta k souboru</param>
        /// <returns>List objektu s udajema pro statisticke vyuziti.</returns>
        public async Task<List<StatistikaSouboru>> VratStatistikuZmenyRadkuSouboruAsync(string cesta)
        {
            return await StatistikaSouboru.VratStatistikuZmenyRadkuSouboruAsync(cesta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typ"></param>
        /// <returns></returns>
        public async Task<Decimal> SpocitejPocetRadkuVSouborechUrcitehoTypuAsync(string typ)
        {
            return await RootObject.SpocitejPocetRadkuVSouborechUrcitehoTypuAsync(typ);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lokalniCesta"></param>
        /// <param name="nazevSouboruGit"></param>
        /// <param name="shaCommitu"></param>
        /// <returns>T/F uspech ulozeni souboru</returns>
        public async Task<bool> StahniSouborZGituAsync(string lokalniCesta, string nazevSouboruGit, string shaCommitu)
        {
            return await File.UlozSouborGituAsync(lokalniCesta, nazevSouboruGit, shaCommitu);
        }

        /// <summary>
        /// VytvorExcelSeznamCommituAsync(new List<Tuple<string, DateTime>>() { new Tuple<string, DateTime>("abc", DateTime.Now), new Tuple<string, DateTime>("def", DateTime.Now) }, "F:\\STI\\UP\\a.xlsx");
        /// </summary>
        /// <param name="soubory"></param>
        /// <param name="cesta"></param>
        /// <returns>T/F uspech ulozeni souboru</returns>
        public async Task<bool> VytvorExcelSeznamCommituAsync(List<Tuple<string, DateTime>> soubory, string cesta)
        {
            return await Excel.VytvorExcelSeznamCommituAsync(soubory, cesta);
        }
    }

    class Cas
    {
        private Decimal _defaultni_ms { get; set; }
        private Decimal _aktualni_ms { get; set; }

        public Cas(Decimal ms)
        {
            _defaultni_ms = ms;
            _aktualni_ms = ms;
        }

        public void OdectiSekunduAktualnihoCasu()
        {
            if (_aktualni_ms != 0)
            {
                _aktualni_ms -= 1000;
            }
            else
            {
                ResetujAktualniCas();
            }
        }

        public void ResetujAktualniCas()
        {
            _aktualni_ms = _defaultni_ms;
        }

        public string VratAktualniCasFormat()
        {
            return VratAktualniCasMinuty() + ":" + VratAktualniCasSekundy();
        }

        private Decimal VratAktualniCasMinuty()
        {
            return Math.Floor((_aktualni_ms % 3600000) / 60000);
        }

        private Decimal VratAktualniCasSekundy()
        {
            return Math.Floor(((_aktualni_ms % 3600000) % 60000) / 1000);
        }

        public Decimal VratAktualniCasMs()
        {
            return _aktualni_ms;
        }

    }

    class DataMiner
    {
        public DataMiner()
        {
            AdresaServer = "http://api.github.com";
            var txt = System.IO.File.ReadAllText(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\config.json");
            Nastaveni n = JsonConvert.DeserializeObject<Nastaveni>(txt);
            Repozitar = n.Repozitar;
            Uzivatel = n.Uzivatel;
        }
        public string AdresaServer { get; set; }
        public string Repozitar { get; set; }
        public string Uzivatel { get; set; }

        public string VratObsahSouboruGitu(string nazev, string sha)
        {
            return UdelejRequest("https://raw.githubusercontent.com/" + Uzivatel + "/" + Repozitar + "/" + sha + "/" + nazev);
        }

        public string UdelejRequest(string url)
        {
            string znak = url.Contains("?") ? "&" : "?";
            Random rd = new Random();
            url = url + znak + "random=" + rd.Next();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.ContentType = "application/vnd.github.v3+json";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0";
            request.Headers.Add("Pragma", "no-cache");

            request.Headers.Add("Accept-Language", "en-GB,en-US;q=0.8,en;q=0.6");
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.ProtocolVersion = HttpVersion.Version11;
            request.KeepAlive = true;
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.CachePolicy = noCachePolicy;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        public string UdelejRequestGitHub(string url, Dictionary<string, string> parametry = null)
        {
            //https://stackoverflow.com/questions/2859790/the-request-was-aborted-could-not-create-ssl-tls-secure-channel
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var txt = System.IO.File.ReadAllText(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\config.json");
            Nastaveni n = JsonConvert.DeserializeObject<Nastaveni>(txt);
            // github api si nekdy doplni nejakej parametr sam, potrebuju zjistit, jestli uz nejakej parametr existuje, abych mohl navazat
            string znak = url.Contains("?") ? "&" : "?";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + znak + "access_token=" + n.githubToken + "&" + PrevedSlovnikParametruNaString(parametry));
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Reload);
            request.ContentType = "application/vnd.github.v3+json";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0";
            //request.Headers["Time-Zone"] = "Europe/Prague";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        /// <summary>
        /// vraci vsechny commity pro jeden soubor
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public List<DetailZaznamu> VratCommityJednohoSouboru(string cesta)
        {
            var odpovedi = CyklujRequesty(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/commits", new Dictionary<string, string>() { { "path", cesta } });
            List<DetailZaznamu> commity = new List<DetailZaznamu>();
            foreach (var odpoved in odpovedi)
            {
                commity.AddRange(JsonConvert.DeserializeObject<List<DetailZaznamu>>(odpoved));
            }
            return commity;
        }

        public async Task<Decimal> VratPrehledRadkuJazykuRepozitareAsync(string typ)
        {
            return await Task.Run(() => VratPrehledRadkuJazykuRepozitare(typ));
        }

        /// <summary>
        /// vraci pocet radku kodu jazyka v repu
        /// </summary>
        /// <param name="typ"></param>
        /// <returns></returns>
        public Decimal VratPrehledRadkuJazykuRepozitare(string typ)
        {
            var odpoved = UdelejRequestGitHub(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/" + "languages");
            var pocet = JsonConvert.DeserializeObject<dynamic>(odpoved)[typ];
            return pocet ?? 0;
        }

        /// <summary>
        /// Pro zpracovani GET requestu je potreba mit string
        /// </summary>
        /// <param name="parametry"></param>
        /// <returns></returns>
        private string PrevedSlovnikParametruNaString(Dictionary<string, string> parametry)
        {
            if (parametry == null)
            {
                return "";
            }
            string text = "";
            foreach (var param in parametry)
            {
                text += param.Key + "=" + param.Value + "&";
            }
            return text;
        }

        public Decimal SpocitejVyskytRetezceSouboruUrl(string url, string retezec)
        {
            DataMiner dm = new DataMiner();
            var odpoved = dm.UdelejRequest(url);
            return Regex.Matches(odpoved, retezec).Count;
        }

        /// <summary>
        /// ziska z API soubory, ktere jsou pod slozkou
        /// </summary>
        /// <param name="ro"></param>
        /// <returns></returns>
        public List<RootObject> VratSouborySlozky(RootObject ro)
        {
            List<RootObject> vsechnySoubory = new List<RootObject>();
            int stranka = -1;
            List<RootObject> tmpSoubory = new List<RootObject>();
            Dictionary<string, string> nastaveni = new Dictionary<string, string>() { { "per_page", "50" }, { "page", stranka.ToString() } };
            do
            {
                int tmpStranka = int.Parse(nastaveni["page"]) + 1;
                nastaveni["page"] = tmpStranka.ToString();
                string odpoved = UdelejRequestGitHub(ro.url, nastaveni);
                vsechnySoubory.AddRange(JsonConvert.DeserializeObject<RootObject[]>(odpoved).ToList());
            } while (vsechnySoubory.Count / (int.Parse(nastaveni["page"]) + 1) == 50);
            return vsechnySoubory;
        }

        public List<string> CyklujRequesty(string url, Dictionary<string, string> nastaveni = null)
        {
            List<string> vsechnyOdpovedi = new List<string>();
            if (nastaveni == null)
            {
                nastaveni = new Dictionary<string, string>() { { "per_page", "50" }, { "page", "0" } };
            }
            else
            {
                nastaveni.Add("per_page", "50");
                nastaveni.Add("page", "0");
            }
            string odpoved = "";
            while (odpoved != "[\n\n]\n")
            {
                int tmpStranka = int.Parse(nastaveni["page"]) + 1;
                nastaveni["page"] = tmpStranka.ToString();
                odpoved = UdelejRequestGitHub(url, nastaveni);
                if (odpoved != "[\n\n]\n")
                {
                    vsechnyOdpovedi.Add(odpoved);
                }
            };
            return vsechnyOdpovedi;
        }

        public List<Zaznam> VratCommity()
        {
            List<Zaznam> zaznamy = new List<Zaznam>();
            List<string> odpovedi = CyklujRequesty(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/commits");
            foreach (var odpoved in odpovedi)
            {
                zaznamy.AddRange(JsonConvert.DeserializeObject<Zaznam[]>(odpoved).ToList());
            }
            return zaznamy;
        }

        public DetailZaznamu VratDetailCommitu(string sha)
        {
            var odpoved = UdelejRequestGitHub(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/commits/" + sha);
            return JsonConvert.DeserializeObject<DetailZaznamu>(odpoved);
        }

        public List<RootObject> VratSoubory()
        {
            string odpoved = UdelejRequestGitHub(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/contents");
            var ret = JsonConvert.DeserializeObject<RootObject[]>(odpoved).ToList();
            var commity = UdelejRequestGitHub(AdresaServer + "/repos/" + Uzivatel + "/" + Repozitar + "/commits");
            var comm = JsonConvert.DeserializeObject<Commit[]>(commity).ToList();
            foreach (var r in ret)
            {
                r.commit = comm[0];
            }
            return ret;
        }

    }

    public class StatistikaSouboru
    {
        public StatistikaSouboru() { }
        public string nazev { get; set; }
        public DateTime cas_commitu { get; set; }
        public string sha { get; set; }
        public Decimal pridane_radky { get; set; }
        public Decimal odebrane_radky { get; set; }
        public string status { get; set; }

        public static async Task<List<StatistikaSouboru>> VratStatistikuZmenyRadkuSouboruAsync(string cesta)
        {
            return await Task.Run(() => VratStatistikuZmenyRadkuSouboru(cesta));
        }

        public static List<StatistikaSouboru> VratStatistikuZmenyRadkuSouboru(string cesta)
        {
            DataMiner dm = new DataMiner();
            var commity = dm.VratCommityJednohoSouboru(cesta);
            List<DetailZaznamu> detaily = new List<DetailZaznamu>();
            foreach (var commit in commity)
            {
                detaily.Add(dm.VratDetailCommitu(commit.sha));
            }
            List<StatistikaSouboru> statistiky = new List<StatistikaSouboru>();
            foreach (var detail in detaily)
            {
                foreach (var soubor in detail.files)
                {
                    if (soubor.filename == cesta)
                    {
                        statistiky.Add(new StatistikaSouboru() { sha = soubor.sha, nazev = soubor.filename, cas_commitu = detail.commit.committer.date, odebrane_radky = soubor.deletions, pridane_radky = soubor.additions, status = soubor.status });
                    }
                }
            }
            return statistiky;
        }
    }

    public class DetailZaznamu
    {
        public string sha { get; set; }
        public Commit commit { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }
        public Author author { get; set; }
        public Committer committer { get; set; }
        public Parent[] parents { get; set; }
        public Stats stats { get; set; }
        public File[] files { get; set; }
    }

    public class Zaznam
    {
        public string sha { get; set; }
        public Commit commit { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }

        /// <summary>
        /// Vraci vyber z listu zaznamu, ktery bude pozdejsiho data
        /// </summary>
        /// <param name="cas">okamzik, od ktereho se zacne filtrovat</param>
        /// <param name="zaznamy">list, ze ktereho probiha filtrace</param>
        /// <returns></returns>
        public static List<Zaznam> SelektujCasovouPeriodu(List<Zaznam> zaznamy, DateTime cas)
        {
            return zaznamy.Where(c => c.commit.committer.date > cas).ToList();
        }

        public static void VratSouboryZaznamu(List<Zaznam> zaznamy)
        {
            foreach (var z in zaznamy)
            {

            }
        }
    }

    public class Author
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
    }

    public class Committer
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
    }

    public class Tree
    {
        public string sha { get; set; }
        public string url { get; set; }
    }

    public class Verification
    {
        public bool verified { get; set; }
        public string reason { get; set; }
        public object signature { get; set; }
        public object payload { get; set; }
    }

    public class Commit
    {
        public string sha { get; set; }
        public Author author { get; set; }
        public Committer committer { get; set; }
        public string message { get; set; }
        public Tree tree { get; set; }
        public string url { get; set; }
        public int comment_count { get; set; }
        public Verification verification { get; set; }
    }

    public class Author2
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Committer2
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Parent
    {
        public string sha { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
    }

    public class RootObject
    {
        public string sha { get; set; }
        public Commit commit { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }
        public Author2 author { get; set; }
        public Committer2 committer { get; set; }
        public List<Parent> parents { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public int size { get; set; }
        public string git_url { get; set; }
        public string download_url { get; set; }
        public string type { get; set; }
        public Links _links { get; set; }

        public static Decimal SpocitejPocetRadkuVSouborechUrcitehoTypu(string typ)
        {
            return SpocitejPocetRadkuSadySouboru(VratSouboryUrcitehoTypuRepozitare(typ));
        }

        public static async Task<Decimal> SpocitejPocetRadkuVSouborechUrcitehoTypuAsync(string typ)
        {
            return await Task.Run(() => SpocitejPocetRadkuVSouborechUrcitehoTypu(typ));
        }

        /// <summary>
        /// projede sadu souboru a vrati sumu radku
        /// </summary>
        /// <param name="soubory"></param>
        /// <returns></returns>
        public static Decimal SpocitejPocetRadkuSadySouboru(List<RootObject> soubory)
        {
            // znak /n == newline == uvozuje dalsi radek == pocet radku bude vzdycky n+1
            return SpocitejPocetZnakuSadySouboru(soubory, "\n") + soubory.Count;
        }

        /// <summary>
        /// projede sadu souboru a zjisti sumu vyskytu retezce ve vsech
        /// </summary>
        /// <param name="soubory"></param>
        /// <param name="znak"></param>
        /// <returns></returns>
        public static Decimal SpocitejPocetZnakuSadySouboru(List<RootObject> soubory, string znak)
        {
            DataMiner dm = new DataMiner();
            Decimal pocet = 0;
            foreach (var js in soubory)
            {
                var url = "https://cdn.rawgit.com/" + dm.Uzivatel + "/" + dm.Repozitar + "/" + js.commit.sha + "/" + js.path;
                //var url = "https://rawgit.com/" + dm.Uzivatel + "/" + dm.Repozitar + "/master/" + js.path;
                pocet += dm.SpocitejVyskytRetezceSouboruUrl(url, znak);
            }
            return pocet;
        }

        /// <summary>
        /// vyfiltruje ze zadanych souboru jen ty se spravnou koncovkou
        /// </summary>
        /// <param name="soubory"></param>
        /// <param name="koncovka"></param>
        /// <returns></returns>
        public static List<RootObject> SelektujSouboryPodleKoncovky(List<RootObject> soubory, string koncovka)
        {
            var regex = new Regex(@".*." + koncovka + "$");
            return soubory.Where(x => regex.IsMatch(x.path.ToLower())).ToList();
        }

        /// <summary>
        /// vrati vsechny soubory a vyfiltruje podle koncovky typ souboru
        /// </summary>
        /// <param name="typ">koncovka souboru</param>
        /// <returns></returns>
        public static List<RootObject> VratSouboryUrcitehoTypuRepozitare(string typ)
        {
            System.Diagnostics.Debug.WriteLine("soubory");
            var vsechnySoubory = VratVsechnySouboryRepozitareRekurzivne();
            var vybraneSoubory = SelektujSouboryPodleKoncovky(vsechnySoubory, typ);
            return vybraneSoubory;
        }

        public static async Task<List<RootObject>> VratSouboryUrcitehoTypuRepozitareAsync(string typ)
        {
            return await Task.Run(() => VratSouboryUrcitehoTypuRepozitare(typ));
        }

        /// <summary>
        /// main metoda - vrati vsechny soubory repozitare definovaneho v DataMineru
        /// </summary>
        /// <returns></returns>
        private static List<RootObject> VratVsechnySouboryRepozitareRekurzivne()
        {
            DataMiner dm = new DataMiner();
            var rootSouboryRepozitare = dm.VratSoubory();
            List<RootObject> vsechnySoubory = new List<RootObject>();
            foreach (var sr in rootSouboryRepozitare)
            {
                if (sr.type == "dir")
                {
                    vsechnySoubory.AddRange(VratRekurzivneVsechnySoubory(sr));
                }
                else
                {
                    vsechnySoubory.Add(sr);
                }
            }
            return vsechnySoubory;
        }

        /// <summary>
        /// Projde zadanou slozku a podslozky a vrati vsechny soubory
        /// </summary>
        /// <param name="ro">slozka</param>
        /// <returns></returns>
        private static List<RootObject> VratRekurzivneVsechnySoubory(RootObject ro)
        {
            DataMiner dm = new DataMiner();
            List<RootObject> soubory = new List<RootObject>();
            var souborySlozky = dm.VratSouborySlozky(ro);
            foreach (var ss in souborySlozky)
            {
                if (ss.type != "dir")
                {
                    soubory.Add(ss);
                }
                else
                {
                    soubory.AddRange(VratRekurzivneVsechnySoubory(ss));
                }
            }
            return soubory;
        }
    }

    public class Links
    {
        public string self { get; set; }
        public string git { get; set; }
        public string html { get; set; }
    }


    public class Stats
    {
        public int total { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
    }

    public class File
    {
        public string sha { get; set; }
        public string filename { get; set; }
        public string status { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
        public int changes { get; set; }
        public string blob_url { get; set; }
        public string raw_url { get; set; }
        public string contents_url { get; set; }
        public string patch { get; set; }
        public DateTime datum_commitu { get; set; }

        /// <summary>
        /// main metoda pro ziskani souboru z commitu uskutecnenych po zadane dobe
        /// </summary>
        /// <param name="cas">Local time - funkce sama prevede do UTC pro github</param>
        /// <returns></returns>
        public static List<File> VratSouboryCommituDoCasu(DateTime cas)
        {
            cas = cas.ToUniversalTime();
            DataMiner dm = new DataMiner();
            List<Zaznam> zaznamy = dm.VratCommity();
            List<Zaznam> zaznamyHodina = Zaznam.SelektujCasovouPeriodu(zaznamy, cas);
            List<File> soubory = new List<File>();
            // az se nekdo bude ptat, proc to trva tak dlouho, ukaz mu tenhle foreach
            foreach (var z in zaznamyHodina)
            {
                var detail = dm.VratDetailCommitu(z.sha);
                foreach (var det in detail.files)
                {
                    det.datum_commitu = z.commit.committer.date.ToLocalTime();
                    det.sha = z.sha;
                }
                soubory.AddRange(detail.files);
            }
            return soubory;
        }

        public static async Task<List<File>> VratSouboryCommituDoCasuAsync(DateTime cas)
        {
            return await Task.Run(() => (VratSouboryCommituDoCasu(cas)));
        }

        /// <summary>
        /// bool x = await s.StahniSouborZGituAsync("F:\\STI\\UP\\zk.txt", "zk.txt", "29b5c4800c535320504b05972cdec8d1a503f1ab");
        /// </summary>
        /// <param name="cesta">lokalni</param>
        /// <param name="nazev">na gitu</param>
        /// <param name="sha">commit sha</param>
        /// <returns></returns>
        public static bool UlozSouborGitu(string cesta, string nazev, string sha)
        {
            try
            {
                DataMiner dm = new DataMiner();
                string obsah = dm.VratObsahSouboruGitu(nazev, sha);
                using (FileStream fs = System.IO.File.Create(cesta))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(obsah);
                    fs.Write(info, 0, info.Length);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba ukladani " + ex);
         
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cesta">lokalni</param>
        /// <param name="nazev">na gitu</param>
        /// <param name="sha">commit sha</param>
        /// <returns></returns>
        public static async Task<bool> UlozSouborGituAsync(string cesta, string nazev, string sha)
        {
            return await Task.Run(() => UlozSouborGitu(cesta, nazev, sha));
        }

    }

    public class Excel
    {
        public Excel() { }

        public static async Task<bool> VytvorExcelSeznamCommituAsync(List<Tuple<string, DateTime>> soubory, string cesta)
        {
            return await Task.Run(() => VytvorExcelSeznamCommitu(soubory, cesta));
        }

        private static bool VytvorExcelSeznamCommitu(List<Tuple<string, DateTime>> soubory, string cesta)
        {
            using (var package = new ExcelPackage())
            {
                try
                {
                    // Add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Seznam Commitu");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Nazev";
                    worksheet.Cells[1, 2].Value = "Datum";

                    int index = 2;

                    foreach (var tuple in soubory)
                    {
                        worksheet.Cells["A" + index].Value = tuple.Item1;
                        worksheet.Cells["B" + index].Value = tuple.Item2.ToString("dd/MM/yy H:mm:ss");
                        index++;
                    }

                    worksheet.View.PageLayoutView = true;
                    // set some document properties
                    package.Workbook.Properties.Title = "Commity";
                    package.Workbook.Properties.Author = "My";
                    package.Workbook.Properties.Comments = "Komentar";

                    // set some extended property values
                    package.Workbook.Properties.Company = "JKKK";

                    // set some custom property values
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", "US");
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "program a EPPlus");

                    package.SaveAs(new FileInfo(cesta));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
                return true;
            }
        }
    }

}
