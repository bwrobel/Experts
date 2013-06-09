Właściwość: Rejestracja eksperta

Scenariusz: Błędna rejestracja eksperta, zły format maila
    Jeśli przejdę do /
	Jeśli kliknę przycisk Zostań ekspertem
	Oraz wpiszę do pola Adres e-mail tekst BLEDNY_MAIL
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz wpiszę do pola Powtórz hasło tekst haslo1
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności.
	Oraz wpiszę do pola Imię tekst Irek
	Oraz wpiszę do pola Nazwisko tekst Irkowaty
	Oraz wpiszę do pola Numer telefonu tekst 123456789
	Oraz zaznaczę kategorię Dom
	Oraz kliknę przycisk Zarejestruj się
	Wtedy znajdę się na stronie /zarejestruj-sie-jako-ekspert

Scenariusz: Błędna rejestracja eksperta, mail już istnieje
	Jeśli przejdę do /
	Jeśli kliknę przycisk Zostań ekspertem
	Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz wpiszę do pola Powtórz hasło tekst haslo1
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności.
	Oraz wpiszę do pola Imię tekst Irek
	Oraz wpiszę do pola Nazwisko tekst Irkowaty
	Oraz wpiszę do pola Numer telefonu tekst 123456789
	Oraz zaznaczę kategorię Dom
	Oraz kliknę przycisk Zarejestruj się
	Wtedy zobaczę tekst Adres e-mail został już użyty. Podaj inny adres.

Scenariusz: Błędna rejestracja eksperta, krótkie hasło
	Jeśli przejdę do /
	Jeśli kliknę przycisk Zostań ekspertem
	Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
	Oraz wpiszę do pola Hasło tekst 123
	Oraz wpiszę do pola Powtórz hasło tekst 123
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności.
	Oraz wpiszę do pola Imię tekst Irek
	Oraz wpiszę do pola Nazwisko tekst Irkowaty
	Oraz wpiszę do pola Numer telefonu tekst 123456789
	Oraz zaznaczę kategorię Dom
	Oraz kliknę przycisk Zarejestruj się
	Wtedy zobaczę tekst Hasło musi mieć minimum 6 znaków

Scenariusz: Błędna rejestracja eksperta, hasła się nie zgadzają
	Jeśli przejdę do /
	Jeśli kliknę przycisk Zostań ekspertem
	Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
	Oraz wpiszę do pola Hasło tekst 123456
	Oraz wpiszę do pola Powtórz hasło tekst 654321
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności.
	Oraz wpiszę do pola Imię tekst Irek
	Oraz wpiszę do pola Nazwisko tekst Irkowaty
	Oraz wpiszę do pola Numer telefonu tekst 123456789
	Oraz zaznaczę kategorię Dom
	Oraz kliknę przycisk Zarejestruj się
	Wtedy zobaczę tekst Hasła muszą się zgadzać

Scenariusz: Poprawna rejestracja eksperta
	Jeśli przejdę do /
	Jeśli kliknę przycisk Zostań ekspertem
	Oraz wpiszę do pola Adres e-mail tekst sebz@lansmail.info
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz wpiszę do pola Powtórz hasło tekst haslo1
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności.
	Oraz wpiszę do pola Imię tekst Irek
	Oraz wpiszę do pola Nazwisko tekst Irkowaty
	Oraz wpiszę do pola Numer telefonu tekst 123456789
	Oraz zaznaczę kategorię Dom
	Oraz kliknę przycisk Zarejestruj się
	Oraz kliknę w linka aktywacyjny użytkownika sebz@lansmail.info
	Wtedy zobaczę tekst Twoje konto zostało aktywowane