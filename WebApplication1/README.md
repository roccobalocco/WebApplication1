## "Blog" - Coseinutili

### Schema database:
<ul>
    <li> <b>Commenti</b>, tabella principale collegata agli utenti sia per quanto riguarda i tag,
        sia per quanto riguarda l'utente che lo ha pubblicato e la/e categoria/e di cui puó fare parte
    </li>
    <li> <b>Tag_Utenti</b>, tabella che riguarda i tag presenti all'interno di ogni commento</li>
    <li> <b>Categorie</b>, tabella collegata a <b>Commenti</b> mediante <b>Commenti_Categorie</b>
        per seguire la convenzione. Se non si specializza, questa tabella può essere eliminata in favore di <b>Commenti_Categorie</b>
    </li>
    <li>
        <b>Utenti</b>, tabella per la gestione degli utenti. Pochi attributi per ora (solo per realizzare la struttura il prima possibile)
        l'attributo Username è <i>univoco</i>
    </li>
</ul>


![Db_Schema](/WebApplication1/Media/Db_diagram_ci.png)


### Schema codice (Provvisorio):
Nella directory <b>API</b> si trovano le utility che svolgono le operazioni basilari del sito.
Vedi inserimento commenti, modifica, eliminazione e così via dicendo.
Nonostante il nome per ora non sono API, dato che sto scrivendo in ASP .NetCore non so se ci sia 
il reale bisogno di realizzarle (con swagger ecc...).

L'applicazione segue il design pattern MVC pertanto sono presenti le rispettive directory.


Per l'accesso al DB si utilizza una classe che implementa DbContext e la si sfrutta anche nelle due 
classi presenti nella dir <b> API </b>. Si utilizza il framework EFCore per la gestione del DB.

### Warning:
Tutto in via provvisoria, in attesa di conferme su come proseguire.
