Właściwość: Widok pytania dla eksperta

Scenariusz: Przejecie pytania
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk Przejmij pytanie
	Wtedy zobaczę tekst Czas do odblokowania pytania:

Scenariusz: Przejecie pytania i dodanie odpowiedzi
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk Przejmij pytanie
	Oraz kliknę przycisk Odpowiedz
	Oraz do pola tekstowego wpiszę tekst Testowa odpowiedź
	Oraz kliknę przycisk Dodaj odpowiedź
	Wtedy poczekam aż zobaczę tekst Testowa odpowiedź
	Oraz nie zobaczę tekstu Czas do odblokowania pytania

Scenariusz: Przejecie pytania i prośba o doprecyzowanie
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk Przejmij pytanie
	Oraz kliknę przycisk Poproś o doprecyzowanie
	Oraz do pola tekstowego wpiszę tekst Testowa odpowiedź
	Oraz kliknę przycisk Dodaj odpowiedź
	Wtedy poczekam aż zobaczę tekst Testowa odpowiedź
	Oraz nie zobaczę tekstu Czas do odblokowania pytania

Scenariusz: Przedłużenie czasu przejęcia pytania
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk Przejmij pytanie
	Oraz kliknę przycisk Przedłuż czas przejęcia
	Wtedy zobaczę tekst Czas do odblokowania pytania

Scenariusz: Rezygnacja z odpowiedzi na pytanie
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Wycofaj się z pytania
	Oraz wpiszę do pola Komentarz tekst Testowy powód rezygnacji
	Oraz kliknę przycisk Wyślij
	Wtedy zobaczę tekst Zrezygnowałeś z odpowiedzi na to pytanie

Scenariusz: Zgłoszenie problemu z pytaniem
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk zgłoś problem
	Oraz wybiorę z listy Typ problemu pozycję Duplikat
	Oraz wpiszę do pola Opis tekst Testowy opis problemu
	Oraz kliknę przycisk Zgłoś
	Wtedy zobaczę tekst Problem został zgłoszony

Scenariusz: Poprawne zaproponowanie wyższej ceny
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk Zaproponuj cenę
	Oraz wpiszę do pola Proponowana cena tekst 50
	Oraz wpiszę do pola Komentarz tekst Testowe uzasadnienie wyższej ceny
	Oraz kliknę przycisk Wyślij
	Wtedy poczekam aż zobaczę tekst Zaproponowałeś nową cenę: 50

Scenariusz: Zaproponowanie ceny niższej niż początkowa
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk Zaproponuj cenę
	Oraz wpiszę do pola Proponowana cena tekst 2
	Oraz wpiszę do pola Komentarz tekst Testowe uzasadnienie niższej ceny
	Oraz kliknę przycisk Wyślij
	Wtedy zobaczę tekst Proponowana cena musi być wyższa od pierwotnej

Scenariusz: Edycja odpowiedzi po usunięciu posta przez autora
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Odpowiedz
	Oraz do pola tekstowego wpiszę tekst Testowa odpowiedź
	Oraz kliknę przycisk Dodaj odpowiedź
	Oraz w poście o treści Testowa odpowiedź kliknę przycisk usunięcia
	Oraz zaloguję się jako ekspert
	Oraz przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz w poście o treści Odpowiedź testowa kliknę przycisk edycji
	Oraz do pola tekstowego wpiszę tekst Zmieniona odpowiedź testowa
	Oraz kliknę przycisk Zapisz
	Oraz odświeżę stronę
	Wtedy zobaczę post o treści Zmieniona odpowiedź testowa

Scenariusz: Poprawne zaproponowanie dodatkowej usługi
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaproponuj dodatkową usługę
	Oraz wpiszę do pola Tytuł tekst Dodatkowa usługa 1
	Oraz wpiszę do pola Cena tekst 10
	Oraz wpiszę do pola Opis tekst Testowy opis 1
	Oraz kliknę przycisk Wyślij
	Wtedy zobaczę tekst Proponujesz dodatkową usługę

Scenariusz: Błędne zaproponowanie dodatkowej usługi
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaproponuj dodatkową usługę
	Oraz wpiszę do pola Cena tekst 20
	Oraz wpiszę do pola Opis tekst Testowy opis 2
	Oraz kliknę przycisk Wyślij
	Wtedy zobaczę tekst Podaj tytuł dodatkowej usługi

Scenariusz: Odrzucenie dodatkowej usługi
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaproponuj dodatkową usługę
	Oraz wpiszę do pola Tytuł tekst Dodatkowa usługa 3
	Oraz wpiszę do pola Cena tekst 30
	Oraz wpiszę do pola Opis tekst Testowy opis 3
	Oraz kliknę przycisk Wyślij
	Oraz przejdę do /
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako moderator
	Oraz kliknę przycisk Zdarzenia
	Oraz kliknę przycisk reakcja
	Oraz kliknę przycisk Potwierdź
	Oraz przejdę do /
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako użytkownik
	Oraz przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Odrzuć
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako ekspert
	Oraz przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Wtedy zobaczę tekst Pytający zrezygnował z dodatkowej usługi

Scenariusz: Zaakceptowanie dodatkowej usługi
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaproponuj dodatkową usługę
	Oraz wpiszę do pola Tytuł tekst Dodatkowa usługa 3
	Oraz wpiszę do pola Cena tekst 30
	Oraz wpiszę do pola Opis tekst Testowy opis 3
	Oraz kliknę przycisk Wyślij
	Oraz przejdę do /
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako moderator
	Oraz kliknę przycisk Zdarzenia
	Oraz kliknę przycisk reakcja
	Oraz kliknę przycisk Potwierdź
	Oraz przejdę do /
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako użytkownik
	Oraz przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaakceptuj
	Oraz wypełnię formularz płatności
	Oraz kliknę przycisk Zapłać
	Oraz przejdę do /
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako ekspert
	Oraz przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Wtedy zobaczę tekst Pytający zaakceptował dodatkową usługę. Udziel mu odpowiedzi zgodnie ze wcześniejszym opisem

Scenariusz: Nieaktualna dodatkowa usługa
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaproponuj dodatkową usługę
	Oraz wpiszę do pola Tytuł tekst Dodatkowa usługa 4
	Oraz wpiszę do pola Cena tekst 40
	Oraz wpiszę do pola Opis tekst Testowy opis 4
	Oraz kliknę przycisk Wyślij
	Oraz kliknę przycisk Wycofaj się z pytania
	Oraz wpiszę do pola Komentarz tekst Testowy powód rezygnacji
	Oraz kliknę przycisk Wyślij
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako moderator
	Oraz kliknę przycisk Zdarzenia
	Oraz kliknę przycisk reakcja
	Oraz kliknę przycisk Potwierdź
	Oraz przejdę do /
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako użytkownik
	Oraz przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Wtedy zobaczę tekst Ta usługa została zadana przez eksperta, który zrezygnował z odpowiedzi. Akceptacja tej usługi jest obecnie nie możliwa




