using LinqConsoleLab.PL.Data;

namespace LinqConsoleLab.PL.Exercises;

public sealed class ZadaniaLinq
{
    /// <summary>
    /// Zadanie:
    /// Wyszukaj wszystkich studentów mieszkających w Warsaw.
    /// Zwróć numer indeksu, pełne imię i nazwisko oraz miasto.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko, Miasto
    /// FROM Studenci
    /// WHERE Miasto = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Zadanie01_StudenciZWarszawy()
    {
        var studenci = DaneUczelni.Studenci.Where(s => s.Miasto == "Warsaw")
            .Select(s => $"{s.NumerIndeksu}, {s.Imie}, {s.Nazwisko}, {s.Miasto}");
        
        return studenci;
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj listę adresów e-mail wszystkich studentów.
    /// Użyj projekcji, tak aby w wyniku nie zwracać całych obiektów.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Studenci;
    /// </summary>
    public IEnumerable<string> Zadanie02_AdresyEmailStudentow()
    {
        var email = DaneUczelni.Studenci
            .Select(s => $"{s.Email}");
        
        return email;
    }

    /// <summary>
    /// Zadanie:
    /// Posortuj studentów alfabetycznie po nazwisku, a następnie po imieniu.
    /// Zwróć numer indeksu i pełne imię i nazwisko.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko
    /// FROM Studenci
    /// ORDER BY Nazwisko, Imie;
    /// </summary>
    public IEnumerable<string> Zadanie03_StudenciPosortowani()
    {
        var studenci = DaneUczelni.Studenci
            .OrderBy(s => s.Nazwisko).OrderBy(s=> s.Imie)
            .Select(s => $"{s.NumerIndeksu}, {s.Imie}, {s.Nazwisko}");
        
        return studenci;
    }

    /// <summary>
    /// Zadanie:
    /// Znajdź pierwszy przedmiot z kategorii Analytics.
    /// Jeżeli taki przedmiot nie istnieje, zwróć komunikat tekstowy.
    ///
    /// SQL:
    /// SELECT TOP 1 Nazwa, DataStartu
    /// FROM Przedmioty
    /// WHERE Kategoria = 'Analytics';
    /// </summary>
    public IEnumerable<string> Zadanie04_PierwszyPrzedmiotAnalityczny()
    {
        var przedmiot = DaneUczelni.Przedmioty.Where(p => p.Kategoria == "Analytics")
            .Select(p => $"{p.Nazwa}, {p.DataStartu}").TakeLast(1);

        if (przedmiot == null)
            return ["Przedmioty nie znaleziony"];
        
        return przedmiot;
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy w danych istnieje przynajmniej jeden nieaktywny zapis.
    /// Zwróć jedno zdanie z odpowiedzią True/False albo Tak/Nie.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Zapisy
    ///     WHERE CzyAktywny = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Zadanie05_CzyIstniejeNieaktywneZapisanie()
    {
        var zapis = DaneUczelni.Zapisy.Where(z => z.CzyAktywny.Equals(false)).Count();

        if (zapis == 0)
            return ["Nie"];
        return ["Tak"];
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy każdy prowadzący ma uzupełnioną nazwę katedry.
    /// Warto użyć metody, która weryfikuje warunek dla całej kolekcji.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Katedra)
    /// THEN 1 ELSE 0 END
    /// FROM Prowadzacy;
    /// </summary>
    public IEnumerable<string> Zadanie06_CzyWszyscyProwadzacyMajaKatedre()
    {
        var prowadzacy = DaneUczelni.Prowadzacy.Any(p => p.Katedra == null);
        
        return prowadzacy ? ["Tak"] : ["Nie"];
    }

    /// <summary>
    /// Zadanie:
    /// Policz, ile aktywnych zapisów znajduje się w systemie.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Zapisy
    /// WHERE CzyAktywny = 1;
    /// </summary>
    public IEnumerable<string> Zadanie07_LiczbaAktywnychZapisow()
    {
        var zapisy = DaneUczelni.Zapisy.Where(z => z.CzyAktywny.Equals(true)).Count().ToString();

        return [zapisy];
    }

    /// <summary>
    /// Zadanie:
    /// Pobierz listę unikalnych miast studentów i posortuj ją rosnąco.
    ///
    /// SQL:
    /// SELECT DISTINCT Miasto
    /// FROM Studenci
    /// ORDER BY Miasto;
    /// </summary>
    public IEnumerable<string> Zadanie08_UnikalneMiastaStudentow()
    {
        var maisto = DaneUczelni.Studenci.OrderBy(s => s.Miasto).Select(s => s.Miasto).Distinct();
        
        return maisto;
    }

    /// <summary>
    /// Zadanie:
    /// Zwróć trzy najnowsze zapisy na przedmioty.
    /// W wyniku pokaż datę zapisu, identyfikator studenta i identyfikator przedmiotu.
    ///
    /// SQL:
    /// SELECT TOP 3 DataZapisu, StudentId, PrzedmiotId
    /// FROM Zapisy
    /// ORDER BY DataZapisu DESC;
    /// </summary>
    public IEnumerable<string> Zadanie09_TrzyNajnowszeZapisy()
    {
        var zapisy = DaneUczelni.Zapisy.OrderBy(z => z.DataZapisu)
            .Select(z => $"{z.DataZapisu}, {z.StudentId}, {z.PrzedmiotId}").Take(3);
        
        return zapisy;
    }

    /// <summary>
    /// Zadanie:
    /// Zaimplementuj prostą paginację dla listy przedmiotów.
    /// Załóż stronę o rozmiarze 2 i zwróć drugą stronę danych.
    ///
    /// SQL:
    /// SELECT Nazwa, Kategoria
    /// FROM Przedmioty
    /// ORDER BY Nazwa
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Zadanie10_DrugaStronaPrzedmiotow()
    {
        var przedmiot = DaneUczelni.Przedmioty.OrderBy(p => p.Nazwa)
            .Select(z => $"{z.Nazwa}, {z.Kategoria}").Skip(2).Take(2);
        
        return przedmiot;
    }

    /// <summary>
    /// Zadanie:
    /// Połącz studentów z zapisami po StudentId.
    /// Zwróć pełne imię i nazwisko studenta oraz datę zapisu.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, z.DataZapisu
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId;
    /// </summary>
    public IEnumerable<string> Zadanie11_PolaczStudentowIZapisy()
    {
        var student = DaneUczelni.Studenci.Join(DaneUczelni.Zapisy, s => s.Id, z => z.Id, (s, z) => new { s, z })
            .Select(e => $"{e.s.Imie}, {e.s.Nazwisko}, {e.z.DataZapisu}");
        
        return student;
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj wszystkie pary student-przedmiot na podstawie zapisów.
    /// Użyj podejścia, które pozwoli spłaszczyć dane do jednej sekwencji wyników.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, p.Nazwa
    /// FROM Zapisy z
    /// JOIN Studenci s ON s.Id = z.StudentId
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId;
    /// </summary>
    public IEnumerable<string> Zadanie12_ParyStudentPrzedmiot()
    {
        var studenci = DaneUczelni.Zapisy.Join(DaneUczelni.Studenci, z => z.Id, s => s.Id, (z, s) => new { z, s })
            .Join(DaneUczelni.Przedmioty, zs => zs.s.Id, p => p.Id, (zs, p) => new { zs, p })
            .Select(e => $"{e.zs.s.Imie}, {e.zs.s.Nazwisko}, {e.p.Nazwa}");
        
        return studenci;
    }

    /// <summary>
    /// Zadanie:
    /// Pogrupuj zapisy według przedmiotu i zwróć nazwę przedmiotu oraz liczbę zapisów.
    ///
    /// SQL:
    /// SELECT p.Nazwa, COUNT(*)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie13_GrupowanieZapisowWedlugPrzedmiotu()
    {
        var przedmiot = DaneUczelni.Zapisy.Join(DaneUczelni.Przedmioty, z => z.PrzedmiotId, p => p.Id, (z, p) => new { z, p })
            .GroupBy(e => e.p.Nazwa).Select(e => $"{e.Key}, {e.Count()}");
        
        return przedmiot;
    }

    /// <summary>
    /// Zadanie:
    /// Oblicz średnią ocenę końcową dla każdego przedmiotu.
    /// Pomiń rekordy, w których ocena końcowa ma wartość null.
    ///
    /// SQL:
    /// SELECT p.Nazwa, AVG(z.OcenaKoncowa)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie14_SredniaOcenaNaPrzedmiot()
    {
        var ocena = DaneUczelni.Zapisy.Where(z => z.OcenaKoncowa != null)
            .Join(DaneUczelni.Przedmioty, z => z.Id, p => p.Id, (z, p) => new { z, p })
            .GroupBy(e => e.p.Nazwa).Select(e => $"{e.Key}, {e.Average(e => e.z.OcenaKoncowa)}");
        
        return ocena;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego prowadzącego policz liczbę przypisanych przedmiotów.
    /// W wyniku zwróć pełne imię i nazwisko oraz liczbę przedmiotów.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, COUNT(p.Id)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie15_ProwadzacyILiczbaPrzedmiotow()
    {
        var przedmiot = DaneUczelni.Prowadzacy
            .GroupJoin(DaneUczelni.Przedmioty, p => p.Id, pr => pr.ProwadzacyId, (p, pr) => new { p, pr })
            .Select(e => $"{e.p.Imie}, {e.p.Nazwisko}, {e.pr.Count()}");
        
        return przedmiot;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego studenta znajdź jego najwyższą ocenę końcową.
    /// Pomiń studentów, którzy nie mają jeszcze żadnej oceny.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, MAX(z.OcenaKoncowa)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY s.Imie, s.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie16_NajwyzszaOcenaKazdegoStudenta()
    {
        var student = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, s => s.Id, z => z.StudentId, 
                (s, z) => new { s.Imie, s.Nazwisko, z.OcenaKoncowa })
            .Where(e => e.OcenaKoncowa != null)
            .GroupBy(e => new {e.Imie, e.Nazwisko})
            .Select(e => 
                $"{e.Key.Imie}, {e.Key.Nazwisko}, {e.Max(e => e.OcenaKoncowa)}");
        
        return student;
    }

    /// <summary>
    /// Wyzwanie:
    /// Znajdź studentów, którzy mają więcej niż jeden aktywny zapis.
    /// Zwróć pełne imię i nazwisko oraz liczbę aktywnych przedmiotów.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Imie, s.Nazwisko
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem()
    {
        var student = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy,  s => s.Id, z => z.StudentId, (s, z) => new { s.Imie, s.Nazwisko, z.CzyAktywny})
            .Where(e => e.CzyAktywny == true)
            .GroupBy(e => new {e.Imie, e.Nazwisko})
            .Select(e => $"{e.Key.Imie}, {e.Key.Nazwisko}, {e.Count() > 0}");

        return student;
    }

    /// <summary>
    /// Wyzwanie:
    /// Wypisz przedmioty startujące w kwietniu 2026, dla których żaden zapis nie ma jeszcze oceny końcowej.
    ///
    /// SQL:
    /// SELECT p.Nazwa
    /// FROM Przedmioty p
    /// JOIN Zapisy z ON p.Id = z.PrzedmiotId
    /// WHERE MONTH(p.DataStartu) = 4 AND YEAR(p.DataStartu) = 2026
    /// GROUP BY p.Nazwa
    /// HAVING SUM(CASE WHEN z.OcenaKoncowa IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych()
    {
        var przedmiot = DaneUczelni.Przedmioty
            .Join(DaneUczelni.Zapisy, p => p.Id, z => z.PrzedmiotId, (p, z) => new { p.Nazwa, p.DataStartu, z.OcenaKoncowa })
            .Where(e => e.DataStartu.Month == 4)
            .Where(e => e.DataStartu.Year == 2026)
            .GroupBy(e => e.Nazwa)
            .Select(e => $"{e.Key}");
        
        return przedmiot;
    }

    /// <summary>
    /// Wyzwanie:
    /// Oblicz średnią ocen końcowych dla każdego prowadzącego na podstawie wszystkich jego przedmiotów.
    /// Pomiń brakujące oceny, ale pozostaw samych prowadzących w wyniku.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, AVG(z.OcenaKoncowa)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// LEFT JOIN Zapisy z ON z.PrzedmiotId = p.Id
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach()
    {
        var ocena = DaneUczelni.Prowadzacy
            .Join(DaneUczelni.Przedmioty, prowadzacy => prowadzacy.Id, przedmiot => przedmiot.ProwadzacyId,
                (prowadzacy, przedmiot) => new { prowadzacy, przedmiot })
            .Join(DaneUczelni.Zapisy, arg => arg.przedmiot.Id, zapis => zapis.PrzedmiotId,
                (arg, zapis) => new { arg, zapis })
            .Where(e => e.zapis.OcenaKoncowa != null)
            .GroupBy(e => new { e.arg.prowadzacy.Imie, e.arg.prowadzacy.Nazwisko })
            .Select(e => $"{e.Key.Imie}, {e.Key.Nazwisko}, {e.Average(e => e.zapis.OcenaKoncowa)}");
        
        return ocena;
    }

    /// <summary>
    /// Wyzwanie:
    /// Pokaż miasta studentów oraz liczbę aktywnych zapisów wykonanych przez studentów z danego miasta.
    /// Posortuj wynik malejąco po liczbie aktywnych zapisów.
    ///
    /// SQL:
    /// SELECT s.Miasto, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Miasto
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Wyzwanie04_MiastaILiczbaAktywnychZapisow()
    {
        var miasta = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy, student => student.Id, zapis => zapis.StudentId,
                (student, zapis) => new { student, zapis })
            .Where(e => e.zapis.CzyAktywny == true)
            .GroupBy(e => new { e.student.Miasto })
            .OrderByDescending(e => e.Count())
            .Select(e => $"{e.Key.Miasto}, {e.Count()}");

        return miasta;
    }

    private static NotImplementedException Niezaimplementowano(string nazwaMetody)
    {
        return new NotImplementedException(
            $"Uzupełnij metodę {nazwaMetody} w pliku Exercises/ZadaniaLinq.cs i uruchom polecenie ponownie.");
    }
}
