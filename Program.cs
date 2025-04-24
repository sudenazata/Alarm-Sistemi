using System;
using System.IO.Ports; // Seri porttan veri okumak için;
using MySql.Data.MySqlClient; // MySQL bağlantısı için

class Program
{
    static SerialPort serialPort;

    static void Main()
    {
        // Arduino'nun bağlı olduğu port ve baud rate
        serialPort = new SerialPort("COM5", 9600);
        serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        serialPort.Open();

        Console.WriteLine("Veri bekleniyor... Arduino'dan gelen veriler alınacak.");
        Console.ReadLine(); // Program kapanmasın diye
        serialPort.Close();
    }

    // Seri porttan veri gelince tetiklenen olay
    public static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            string data = serialPort.ReadLine().Trim();
            Console.WriteLine("Gelen Veri: " + data);
            SaveToDatabase(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Veri okuma hatası: " + ex.Message);
        }
    }

    // Veritabanına veri kaydetme
    public static void SaveToDatabase(string distance)
    {
        // Bağlantı cümlesi — XAMPP MySQL içindir
        string veri= "server=localhost;user=root;database=mesafe;port=3306;password=;";

        using (var conn = new MySqlConnection(veri))
        {
            try
            {
                conn.Open();

                string tablo = "INSERT INTO mesafe_kayitlari (mesafe, zaman) VALUES (@mesafe, NOW())";
                MySqlCommand kayıt = new MySqlCommand(tablo, conn);
                kayıt.Parameters.AddWithValue("@mesafe", distance);
                kayıt.ExecuteNonQuery();

                Console.WriteLine("Veri başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Veritabanı hatası: " + ex.Message);
            }
        }
    }
}