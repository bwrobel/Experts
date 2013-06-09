Właściwość: Akcje moderatora

Scenariusz: Dostępność logu systemowego
	Zakładając że jestem zalogowany jako moderator
	Jeśli przejdę do /
	Oraz kliknę przycisk Zdarzenia
	Wtedy zobaczę tekst Log systemowy

Scenariusz: Reakcja na zdarzenie w logu systemowym
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /edytuj-profil
	Oraz wpiszę do pola Publiczna nazwa użytkownika tekst Niezweryfikowana nazwa
	Oraz kliknę Zapisz pod formularzem edycji publicznej nazwy
	Oraz kliknę przycisk Wyloguj się
	Oraz zaloguję się jako moderator
	Oraz przejdę do /
	Oraz kliknę przycisk Zdarzenia
	Oraz kliknę przycisk reakcja
	Oraz kliknę radio button Zaakceptuj
	Oraz do pola tekstowego wpiszę tekst Zaakceptowano nową nazwę eksperta
	Oraz kliknę przycisk Potwierdź
	Wtedy nie zobaczę tekstu reakcja

	