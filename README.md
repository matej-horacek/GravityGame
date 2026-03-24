# Gravity Game (Pracovní název)
Tento projekt je 3D hra vytvořená v herním enginu Unity na styl klasického "bullet hell". Hlavní mechanikou hry je dynamická změna gravitace, která se přizpůsobuje povrchům, se kterými hráč koliduje. Hra dále obsahuje systém zdraví, pohyb hráče založený na fyzice, uživatelské rozhraní (UI Toolkit) a nepřátelskou umělou inteligenci využívající NavMesh a zorné pole (Field of View).

Hlavním konceptem je vytvořit střílečku kde se hráč a nepřátelé pohybují ve 3d prostoru, který umožňuje unikátní práci s gravitací( Změna směru, velikosti). Někteří nepřátelé a objekty ignorují gravitaci, někteří ne, stejně jako schopnosti hlavní postavy. 

## 🌟 Hlavní funkce
* **Dynamická gravitace:** Gravitace se mění podle povrchu, kterého se hráč dotkne, což umožňuje chůzi po zdech a stropech.
* **Fyzikální pohyb:** Hráč se pohybuje a skáče pomocí `Rigidbody` fyziky a Unity Input Systemu.
* **Systém zdraví a UI:** Hráč má implementované zdraví propojené s vizuálním Health Barem vytvořeným pomocí UI Toolkit.
* **Umělá inteligence:** Nepřátelé využívají zorné pole (úhly pohledu) k detekci hráče a `NavMeshAgent` k jeho pronásledování.
* **Sledování kamerou:** Momentálně se ve hře nachází 2 kamery: First person kamera(pohled z první osoby), která umožnuje otáčení pomocí pohybu myši a rozhlížení se do všech stran. Dále je zde third person camera která umožňuje větší přehled ale na druhou stranu tvoří jiné slepé body. Do budoucna se plánuje přidání 3. Free lock kamery která nebude přímo následovat hráče ale bude spíše statická.

## 🛠️ Technologie a závislosti
* **Engine:** Unity (skripty využívají `UnityEngine` a `Unity.Cinemachine`)
* **Vstupy:** Unity Input System (`UnityEngine.InputSystem`)
* **UI:** UI Toolkit (`UnityEngine.UIElements`)
* **AI:** NavMesh (`UnityEngine.AI`)



## Shrnutí kódu
* **Třída GravitySwitch**
* **Soubor: GravityChanger.cs**
Účel: Zajišťuje hlavní herní mechaniku – změnu vektoru gravitace na základě povrchu, na který hráč dopadne nebo do něj narazí.

Klíčové proměnné:

changeDelay (float): Zpoždění (cooldown) mezi možnými změnami gravitace, aby nedocházelo k chybám při rychlých kolizích.

previousPullDirection (Vector3): Ukládá předchozí směr gravitace.

Klíčové metody:

OnCollisionEnter(Collision other): Detekuje náraz do objektu. Zkontroluje nový směr gravitace a pokud se liší a uběhl časový limit, změní Physics.gravity a zavolá rotaci hráče.

GetPullDirection(Collision other): Vypočítá nový vektor gravitace na základě směru (obráceného up vektoru) objektu, se kterým proběhla kolize.

setRotation(Vector3 newPullDirection): Vypočítá rozdíl rotací (Quaternion.FromToRotation nebo manuální otočení pro 180 stupňů) a otočí hráče. Také hráče mírně odsune od starého povrchu, aby nepropadl mapou.


* **Třída Movements**
* **Soubor: Movements.cs**
Účel: Zpracovává pohyb a skákání hráče s využitím Unity Input Systemu a fyziky (Rigidbody).

Klíčové proměnné:

inputActions (InputActionAsset): Odkaz na nastavení ovládání.

rb (Rigidbody): Fyzikální těleso hráče.

speed / jumpForce (float): Rychlost pohybu a síla skoku.

Klíčové metody:

OnEnable() / OnDisable(): Aktivuje a deaktivuje akce vstupu (Move, Jump) a váže události skoku (Jump_performed).

Update(): Čte hodnoty vstupu pro pohyb z os (Vector2) a převádí je do 3D prostoru (X a Z).

FixedUpdate(): Aplikuje relativní sílu na Rigidbody hráče (ForceMode.Force) pro plynulý fyzikální pohyb.

Jump_performed(): Aplikuje okamžitou sílu (ForceMode.Impulse) směrem nahoru vzhledem k rotaci hráče pro skok.


* **Třída Player**
* **Soubor: Player.cs**
Účel: Spravuje statistiky hráče (zdraví, rychlost, poškození) a stará se o vizualizaci zdraví pomocí UI Toolkit.

Klíčové proměnné:

MaxHealth / Currenthealth (float): Hodnoty maximálního a aktuálního zdraví.

uiDocument (UIDocument): Odkaz na UI dokument, který obsahuje ukazatel zdraví.

Currenthealthbar (ProgressBar): Prvek UI, který vizuálně ukazuje zdraví.

Klíčové metody:

OnEnable(): Inicializuje ukazatel zdraví z UI dokumentu a nastaví jeho viditelnost a rozsah (0 až 1).

Update(): Kontroluje, zda zdraví nekleslo na nebo pod nulu (v takovém případě zničí objekt hráče - Destroy(gameObject)) a průběžně aktualizuje UI.

UpdateCurrenthealthUI(): Přepočítává zdraví na procenta a aktualizuje text (např. "100 / 100") a posuvník v UI.

TakeDamage(float damage): Odečte příslušné poškození z aktuálního zdraví hráče.


* **Třída EnemyScript<T>**
* **Soubor: EnemyScript.cs**
Účel: Generická základní třída pro nepřátele. Stará se o detekci hráče pomocí FOV (zorného pole), pronásledování pomocí NavMeshe a útočení. Předpokládá se použití datového kontejneru EnemyData.

Klíčové proměnné:

EnemyStats (T): Datový objekt obsahující statistiky nepřítele (rychlost, zdraví, dosah, úhel pohledu, poškození).

Eyes (Transform): Bod, ze kterého nepřítel "vidí" (slouží jako počátek pro výpočet zorného pole).

agent (NavMeshAgent): Komponenta pro navigaci a hledání cesty k hráči.

IsPlayerInView (bool): Stavová proměnná určující, zda je hráč momentálně viděn.

Klíčové metody:

Update(): Volá detekci hráče a pokud je hráč viděn a v dosahu (EnemyStats.Range), zavolá útok.

FindPlayer(): Vypočítá vzdálenost a úhel k hráči. Pokud je hráč ve stanoveném horizontálním a vertikálním zorném úhlu, přepne nepřítele do režimu pronásledování (SetDestination) a vypne případný skript pro náhodné bloudění (EnemyWander).

Attack(): Zavolá metodu TakeDamage na skriptu hráče.

OnDrawGizmos(): Pomocná metoda pro vývojáře. Vykresluje v editoru (v okně Scene) vizuální reprezentaci zorného pole nepřítele a barevně ukazuje, zda je hráč detekován.


* **Třída CameraScript**
* **Soubor: CameraScript.cs**
Účel: Skript pro jednoduchou kameru, která drží fixní pozici v osách X a Y, ale sleduje hráče v ose Z.

Klíčové proměnné:

player (Transform): Objekt hráče, kterého má kamera sledovat.

fixedX, fixedY (float): Počáteční souřadnice kamery, které se během hraní nemění.

Klíčové metody:

Start(): Uloží počáteční X a Y pozici kamery do mezipaměti.

LateUpdate(): Posune kameru tak, aby sdílela osu Z s hráčem, avšak zachovává fixní odstup (-250f). Zaručuje, že pohyb kamery proběhne až po zpracování pohybu hráče (díky použití LateUpdate).
