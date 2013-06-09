Właściwość: Edycja profilu

Scenariusz: Poprawna zmiana hasła
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Stare hasło tekst haslo1
	Oraz wpiszę do pola Hasło tekst haslo2
	Oraz wpiszę do pola Powtórz hasło tekst haslo2
	Oraz kliknę Zapisz pod formularzem zmiany hasła
	Wtedy zobaczę tekst Hasło zostało zmienione

Scenariusz: Błędne stare hasło
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Stare hasło tekst zlehaslo
	Oraz wpiszę do pola Hasło tekst haslo2
	Oraz wpiszę do pola Powtórz hasło tekst haslo2
	Oraz kliknę Zapisz pod formularzem zmiany hasła
	Wtedy zobaczę tekst Niepoprawne hasło

Scenariusz: Za krótkie nowe hasło
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Stare hasło tekst haslo1
	Oraz wpiszę do pola Hasło tekst has
	Oraz wpiszę do pola Powtórz hasło tekst has
	Oraz kliknę Zapisz pod formularzem zmiany hasła
	Wtedy zobaczę tekst Hasło musi mieć minimum 6 znaków

Scenariusz: Niezgodne hasła
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Stare hasło tekst haslo1
	Oraz wpiszę do pola Hasło tekst haslo2
	Oraz wpiszę do pola Powtórz hasło tekst haslo3
	Oraz kliknę Zapisz pod formularzem zmiany hasła
	Wtedy zobaczę tekst Hasła muszą się zgadzać

Scenariusz: Poprawna zmiana adresu e-mail
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Adres e-mail tekst user2-asknuts@asknuts.com
	Oraz kliknę Zapisz pod formularzem zmiany adresu e-mail
	Wtedy zobaczę tekst Link z potwierdzeniem został wysłany na podany adres.
	Oraz w polu Adres e-mail zobaczę tekst user-asknuts@asknuts.com
	Oraz otrzymam e-mail o temacie AskNuts.com - potwierdzenie adresu e-mail wysłany na adres user2-asknuts@asknuts.com

Scenariusz: Potwierdzenie zmiany adresu e-mail
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Adres e-mail tekst user2-asknuts@asknuts.com
	Oraz kliknę Zapisz pod formularzem zmiany adresu e-mail
	Oraz kliknę link w mailu wysłanym na adres user2-asknuts@asknuts.com
	Oraz przejdę do /edytuj-profil
	Wtedy w polu Adres e-mail zobaczę tekst user2-asknuts@asknuts.com

Scenariusz: Niepoprawny format adresu e-mail
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Adres e-mail tekst user2-asknuts@bob
	Oraz kliknę Zapisz pod formularzem zmiany adresu e-mail
	Wtedy zobaczę tekst Niepoprawny format adresu e-mail

Scenariusz: Wprowadzony e-mail został już wcześniej użyty
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Adres e-mail tekst moderator-asknuts@asknuts.com
	Oraz kliknę Zapisz pod formularzem zmiany adresu e-mail
	Wtedy zobaczę tekst Adres e-mail został już użyty. Podaj inny adres.

Scenariusz: Poprawna zmiana zdjęcia eksperta
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /edytuj-profil
	Oraz prześlę zdjęcie użytkownika avatar_small.jpg
	Wtedy zobaczę niestandardowe zdjęcie użytkownika

Scenariusz: Przesłanie zdjęcia eksperta o zbyt dużym rozmiarze
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /edytuj-profil
	Oraz prześlę zdjęcie użytkownika avatar_toobig.jpg
	Wtedy zobaczę tekst Wybrałes nie wspierany format pliku lub jego rozmiar przekroczył 2MB.

Scenariusz: Zmiana mikroprofilu
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Obecne stanowisko tekst Testowe stanowisko
	Oraz wpiszę do pola Twój opis profilu tekst Testowy opis
	Oraz kliknę Zapisz pod formularzem edycji mikroprofilu
	Wtedy zobaczę tekst Profil został zaktualizowany
	Oraz w polu Obecne stanowisko zobaczę tekst Testowe stanowisko
	Oraz w polu Twój opis profilu zobaczę tekst Testowy opis
