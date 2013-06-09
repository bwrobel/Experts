Właściwość: Logowanie użytkownika

Scenariusz: Poprawne logowanie użytkownika
		Jeśli przejdę do /zaloguj-sie
		Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
		Oraz wpiszę do pola Hasło tekst haslo1
		Oraz kliknę przycisk Zaloguj się
		Wtedy znajdę się na stronie /
		Oraz zobaczę tekst Moje konto

Scenariusz: Błędny login użytkownika
		Jeśli przejdę do /zaloguj-sie
		Oraz wpiszę do pola Adres e-mail tekst BŁĘDNY_LOGIN@BLA.PL
		Oraz wpiszę do pola Hasło tekst haslo1
		Oraz kliknę przycisk Zaloguj się
		Wtedy zobaczę tekst Niepoprawny e-mail lub hasło

Scenariusz: Błędne hasło użytkownika
		Jeśli przejdę do /zaloguj-sie
		Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
		Oraz wpiszę do pola Hasło tekst haslo123456789101112
		Oraz kliknę przycisk Zaloguj się
		Wtedy zobaczę tekst Niepoprawny e-mail lub hasło

Scenariusz: Błędne hasło i błedny login użytkownika
		Jeśli przejdę do /zaloguj-sie
		Oraz wpiszę do pola Adres e-mail tekst BŁĘDNY_LOGIN@BLA.PL
		Oraz wpiszę do pola Hasło tekst haslo123456789101112
		Oraz kliknę przycisk Zaloguj się
		Wtedy zobaczę tekst Niepoprawny e-mail lub hasło

Scenariusz: Brak danych przy logowaniu
		Jeśli przejdę do /zaloguj-sie
		Oraz kliknę przycisk Zaloguj się
		Wtedy znajdę się na stronie /zaloguj-sie

Scenariusz: Poprawne przekierowanie przy logowaniu
		Jeśli przejdę do /edytuj-profil
		Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
		Oraz wpiszę do pola Hasło tekst haslo1
		Oraz kliknę przycisk Zaloguj się 
		Wtedy znajdę się na stronie /edytuj-profil

Scenariusz: Poprawne przypomnienie hasła
		Jeśli przejdę do /zaloguj-sie
		Oraz kliknę przycisk Przypomnij mi hasło
		Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
		Oraz kliknę przycisk Wyślij
		Oraz kliknę link w mailu wysłanym na adres moderator-asknuts@asknuts.com
		Oraz wpiszę do pola Hasło tekst haslo2
		Oraz wpiszę do pola Powtórz hasło tekst haslo2
		Oraz kliknę przycisk Wyślij
		Wtedy zobaczę tekst Hasło zostało zmienione

Scenariusz: Błędne przypomnienie hasła: nie zgadzające się hasła
		Jeśli przejdę do /zaloguj-sie
		Oraz kliknę przycisk Przypomnij mi hasło
		Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
		Oraz kliknę przycisk Wyślij
		Oraz kliknę link w mailu wysłanym na adres moderator-asknuts@asknuts.com
		Oraz wpiszę do pola Hasło tekst haslo2
		Oraz wpiszę do pola Powtórz hasło tekst haslo3
		Oraz kliknę przycisk Wyślij
		Wtedy zobaczę tekst Hasła muszą się zgadzać

Scenariusz: Błędne przypomnienie hasła: nie istniejacy email
		Jeśli przejdę do /zaloguj-sie
		Oraz kliknę przycisk Przypomnij mi hasło
		Oraz wpiszę do pola Adres e-mail tekst dafuq-am-i-writing-here@asknuts.com
		Oraz kliknę przycisk Wyślij
		Wtedy zobaczę tekst Niepoprawny adres e-mail

Scenariusz: Błędne przypomnienie hasła: zly format maila
		Jeśli przejdę do /zaloguj-sie
		Oraz kliknę przycisk Przypomnij mi hasło
		Oraz wpiszę do pola Adres e-mail tekst this-is-not-a-legal-email-hue-hue-hue
		Oraz kliknę przycisk Wyślij
		Wtedy znajdę się na stronie /przypomnij-haslo