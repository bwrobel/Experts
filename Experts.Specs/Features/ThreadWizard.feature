Właściwość: Zadawanie pytania

Scenariusz: Brak kategorii
	Jeśli przejdę do /
	Oraz wpiszę treść pytania Testowe pytanie
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Wtedy zobaczę tekst Wybierz kategorię

Scenariusz: Poprawnie wypełniona pierwsza strona
	Jeśli przejdę do /
	Oraz wybiorę kategorię pytania Prawo i podatki oraz wpiszę treść Testowe pytanie
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Wtedy znajdę się na stronie /sprecyzuj-pytanie

Scenariusz: Zmiana szczegółowości odpowiedzi
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Oraz wybiorę kategorię pytania Nauka oraz wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wybiorę z listy Szczegółowość pozycję Dogłębna analiza
	Wtedy zobaczę tekst 35

Scenariusz: Doprecyzowanie pytania
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Oraz wybiorę kategorię pytania Ja i o mnie oraz wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę checkbox Religia
	Oraz kliknę checkbox Magia
	Oraz wybiorę z listy Wiek: pozycję 18-24
	Oraz kliknę przycisk Kontynuuj
	Oraz wypełnię formularz płatności
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Oraz przejdę do /moje-pytania
	Wtedy zobaczę tekst Religia 
	Oraz zobaczę tekst Magia
	Oraz zobaczę tekst 18-24

Scenariusz: Zadanie pytania bez rejestracji
	Jeśli przejdę do /
	Oraz wybiorę kategorię pytania Nauka oraz wpiszę treść Pytanie testowe zadane bez rejestracji
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wypełnię formularz płatności
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności.
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst Pytanie oczekuje na odpowiedź eksperta
	Oraz zobaczę tekst zostało dla Ciebie założone anonimowe konto użytkownika

Scenariusz: Logowanie w trakcie zadawania pytania
	Jeśli przejdę do /
	Oraz wybiorę kategorię pytania Nauka oraz wpiszę treść Pytanie testowe zadane bez rejestracji
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Pomiń ten krok
	Oraz kliknę przycisk zaloguj się
	Oraz wpiszę do pola Adres e-mail tekst user-asknuts@asknuts.com
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz kliknę przycisk Zaloguj się
	Oraz wypełnię formularz płatności
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst Pytanie oczekuje na odpowiedź eksperta
	Oraz zobaczę tekst Wyloguj się

Scenariusz: Zakładanie konta w trakcie zadawania pytania
	Jeśli przejdę do /
	Oraz wybiorę kategorię pytania Nauka oraz wpiszę treść Pytanie testowe zadane bez rejestracji
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wypełnię formularz płatności
	Oraz kliknę checkbox zapoznałem się i akceptuję regulamin strony oraz politykę prywatności.
	Oraz kliknę checkbox chcę założyć bezpłatne konto w serwisie AskNuts.
	Oraz wpiszę do pola Hasło tekst haslo1
	Oraz wpiszę do pola Powtórz hasło tekst haslo1
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst Pytanie testowe zadane bez rejestracji
	Oraz zobaczę tekst Pytanie oczekuje na odpowiedź eksperta
	Oraz zobaczę tekst Wyloguj się

Scenariusz: Informowanie pytającego o zmianach w QA
	Zakładając że ekspert dodał posta do przejętego pytania
	Jeśli zaloguję się jako użytkownik
	Oraz kliknę link w mailu wysłanym na adres user-asknuts@asknuts.com

Scenariusz: Informowanie eksperta o zmianach w QA
	Zakładając że użytkownik dodał posta do swojego pytania
	Jeśli zaloguję się jako ekspert
	Oraz kliknę link w mailu wysłanym na adres expert-asknuts@asknuts.com

Scenariusz: Poprawne dodanie załącznika
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz prześlę załącznik attachment_ok.png
	Wtedy zobaczę tekst attachment_ok.png
	
Scenariusz: Dodanie załącznika: niepoprawny format
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz prześlę załącznik attachment_wrongformat.exe\
	Wtedy zobaczę tekst Wybrałeś nie wspierany format pliku lub jego rozmiar przekroczył 10MB

Scenariusz: Dodanie załącznika: zaduży załącznik
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz prześlę załącznik attachment_toobig.bmp
	Wtedy zobaczę tekst Wybrałeś nie wspierany format pliku lub jego rozmiar przekroczył 10MB

Scenariusz: Zadanie pytania przez wizytówke eksperta
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk test-expert
	Oraz w miniformie wybiorę kategorię pytania Nauka
	Oraz w miniformie wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wypełnię formularz płatności
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst test-expert został zapytany bezpośrednio.
	Oraz zobaczę przycisk Zrezygnuj z odpowiedzi tego eksperta
	Oraz otrzymam e-mail o temacie AskNuts.com - pytanie "Pytanie testowe" wysłany na adres user-asknuts@asknuts.com

Scenariusz: Zadanie pytania przez formularz w katalogu pytań
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Oraz kliknę przycisk Katalog odpowiedzi
	Oraz kliknę przycisk Zamknięte pytanie testowe sanitized
	Oraz w miniformie wybiorę kategorię pytania Nauka
	Oraz w miniformie wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wypełnię formularz płatności
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst test-expert został zapytany bezpośrednio.
	Oraz zobaczę przycisk Zrezygnuj z odpowiedzi tego eksperta
	Oraz otrzymam e-mail o temacie AskNuts.com - pytanie "Pytanie testowe" wysłany na adres user-asknuts@asknuts.com

Scenariusz: Zadanie pytania przez formularz w popularnych ekspertach z tej kategorii
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Oraz kliknę przycisk Popularni eksperci
	Oraz kliknę przycisk ekspert
	Oraz w miniformie wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wypełnię formularz płatności
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst Pytanie testowe
	Oraz zobaczę tekst Pytanie oczekuje na odpowiedź eksperta

Scenariusz: Zadanie pytania przez formularz w popularnych tematach pytań z tej kategorii
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Oraz kliknę przycisk Popularne tematy pytań
	Oraz kliknę przycisk pytanie
	Oraz w miniformie wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wypełnię formularz płatności
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst Pytanie testowe
	Oraz zobaczę tekst Pytanie oczekuje na odpowiedź eksperta

Scenariusz: Zadanie pytania przez formularz w wyszukiwanych zwrotach z tej kategorii
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Oraz kliknę przycisk Popularne frazy
	Oraz kliknę przycisk fraza
	Oraz w miniformie wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wypełnię formularz płatności
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst Pytanie testowe
	Oraz zobaczę tekst Pytanie oczekuje na odpowiedź eksperta

Scenariusz:	Zaproponowanie za małej własnej ceny za pytanie
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Oraz wybiorę kategorię pytania Nauka oraz wpiszę treść Pytanie testowe z własną ceną
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Kontynuuj
	Oraz kliknę przycisk Zaproponuj własną cenę
	Oraz wpiszę do pola Proponowana cena tekst 4
	Oraz kliknę przycisk Wyślij
	Wtedy poczekam aż zobaczę tekst Minimalna wartość pytania wynosi 5 zł

Scenariusz:	Zaproponowanie własnej ceny za pytanie
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Oraz wybiorę kategorię pytania Nauka oraz wpiszę treść Pytanie testowe z własną ceną
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Kontynuuj
	Oraz kliknę przycisk Zaproponuj własną cenę
	Oraz wpiszę do pola Proponowana cena tekst 1337
	Oraz kliknę przycisk Wyślij
	Wtedy zobaczę tekst 1337
	Oraz zobaczę ikonę icon-lock

