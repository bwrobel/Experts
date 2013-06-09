Właściwość: Przeglądanie listy pytań

Scenariusz: Przejście do szczegółów pytania z listy moje-pytania
	Zakładając że jestem zalogowany jako użytkownik
	Jeśli przejdę do /moje-pytania
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Wtedy zobaczę tekst Pytanie oczekuje na odpowiedź eksperta

Scenariusz: Przejście do szczegółów pytania z listy moje-odpowiedzi
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /moje-odpowiedzi
	Oraz kliknę przycisk Pytanie testowe z ekspertem
	Wtedy zobaczę tekst Odpowiadasz na to pytanie

Scenariusz: Przejście do szczegółów nieprzejetego pytania z listy pytania-uzytkownikow
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Oraz kliknę przycisk Pytanie testowe bez eksperta
	Wtedy zobaczę tekst Pytanie oczekuje na odpowiedź eksperta

Scenariusz: Niewyświetlanie nieopłaconych pytań
	Zakładając że jestem zalogowany jako ekspert
	Jeśli przejdę do /pytania-uzytkownikow
	Wtedy nie zobaczę tekstu Nieopłacone pytanie testowe


