Właściwość: Widok pytania dla autora

Scenariusz: Usunięcie nieopłaconego pytania
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Nieopłacone pytanie testowe
	Oraz kliknę przycisk Usuń pytanie
	Oraz kliknę przycisk Tak
	Wtedy znajdę się na stronie /moje-pytania
	Oraz nie zobaczę tekstu Nieopłacone pytanie testowe

Scenariusz: Opłacenie pytania nieopłaconego w trakcie zadawania
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Nieopłacone pytanie testowe
	Oraz kliknę przycisk Zapłać za pytanie
	Oraz wpiszę do pola Imię tekst T4JNY_c0de#
	Oraz wpiszę do pola Nazwisko tekst test
	Oraz wpiszę do pola Adres e-mail tekst user-asknuts@asknuts.com
	Oraz kliknę przycisk submitPaymentModal
	Oraz przejdę do /
	Oraz przejdę do /moje-pytania
	Oraz kliknę przycisk Nieopłacone pytanie testowe
	Wtedy nie zobaczę tekstu Zapłać za pytanie

Scenariusz: Akceptacja odpowiedzi z oceną
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaakceptuj i oceń odpowiedź!
	Oraz wpiszę do pola Opis tekst Testowa neutralna opinia
	Oraz kliknę radio button Neutralnie
	Oraz kliknę przycisk Oceń i zaakceptuj
	Wtedy poczekam aż zobaczę tekst Zaakceptowałeś odpowiedź

Scenariusz: Akceptacja odpowiedzi bez oceny
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Zaakceptuj i oceń odpowiedź!
	Oraz kliknę przycisk Zaakceptuj bez oceny
	Wtedy poczekam aż zobaczę tekst Zaakceptowałeś odpowiedź

Scenariusz: Akceptacja wyższej ceny
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk Zaakceptuj
	Oraz wpiszę do pola Imię tekst T4JNY_c0de#
	Oraz wpiszę do pola Nazwisko tekst test
	Oraz wpiszę do pola Adres e-mail tekst user-asknuts@asknuts.com
	Oraz kliknę przycisk submitPaymentModal
	Wtedy poczekam aż zobaczę tekst test-expert2 odpowiada na Twoje pytanie

Scenariusz: Edycja pytania
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz w poście o treści Pytanie testowe bez eksperta kliknę przycisk edycji
	Oraz do pola tekstowego wpiszę tekst Zmienione pytanie testowe bez eksperta
	Oraz kliknę przycisk Zapisz
	Oraz odświeżę stronę
	Wtedy zobaczę tekst Zmienione pytanie testowe bez eksperta

Scenariusz: Blokada edycji pytania po przejęciu przez eksperta
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz kliknę przycisk Przejmij pytanie
	Oraz zaloguję się jako użytkownik
	Oraz przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Wtedy nie zobaczę żadnych przycisków edycji

Scenariusz: Edycja odpowiedzi
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Oraz kliknę przycisk Odpowiedz
	Oraz do pola tekstowego wpiszę tekst Testowa odpowiedź
	Oraz kliknę przycisk Dodaj odpowiedź
	Oraz w poście o treści Testowa odpowiedź kliknę przycisk edycji
	Oraz do pola tekstowego wpiszę tekst Zmieniona testowa odpowiedź
	Oraz kliknę przycisk Zapisz
	Oraz odświeżę stronę
	Wtedy zobaczę tekst Zmieniona testowa odpowiedź

Scenariusz: Dodawanie załącznika
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Oraz prześlę załącznik palenque_temple.jpg
	Wtedy zobaczę tekst palenque_temple.jpg
	#Jeśli w poście o treści palenque_temple.jpg kliknę przycisk usunięcia załącznika
	#Wtedy nie zobaczę tekstu palenque_temple.jpg

