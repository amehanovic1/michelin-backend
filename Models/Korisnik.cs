using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Michelin.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Michelin.Util;

namespace Michelin.Models  
{
	public class Korisnik : IdentityUser, IPretplatnik
	{

		#region Properties

        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Korisničko ime mora sadržati između 3 i 30 karaktera.")]
        public string korisnickoIme { get; set; }

        [Required(ErrorMessage = "Ime je obavezno.")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Ime može sadržavati samo slova.")]
        public string ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Prezime može sadržavati samo slova.")]
        public string prezime { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Broj mobilnog telefona može sadržavati samo brojeve.")]
        public string brojMobitela { get; set; }

        [StringLength(400, MinimumLength = 0, ErrorMessage = "Biografija ne smije prelaziti 400 karaktera.")]
        public string biografija { get; set; }

        [Required]
		[DataType(DataType.Date)]
		public DateTime datumAktivacije { get; set; }

		[DataType(DataType.Date)]
		public DateTime datumDeaktivacije { get; set; }

		[Required]
		public Boolean aktivan { get; set; }

        public string omiljeniRecepti { get; set; } = new string("");

		public string profilnaSlika {get; set;}

        #endregion

        #region Constructor

		public Korisnik(string ime, string prezime, string korisnickoIme, string brojMobitela = null) 
		{   
			this.ime = ime;
			this.prezime = prezime;
			this.korisnickoIme = korisnickoIme;
			this.brojMobitela = brojMobitela;
			datumAktivacije = DateTime.Now;
			aktivan = true;
			omiljeniRecepti = new List<Recept>();
		}

		public Korisnik() { }

        #endregion

        #region Metode
        public List<Recept> pretvoriStringUListu(List<Recept> recepti)
        {
			
			List<Recept> omiljeni = new List<Recept>();
			if (omiljeniRecepti == null) return omiljeni;
			string[] ids = omiljeniRecepti.Split(" ");

			foreach(string id in ids)
            {
				omiljeni.Add(recepti.Find(r=>r.id==id));
            }
			
			return omiljeni;
        }
	 

		public List<Recept> dajKorisnikoveRecepte(List<Recept> sviRecepti)
        {
			List<Recept> recepti = new List<Recept>();

			foreach (Recept recept in sviRecepti)
            {
				if (recept.autor == this)
				{
					recepti.Add(recept);
				}
            }

			return recepti;
        }


        public void DodajUOmiljene(Recept recept)
        {
            if (omiljeniRecepti == null) omiljeniRecepti = new string("");
            omiljeniRecepti += " " + recept.id;
        }

        public void UkloniIzOmiljenih(Recept recept)
        {
            string[] ids = omiljeniRecepti.Split(" ");
            string novi = "";
            foreach (string id in ids)
            {
                if (id == recept.id) continue;
                else
                {
                    novi += " " + id;
                }
            }
            omiljeniRecepti = novi;
        }

        public void ukljuciObavijesti()
        {
			PretplatnikRepozitorij.getInstance().dodajPretplatnika(this);
		}

		public void iskljuciObavijesti()
        {
			PretplatnikRepozitorij.getInstance().ukloniPretplatnika(this);
		}

		public void posaljiMail()
        {
			MimeMessage message = new MimeMessage();

			MailboxAddress from = new MailboxAddress("Michelin",
			"admin@example.com");
			message.From.Add(from);

			MailboxAddress to = new MailboxAddress(korisnickoIme,
			Email);
			message.To.Add(to);

			message.Subject = "Michelin: Novost";
			BodyBuilder bodyBuilder = new BodyBuilder();
			bodyBuilder.HtmlBody = "<h1>Ažurirana je kategorija najboljih recepata!</h1>";
			bodyBuilder.TextBody = "Ažurirana je kategorija najboljih recepata!";
			message.Body = bodyBuilder.ToMessageBody();

			SmtpClient client = new SmtpClient();
			client.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
			client.Authenticate("lemuel.rippin@ethereal.email", "GN2Nf74YUpwNg2tANr");
			client.Send(message);
			client.Disconnect(true);
			client.Dispose();
		}

		public void posaljiPoruku()
        {
			if (brojMobitela != null)
			{
				string broj = brojMobitela.Substring(1);
				string accountSid = Environment.GetEnvironmentVariable("AC641892399a6fc5dd07a4c18fdad2eb31");
				string authToken = Environment.GetEnvironmentVariable("2a4bec242c39c2896de7a28909463d73");

				TwilioClient.Init(accountSid, authToken);

				var message = MessageResource.Create(
					body: "Michelin: Kategorija s najboljim receptima je ažurirana!",
					from: new Twilio.Types.PhoneNumber("+16158007624"),
					to: new Twilio.Types.PhoneNumber("+387" + broj)
				);

				Console.WriteLine(message.Sid);
			}
		}

        #endregion
    }
}


/*public void dodajUOmiljene(Recept recept)
{
    omiljeniRecepti.Add(recept);
}

public void ukloniIzOmiljenih(Recept recept)
{
    omiljeniRecepti.Remove(recept);
}*/
//public List<Recept> omiljeniRecepti { get; set; }