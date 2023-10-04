namespace UrlShortener;

public class UrlTransfomer
{
    public static string GenerarCadenaAleatoria()
    {

        const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        List<char> cadenaAleatoria = new List<char>();

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            int indice = rand.Next(caracteresPermitidos.Length);
            cadenaAleatoria.Add(caracteresPermitidos[indice]);
        }

        return string.Join("", cadenaAleatoria);
    }
}
