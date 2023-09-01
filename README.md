# DormsProject
 Pasi rulare aplicatie
1.
-restore baza de date
-ne creem pe pgadmin o baza de date goala
-click dreapta pe ea si restore unde lasam formatul custom + selectam dormsFinal.sql din folderul dormsDB

2.
-conexiunea cu baza de date
-run comanda cu numele bazei de date create mai inainte, password-ul de la intrarea in postgres si, la Postgres properties verificam numele host-ului si portul, daca portul nu e cel default trebuie mentionat,daca nu, nu

scaffold-dbcontext “host=localhost;database=name;username=postgres;password=pass;port=1111” npgsql.entityframeworkcore.postgresql -outputdir models
