Właściwość: Płatności

Scenariusz: Poprawne opłacenie pytania
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Jeśli wybiorę kategorię pytania Dom oraz wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wpiszę do pola Imię tekst Bolek
	Oraz wpiszę do pola Nazwisko tekst Lolek
	Oraz wpiszę do pola Adres e-mail tekst bolek@lolek.pl
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy znajdę się na zewnętrznej stronie https://secure.transferuj.pl/

Scenariusz: Błędne opłacenie pytania: brak danych 
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /
	Jeśli wybiorę kategorię pytania Dom oraz wpiszę treść Pytanie testowe
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Pomiń ten krok
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Wtedy zobaczę tekst Ekspert proponuje wartość odpowiedzi

Scenariusz: Strona bilansu
	Zakładając że jestem zalogowany jako ekspert
	Jeśli kliknę przycisk Moje konto
	Oraz kliknę przycisk Moje płatności
	Wtedy znajdę się na stronie /bilans

Scenariusz: Szczegóły bilansu: pytający
	Zakładając że użytkownik zadał pytanie
	Jeśli przejdę do /bilans
	Wtedy zobaczę tekst Wpłata na poczet pytania 'Pytanie testowe...'

Scenariusz: Szczegóły bilansu: ekspert
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaakceptuj odpowiedź
	Oraz kliknę przycisk Zaakceptuj bez oceny
	Oraz odświeżę stronę
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako ekspert
	Oraz przejdę do /bilans
	Wtedy zobaczę tekst Wynagrodzenie za pytanie 'Pytanie testowe z ekspertem...'

Scenariusz: Wypłata środków poprawna: ekspert
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaakceptuj odpowiedź
	Oraz kliknę przycisk Zaakceptuj bez oceny
	Oraz odświeżę stronę
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako ekspert
	Oraz przejdę do /bilans
	Oraz kliknę przycisk Wypłać środki
	Oraz kliknę przycisk Zleć wypłatę środków
	Wtedy zobaczę tekst Przyjęto zlecenie wypłaty
	Oraz zobaczę tekst Zlecenie wypłaty środków na życzenie klienta

Scenariusz: Wypłata środków brak konta: ekspert
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaakceptuj odpowiedź
	Oraz kliknę przycisk Zaakceptuj bez oceny
	Oraz odświeżę stronę
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako ekspert
	Oraz przejdę do /bilans
	Oraz kliknę przycisk Wypłać środki
	Oraz usunę z pola Numer konta bankowego do wpłaty jego zawartość
	Oraz kliknę przycisk Zleć wypłatę środków
	Wtedy zobaczę tekst Podaj numer konta bankowego

Scenariusz: Wypłata środków brak wartości: ekspert
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaakceptuj odpowiedź
	Oraz kliknę przycisk Zaakceptuj bez oceny
	Oraz odświeżę stronę
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako ekspert
	Oraz przejdę do /bilans
	Oraz kliknę przycisk Wypłać środki
	Oraz usunę z pola Wartość jego zawartość
	Oraz kliknę przycisk Zleć wypłatę środków
	Wtedy zobaczę tekst Podaj wartość przelewu

Scenariusz: Autouzupełnianie formularza płatności z ostatniej wizyty
	Zakładając że użytkownik zadał pytanie
	Jeśli przejdę do /
	Jeśli wybiorę kategorię pytania Dom oraz wpiszę treść Pytanie testowe nr 2
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Pomiń ten krok
	Oraz wpiszę do pola Nazwisko tekst ZMIENIONE NAZWISKO
	Oraz kliknę przycisk Uzyskaj odpowiedź
	Oraz przejdę do /
	Jeśli wybiorę kategorię pytania Dom oraz wpiszę treść Pytanie testowe nr 3
	Oraz kliknę przycisk Zadaj pytanie ekspertowi!
	Oraz kliknę przycisk Pomiń ten krok
	Wtedy zobaczę w elemencie tekst ZMIENIONE NAZWISKO






