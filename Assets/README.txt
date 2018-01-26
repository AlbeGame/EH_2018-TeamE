Nella scena GameScene ho aggiunto un UI Manager con uno script per gestire due diversi pannelli (quello di pausa e quello che chiede la conferma per tornare
al menù principale). Il suo script è abbastanza semplice e contiene fondamentalmente due funzioni: un toggle (che permette di abilitare e disabillitare uno dei due pannelli)
ed una funzione per tornare al menù principale.

i due pannelli invece sono composti esattamente come il menù principale che abbiamo visto durante la lezione: hanno anche loro uno script
MainMenuScript con due opzioni ciascuno:
per quanto riguarda il menù di pausa:

una volta invocato (col tasto esc) si può scegliere di continuare il gioco (il pannello viene semplicemente disabilitato con la funzione toggle)
o di uscire dal gioco. Anche in questo caso il pannello viene disabilitato con la funzione toggle, ma al contempo, e con la stessa funzione, viene
abilitato il pannello che chiede conferma per uscire dal gioco.

Il pannello per uscire dal gioco ha altre due opzioni, una che permette di tornare al menù principale, l'altra che, semplicemente, disabilita questo pannello.

Nota che, grazie all'if inserito nella funzione update del nuovo script, è impossibile invocare il pannello di pausa se si sta visualizzando
il pannello per uscire dalla partita