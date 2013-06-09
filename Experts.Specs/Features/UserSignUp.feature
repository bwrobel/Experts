Właściwość: Rejestracja użytkownika

Scenariusz: Rejestracja klienta
	Jeśli przejdę do /zarejestruj-sie
	Oraz wpiszę do pola Adres e-mail tekst nie-istnieje@gorrion.pl
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz wpiszę do pola Powtórz hasło tekst haslo1
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności. 
	Oraz kliknę przycisk Zarejestruj się
	Oraz kliknę w linka aktywacyjny użytkownika nie-istnieje@gorrion.pl
	Oraz przejdę do /zaloguj-sie
	Oraz wpiszę do pola Adres e-mail tekst nie-istnieje@gorrion.pl
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz kliknę przycisk Zaloguj się
	Wtedy zobaczę tekst Moje konto

Scenariusz: Rejestracja klienta - walidacja - różne hasła
	Jeśli przejdę do /zarejestruj-sie
	Oraz wpiszę do pola Adres e-mail tekst nie-istnieje@gorrion.pl
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz wpiszę do pola Powtórz hasło tekst haslo2
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności. 
	Oraz kliknę przycisk Zarejestruj się
	Wtedy zobaczę tekst Hasła muszą się zgadzać

Scenariusz: Rejestracja klienta - walidacja - format adresu e-mail
	Jeśli przejdę do /zarejestruj-sie
	Oraz wpiszę do pola Adres e-mail tekst to-nie-jest-poprawny-email
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz wpiszę do pola Powtórz hasło tekst haslo1
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności. 
	Oraz kliknę przycisk Zarejestruj się
	Wtedy znajdę się na stronie /zarejestruj-sie

	
Scenariusz: Rejestracja klienta - duplikacja adresu e-mail
	Jeśli przejdę do /zarejestruj-sie
	Oraz wpiszę do pola Adres e-mail tekst prawoipodatki@asknuts.com
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz wpiszę do pola Powtórz hasło tekst haslo1
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności. 
	Oraz kliknę przycisk Zarejestruj się
	Wtedy zobaczę tekst Adres e-mail został już użyty. Podaj inny adres.

Scenariusz: Regulamin strony podczas rejestracji
  Jeśli przejdę do /zarejestruj-sie
	Oraz kliknę przycisk regulamin strony
	Wtedy znajdę się na stronie /poznaj-asknuts/regulamin
	Oraz zobaczę tekst Regulamin

Scenariusz: Polityka prywatności podczas rejestracji
  Jeśli przejdę do /zarejestruj-sie
	Oraz kliknę przycisk politykę prywatności
	Wtedy znajdę się na stronie /polityka-prywatnosci
	Oraz zobaczę tekst Polityka prywatności