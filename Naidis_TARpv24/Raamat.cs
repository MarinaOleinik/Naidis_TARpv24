using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpv24
{
    public class Raamat:ContentPage
    {
        TableView tabel;
        SwitchCell sc;
        ImageCell ic;
        TableSection ts;
        EntryCell email_phone;
        Button saada_email, saada_sms;
        public Raamat()
        {

            sc = new SwitchCell { Text = "Teida/Kuva" };
            sc.OnChanged += Kuva_Peida_pilt;
            ic = new ImageCell
            {
                ImageSource = ImageSource.FromFile("bee.png"),
                Text = "Pildi nimetus",
                Detail = "Pildi kirjeldus"
            };
            email_phone = new EntryCell
            {
                Label = "Saada e-mail või sms",
                Placeholder = "Sisesta e-maili adress või telefoni number:",
                Keyboard = Keyboard.Email
            };
            saada_email = new Button
            {
                Text = "Saada email"
            };
            saada_sms = new Button
            {
                Text = "Saada sms"
            };
            saada_email.Clicked += Saada_email_Clicked;
            saada_sms.Clicked += Saada_sms_Clicked;
            ts = new TableSection();
            tabel = new TableView
            {
                Root = new TableRoot("Andmed")
            {
                new TableSection("Põhiandmed:")
                {
                    new EntryCell
                    {
                        Label="Nimetus:",
                        Placeholder="Sisesta andmed",
                        Keyboard=Keyboard.Default
                    }
                },
                new TableSection("Lisaandmed:")
                {
                    new TextCell
                    {
                        Text="Siia saab veebelehe url lisada"
                    },
                    new EntryCell
                    {
                        Label="Veebileht",
                        Placeholder="Sisesta lehe link",
                        Keyboard=Keyboard.Url
                    },
                    sc,

                },
                ts
            }
            };
            HorizontalStackLayout hs = new HorizontalStackLayout();
            hs.Add(saada_email);
            hs.Add(saada_sms);
            Content = new VerticalStackLayout { Children = { tabel, hs } };
        }

        private async void Saada_sms_Clicked(object? sender, EventArgs e)
        {
            string phone = email_phone.Text;
            var message = "Tere tulemast! Saadan sõnumi";
            SmsMessage sms = new SmsMessage(message, phone);
            if (phone != null && Sms.Default.IsComposeSupported)
            {
                await Sms.Default.ComposeAsync(sms);
            }
        }
        private async void Saada_email_Clicked(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(email_phone.Text)) return;
            var message = "Tere tulemast! Saadan email";
            EmailMessage e_mail = new EmailMessage
            {
                Subject = email_phone.Text,
                Body = message,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(new[] { email_phone.Text })
            };
            if (Email.Default.IsComposeSupported)
            {
                await Email.Default.ComposeAsync(e_mail);
            }
            else
            {
                await DisplayAlertAsync("Viga", "E-maili saatmine pole selles seadmes toetatud", "OK");
            }
        }

        private void Kuva_Peida_pilt(object? sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                ts.Title = "Pilt";
                ts.Add(ic);
                sc.Text = "Peida";
                ts.Add(email_phone);
            }
            else
            {
                ts.Title = "";
                ts.Remove(ic);
                ts.Remove(email_phone);
                sc.Text = "Näita veel kord";
            }
        }
    }
}
