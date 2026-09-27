[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/TWZD_Rru)
# web3-pg3-project voxel site

Beschrijf in deze markdown file het **onderwerp** en de **functionaliteit** van jouw project.

Ik heb voor mijn project een website gemaakt zoals DeviantArt, maar alleen voor .vox (voxel) files.
Voordat je de site kan gebruiken moet je je eerst inloggen (of misschien ook registreren).
Als je ingelogd bent zie je een frontpage. op deze frontpage kan je zoeken naar specifieke .vox files. Je kan hier ook makkelijk je favorite .vox files vinden. er is hier ook wat niews van de admin over de site.
Er is ook een pagina om je .vox files te uploaden.
Als je geïnteresseerd bent, in een file kan je het ook, op een aparte pagina, in het groot bekijken en wat commentaar achterlaten.
> [!IMPORTANT]
> Web 3 minimum requirements

>Functionele vereisten

• Gebruik meerdere entiteiten met relaties (1-N of N-N) uit de vereiste van uit het OLOD
Programmeren Gevorderd 3

Een User kan meerdere .vox files uploaden, liken en als favoriet hebben

• Voorzie een volledige CRUD-flow per entiteit (formulieren, validatie, lijst, bewerken,
verwijderen, detail)

dit heb ik gedaan voor comments en site news

• Registratie en inloggen is verplicht te implementeren volgens de richtlijnen van uit het OLOD
Programmeren Gevorderd 3

heb ik

• Gebruik minstens twee rollen (user en admin) en rolgebaseerde UI

Ik heb 2 rollen, maar het enige verschil tussen een admin en een normale gebruiker is dat de admin het niews op de site kan veranderen / aanmaken en deleten

• Zoeken, filteren en pagineren op server-side niveau en tonen op frontend

kan je zien op de frontpage (na inloggen)

• Volledige gesimuleerde betaalflow met Mollie of Stripe en webhookafhandeling op backend

Ik gebruik stripe, als je een donatie maakt, wordt je naam groen

• Gebruik minstens één niet-triviale React context (bv. auth, cart, permissions, …)

Ik heb een favorites context en een AdminAuthorizationContext

• Voorzie een duidelijke UX met states voor loading, error, empty en success

kan je zien op de frontpage en op de Upload page, maar op de individuele .vox file pagina is het wat minder.

• Zorg voor een consistente layout en styling

heb ik gedaan met tailwind

• Voorzie minstens één export functionaliteit (CSV, PDF

het is een pdf, je kan het downloaden van de individuele .vox pagina

>Technische vereisten

• React + TypeScript (nergens any) met Vite

Ik denk niet dat ik ergens any heb

• React Router en Formik (of React Hook Form)

Ik gebruik ze bijden (ik gebruik ook yup)

• Fetchen met fetch API of Axios met foutafhandeling

Ik gebruik Axios

• C# backend uitbreiden waar nodig, met correcte foutcodes en security

Ik denk dat dit ok is

• Frontend en backend apart gescheiden deployen

• CORS correct configureren

Ik kan connecteren vanuit mijn frontend, zo ik denk dat CORS inorde is

• Azure DevOps repository met toegang voor beide docenten

Ik heb dit niet

• Duidelijke commitgeschiedenis

Ik denk dat dit ok is
> [!IMPORTANT]
> PG3 minimum requirements

o Volledige historiek taken en commits in versiebeheer

heb ik

o Volledig live op Azure
https://hogenttaskvoxfilestorage.z1.web.core.windows.net/ 

o Communicatie tussen eigen web services(Identity Server telt nietmee)

de voxdata.Api Kan de BlopAPi aanspreken
de identityserver is de voxdata.Api aanspreken (voor users op de voxdata Databank te registreren)
de stripe payment server Kan de vox-data.API aanspreken

o Communicatie met externe web service via de backend

Ik gebruik de API op https://restcountries.com/

o Gebruik van de drie verschillende opslagtypes

azure blob voor de .vox files and de preview image,
azure sql voor de voxdata Databank en identityserver
azure Cosmos voor het niews

o Authenticatie & Authorisatiemet Identity Serverop alle api en ui elementen

het enige waar ik dit niet op heb is de stripe webhook

o Documentatie (OpenApi + Scalar(geenSwagger) enPostmanCollection)oEenvoudige UI om user login en 1 beveiligde api te demonstreren
de postman files staan in docs en dit zijn de openAPI's
https://vox-payment-server.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io/scalar
https://vox-info-service.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io/scalar
https://vox-blob-service.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io/scalar
https://vox-news-server.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io/scalar
